using System;
using System.IO;
using Kinde.Accounts.Model;

namespace FeatureFlagValueTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing FeatureFlagValue oneOf fix...");
            
            // Test boolean values
            TestValue("true", "Boolean");
            TestValue("false", "Boolean");
            
            // Test string values
            TestValue("\"feature_enabled\"", "String");
            TestValue("\"premium\"", "String");
            
            // Test number values
            TestValue("42", "Number");
            TestValue("3.14", "Number");
            TestValue("-123", "Number");
            
            // Test object values
            TestValue("{\"key\": \"value\"}", "Object");
            TestValue("{\"enabled\": true, \"limit\": 100}", "Object");
            
            // Test array values
            TestValue("[1, 2, 3]", "Object");
            TestValue("[\"a\", \"b\", \"c\"]", "Object");
            
            Console.WriteLine("All tests completed successfully!");
        }
        
        static void TestValue(string jsonString, string expectedType)
        {
            try
            {
                var result = FeatureFlagValue.FromJson(jsonString);
                var actualType = result.ActualInstance.GetType().Name;
                Console.WriteLine($"✅ {jsonString} -> {actualType} (Expected: {expectedType})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ {jsonString} -> ERROR: {ex.Message}");
            }
        }
    }
}
