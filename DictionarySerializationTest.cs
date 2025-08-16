using System;
using System.Collections;
using System.Collections.Generic;
using Kinde.Accounts;

namespace DictionarySerializationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Dictionary Serialization Issue...");
            
            // Test dictionary with collectionFormat = "multi"
            var dictionary = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };
            
            Console.WriteLine("\n=== BEFORE FIX ===");
            Console.WriteLine("Dictionary with collectionFormat = 'multi':");
            
            // This demonstrates the bug - dictionary implements ICollection
            // so it matches the first condition and gets serialized incorrectly
            var result = ClientUtils.ParameterToMultiMap("multi", "test", dictionary);
            
            foreach (var param in result)
            {
                Console.WriteLine($"  {param.Key}: {string.Join(", ", param.Value)}");
            }
            
            Console.WriteLine("\nExpected behavior:");
            Console.WriteLine("  key1: value1");
            Console.WriteLine("  key2: value2");
            
            Console.WriteLine("\nActual behavior (bug):");
            Console.WriteLine("  test: System.Collections.DictionaryEntry, System.Collections.DictionaryEntry");
            
            Console.WriteLine("\n=== AFTER FIX ===");
            Console.WriteLine("Dictionary should be handled by IDictionary branch first");
            Console.WriteLine("and produce proper key/value pairs regardless of collectionFormat");
        }
    }
}
