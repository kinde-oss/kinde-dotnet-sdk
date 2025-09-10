#!/usr/bin/env node

/**
 * .NET Standard 2.1 Compatibility Fix Script
 * 
 * This script applies compatibility fixes to generated C# files to ensure
 * they work with both .NET 8.0 and .NET Standard 2.1.
 */

const fs = require('fs');
const path = require('path');

// Colors for console output
const colors = {
    reset: '\x1b[0m',
    bright: '\x1b[1m',
    red: '\x1b[31m',
    green: '\x1b[32m',
    yellow: '\x1b[33m',
    blue: '\x1b[34m',
    magenta: '\x1b[35m',
    cyan: '\x1b[36m'
};

function log(message, color = colors.reset) {
    console.log(`${color}${message}${colors.reset}`);
}

function logInfo(message) {
    log(`[INFO] ${message}`, colors.blue);
}

function logSuccess(message) {
    log(`[SUCCESS] ${message}`, colors.green);
}

function logWarning(message) {
    log(`[WARNING] ${message}`, colors.yellow);
}

function logError(message) {
    log(`[ERROR] ${message}`, colors.red);
}

/**
 * Fix certificate data issues in XML comments
 */
function fixCertificateData(content) {
    // Pattern to match unescaped certificate data in XML comments
    const certificatePattern = /(\s*\/\/\/ <value>[^<]*<\/value>\s*)([A-Za-z0-9+/=]{50,})\s*(\[JsonPropertyName)/g;
    
    let modified = content.replace(certificatePattern, (match, xmlComment, certificateData, jsonProperty) => {
        logWarning(`Removing unescaped certificate data from XML comment`);
        return xmlComment + jsonProperty;
    });
    
    return modified;
}

/**
 * Fix ReadAsStringAsync compatibility issues
 */
function fixReadAsStringAsync(content) {
    // Pattern to match the problematic ReadAsStringAsync call
    const pattern = /await httpResponseMessageLocalVar\.Content\.ReadAsStringAsync\(cancellationToken\)\.ConfigureAwait\(false\);/g;
    
    if (pattern.test(content)) {
        logInfo('Fixing ReadAsStringAsync compatibility...');
        
        // Replace with the .NET Standard 2.1 compatible version
        const replacement = 'await httpResponseMessageLocalVar.Content.ReadAsStringAsync().ConfigureAwait(false);';
        const newContent = content.replace(pattern, replacement);
        
        logSuccess('ReadAsStringAsync compatibility fixed');
        return newContent;
    }
    
    return content;
}

/**
 * Fix DateOnlyJsonConverter.cs
 */
function fixDateOnlyJsonConverter(content, filePath) {
    if (!filePath.includes('DateOnlyJsonConverter.cs')) {
        return content;
    }
    
    logInfo('Fixing DateOnlyJsonConverter.cs...');
    
    // Wrap the entire file content with conditional compilation
    const wrappedContent = `#if NET6_0_OR_GREATER
${content}
#else
// For .NET Standard 2.1, DateOnly is not available
// This converter is not needed
#endif`;
    
    logSuccess('DateOnlyJsonConverter.cs compatibility fixed');
    return wrappedContent;
}

/**
 * Fix DateOnlyNullableJsonConverter.cs
 */
function fixDateOnlyNullableJsonConverter(content, filePath) {
    if (!filePath.includes('DateOnlyNullableJsonConverter.cs')) {
        return content;
    }
    
    logInfo('Fixing DateOnlyNullableJsonConverter.cs...');
    
    // Wrap the entire file content with conditional compilation
    const wrappedContent = `#if NET6_0_OR_GREATER
${content}
#else
// For .NET Standard 2.1, DateOnly is not available
// This converter is not needed
#endif`;
    
    logSuccess('DateOnlyNullableJsonConverter.cs compatibility fixed');
    return wrappedContent;
}

/**
 * Fix HostConfiguration.cs
 */
function fixHostConfiguration(content, filePath) {
    if (!filePath.includes('HostConfiguration.cs')) {
        return content;
    }
    
    logInfo('Fixing HostConfiguration.cs...');
    
    // Find and wrap DateOnly converter registrations
    const dateOnlyConverterPattern = /(_jsonOptions\.Converters\.Add\(new DateOnlyJsonConverter\(\)\);[\s\S]*?_jsonOptions\.Converters\.Add\(new DateOnlyNullableJsonConverter\(\)\);)/;
    
    if (dateOnlyConverterPattern.test(content)) {
        const newContent = content.replace(dateOnlyConverterPattern, `#if NET6_0_OR_GREATER
            $1
#endif`);
        
        logSuccess('HostConfiguration.cs compatibility fixed');
        return newContent;
    }
    
    return content;
}

/**
 * Fix ClientUtils.cs
 */
function fixClientUtils(content, filePath) {
    if (!filePath.includes('ClientUtils.cs')) {
        return content;
    }
    
    logInfo('Fixing ClientUtils.cs...');
    
    let newContent = content;
    
    // Fix DateOnly usage in ParameterToString method - more flexible pattern
    const dateOnlyPattern = /(if \(obj is DateOnly dateOnly\)[\s\S]*?return dateOnly\.ToString\(format\);)/;
    if (dateOnlyPattern.test(newContent)) {
        newContent = newContent.replace(dateOnlyPattern, `#if NET6_0_OR_GREATER
            $1
#endif`);
        logInfo('Fixed DateOnly usage in ClientUtils.cs');
    }
    
    // Fix GeneratedRegex attribute - more flexible pattern
    const generatedRegexPattern = /(\[GeneratedRegex\("\([^"]+\)"\)\]\s*private static partial Regex JsonRegex\(\);)/;
    if (generatedRegexPattern.test(newContent)) {
        newContent = newContent.replace(generatedRegexPattern, `#if NET7_0_OR_GREATER
        $1
#else
        private static Regex JsonRegex() => new Regex("(?i)^(application/json|[^;/ \\t]+/[^;/ \\t]+[+]json)[ \\t]*(;.*)?$");
#endif`);
        logInfo('Fixed GeneratedRegex in ClientUtils.cs');
    }
    
    logSuccess('ClientUtils.cs compatibility fixed');
    return newContent;
}

/**
 * Fix RateLimitProvider.cs
 */
function fixRateLimitProvider(content, filePath) {
    if (!filePath.includes('RateLimitProvider')) {
        return content;
    }
    
    logInfo('Fixing RateLimitProvider.cs...');
    
    let newContent = content;
    
    // Fix System.Threading.Channels usage in AvailableTokens declaration
    const channelsPattern = /internal Dictionary<string, global::System\.Threading\.Channels\.Channel<TTokenBase>> AvailableTokens \{ get; \} = new\(\);/;
    if (channelsPattern.test(newContent)) {
        newContent = newContent.replace(channelsPattern, `#if NET6_0_OR_GREATER
        internal Dictionary<string, global::System.Threading.Channels.Channel<TTokenBase>> AvailableTokens { get; } = new();
#else
        internal Dictionary<string, System.Collections.Concurrent.ConcurrentQueue<TTokenBase>> AvailableTokens { get; } = new();
#endif`);
        logInfo('Fixed AvailableTokens declaration in RateLimitProvider.cs');
    }
    
    // Fix constructor - System.Threading.Channels usage
    const constructorPattern = /(global::System\.Threading\.Channels\.BoundedChannelOptions options = new global::System\.Threading\.Channels\.BoundedChannelOptions\(_tokens\.Length\)[\s\S]*?AvailableTokens\.Add\(string\.Empty, global::System\.Threading\.Channels\.Channel\.CreateBounded<TTokenBase>\(options\)\);)/;
    if (constructorPattern.test(newContent)) {
        newContent = newContent.replace(constructorPattern, `#if NET6_0_OR_GREATER
            $1
#else
            // For .NET Standard 2.1, we'll use a simple queue-based approach
            AvailableTokens.Add(string.Empty, new System.Collections.Concurrent.ConcurrentQueue<TTokenBase>());
#endif`);
        logInfo('Fixed constructor in RateLimitProvider.cs');
    }
    
    // Fix event handler assignment
    const eventHandlerPattern = /(token\.TokenBecameAvailable \+= \(\(sender\) => availableToken\.Value\.Writer\.TryWrite\(\(TTokenBase\)sender\)\);)/;
    if (eventHandlerPattern.test(newContent)) {
        newContent = newContent.replace(eventHandlerPattern, `#if NET6_0_OR_GREATER
                    $1
#else
                    token.TokenBecameAvailable += ((sender) => ((System.Collections.Concurrent.ConcurrentQueue<TTokenBase>)availableToken.Value).Enqueue((TTokenBase)sender));
#endif`);
        logInfo('Fixed event handler in RateLimitProvider.cs');
    }
    
    // Fix GetAsync method
    const getAsyncPattern = /(if \(!AvailableTokens\.TryGetValue\(header, out global::System\.Threading\.Channels\.Channel<TTokenBase>\? tokens\)\)[\s\S]*?return await tokens\.Reader\.ReadAsync\(cancellation\)\.ConfigureAwait\(false\);)/;
    if (getAsyncPattern.test(newContent)) {
        newContent = newContent.replace(getAsyncPattern, `#if NET6_0_OR_GREATER
            $1
#else
            if (!AvailableTokens.TryGetValue(header, out System.Collections.Concurrent.ConcurrentQueue<TTokenBase>? tokens))
                throw new KeyNotFoundException($"Could not locate a token for header '{header}'.");

            // For .NET Standard 2.1, we'll use a simple polling approach
            while (!cancellation.IsCancellationRequested)
            {
                if (tokens.TryDequeue(out TTokenBase? token))
                    return token;

                await System.Threading.Tasks.Task.Delay(10, cancellation);
            }

            throw new System.OperationCanceledException();
#endif`);
        logInfo('Fixed GetAsync method in RateLimitProvider.cs');
    }
    
    logSuccess('RateLimitProvider.cs compatibility fixed');
    return newContent;
}

/**
 * Process a single file
 */
function processFile(filePath) {
    try {
        const content = fs.readFileSync(filePath, 'utf8');
        let newContent = content;
        
        // Apply all fixes
        newContent = fixCertificateData(newContent);
        newContent = fixReadAsStringAsync(newContent);
        newContent = fixDateOnlyJsonConverter(newContent, filePath);
        newContent = fixDateOnlyNullableJsonConverter(newContent, filePath);
        newContent = fixHostConfiguration(newContent, filePath);
        newContent = fixClientUtils(newContent, filePath);
        newContent = fixRateLimitProvider(newContent, filePath);
        
        // Only write if content changed
        if (newContent !== content) {
            fs.writeFileSync(filePath, newContent, 'utf8');
            logSuccess(`Processed: ${filePath}`);
            return true;
        }
        
        return false;
    } catch (error) {
        logError(`Error processing ${filePath}: ${error.message}`);
        return false;
    }
}

/**
 * Find all C# files in the Kinde.Api directory
 */
function findCSharpFiles(dir) {
    const files = [];
    
    function traverse(currentDir) {
        const items = fs.readdirSync(currentDir);
        
        for (const item of items) {
            const fullPath = path.join(currentDir, item);
            const stat = fs.statSync(fullPath);
            
            if (stat.isDirectory()) {
                traverse(fullPath);
            } else if (item.endsWith('.cs')) {
                files.push(fullPath);
            }
        }
    }
    
    traverse(dir);
    return files;
}

/**
 * Main function
 */
function main() {
    log('🔧 .NET Standard 2.1 Compatibility Fix Script', colors.cyan);
    log('================================================', colors.cyan);
    
    const kindeApiDir = path.join(__dirname, 'Kinde.Api');
    
    if (!fs.existsSync(kindeApiDir)) {
        logError('Kinde.Api directory not found!');
        process.exit(1);
    }
    
    logInfo('Scanning for C# files...');
    const csharpFiles = findCSharpFiles(kindeApiDir);
    logInfo(`Found ${csharpFiles.length} C# files`);
    
    let processedCount = 0;
    
    for (const file of csharpFiles) {
        if (processFile(file)) {
            processedCount++;
        }
    }
    
    logSuccess(`\n✅ Processing complete!`);
    logInfo(`📊 Processed ${processedCount} files`);
    
    if (processedCount > 0) {
        logInfo('🔧 Applied .NET Standard 2.1 compatibility fixes');
    } else {
        logInfo('ℹ️  No files required compatibility fixes');
    }
}

// Run the script
if (require.main === module) {
    main();
}

module.exports = {
    fixCertificateData,
    fixReadAsStringAsync,
    fixDateOnlyJsonConverter,
    fixDateOnlyNullableJsonConverter,
    fixHostConfiguration,
    fixClientUtils,
    fixRateLimitProvider,
    processFile
};