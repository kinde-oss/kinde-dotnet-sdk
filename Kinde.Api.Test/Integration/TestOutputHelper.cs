using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace Kinde.Api.Test.Integration
{
    /// <summary>
    /// Helper class for formatting test output with detailed API response information
    /// </summary>
    public static class TestOutputHelper
    {
        /// <summary>
        /// Formats and outputs detailed information about an API response
        /// </summary>
        public static void WriteResponseDetails<T>(ITestOutputHelper output, string testName, T result) where T : class
        {
            output.WriteLine("");
            output.WriteLine($"═══════════════════════════════════════════════════════════════");
            output.WriteLine($"Test: {testName}");
            output.WriteLine($"═══════════════════════════════════════════════════════════════");
            
            try
            {
                // Serialize to JSON for display
                // Get converters using reflection (JsonConverterRegistry is internal)
                var apiClientType = typeof(Kinde.Api.Client.ApiClient);
                var helperType = apiClientType.Assembly.GetType("Kinde.Api.Client.JsonConverterHelper");
                IList<JsonConverter> converters;
                if (helperType != null)
                {
                    var method = helperType.GetMethod("CreateStandardConverters",
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    if (method != null)
                    {
                        converters = (IList<JsonConverter>)method.Invoke(null, null)!;
                    }
                    else
                    {
                        converters = new List<JsonConverter>();
                    }
                }
                else
                {
                    converters = new List<JsonConverter>();
                }
                
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Include,
                    Converters = converters
                };
                
                var json = JsonConvert.SerializeObject(result, settings);
                var jsonObj = JObject.Parse(json);
                
                // Display summary
                output.WriteLine($"Response Type: {typeof(T).Name}");
                output.WriteLine($"");
                
                // Display key properties
                output.WriteLine("Key Properties:");
                DisplayProperties(output, jsonObj, 0, maxDepth: 2);
                
                // Display full JSON (truncated if too long)
                output.WriteLine("");
                output.WriteLine("Full Response JSON:");
                if (json.Length > 2000)
                {
                    output.WriteLine(json.Substring(0, 2000) + "... (truncated)");
                    output.WriteLine($"Total length: {json.Length} characters");
                }
                else
                {
                    output.WriteLine(json);
                }
                
                output.WriteLine($"═══════════════════════════════════════════════════════════════");
                output.WriteLine($"✓ {testName}: Success");
                output.WriteLine("");
            }
            catch (Exception ex)
            {
                output.WriteLine($"Error formatting response: {ex.Message}");
                output.WriteLine($"Response Type: {typeof(T).Name}");
                output.WriteLine($"Response: {result?.ToString() ?? "null"}");
            }
        }

        private static void DisplayProperties(ITestOutputHelper output, JToken token, int depth, int maxDepth)
        {
            if (depth > maxDepth) return;
            
            var indent = new string(' ', depth * 2);
            
            if (token is JObject obj)
            {
                foreach (var prop in obj.Properties().Take(10)) // Limit to first 10 properties
                {
                    if (prop.Value is JObject || prop.Value is JArray)
                    {
                        output.WriteLine($"{indent}{prop.Name}: {{...}}");
                        if (depth < maxDepth)
                        {
                            DisplayProperties(output, prop.Value, depth + 1, maxDepth);
                        }
                    }
                    else
                    {
                        var value = prop.Value?.ToString() ?? "null";
                        if (value.Length > 100)
                        {
                            value = value.Substring(0, 100) + "...";
                        }
                        output.WriteLine($"{indent}{prop.Name}: {value}");
                    }
                }
                
                if (obj.Properties().Count() > 10)
                {
                    output.WriteLine($"{indent}... ({obj.Properties().Count() - 10} more properties)");
                }
            }
            else if (token is JArray array)
            {
                output.WriteLine($"{indent}[Array with {array.Count} items]");
                if (array.Count > 0 && depth < maxDepth)
                {
                    output.WriteLine($"{indent}  First item:");
                    DisplayProperties(output, array[0], depth + 1, maxDepth);
                }
            }
        }

        /// <summary>
        /// Formats error output
        /// </summary>
        public static void WriteError(ITestOutputHelper output, string testName, Exception ex)
        {
            output.WriteLine("");
            output.WriteLine($"═══════════════════════════════════════════════════════════════");
            output.WriteLine($"✗ {testName}: FAILED");
            output.WriteLine($"═══════════════════════════════════════════════════════════════");
            output.WriteLine($"Error: {ex.Message}");
            output.WriteLine($"Type: {ex.GetType().Name}");
            if (ex.InnerException != null)
            {
                output.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            output.WriteLine($"Stack Trace:");
            output.WriteLine(ex.StackTrace);
            output.WriteLine($"═══════════════════════════════════════════════════════════════");
            output.WriteLine("");
        }

        /// <summary>
        /// Formats serialization round-trip test results
        /// </summary>
        public static void WriteSerializationTest(ITestOutputHelper output, string testName, int jsonLength, bool success)
        {
            if (success)
            {
                output.WriteLine($"✓ {testName}: Serialization round-trip successful ({jsonLength} bytes)");
            }
            else
            {
                output.WriteLine($"✗ {testName}: Serialization round-trip failed");
            }
        }
    }
}

