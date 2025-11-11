"""
Unit tests for ConverterGenerator

Tests based on production Kinde requirements:
- Must generate valid C# code
- Must handle all property types correctly
- Must generate proper Option<> wrapping
- Must use PascalCase for property access in WriteJson
- Must use camelCase for constructor parameters in ReadJson
- Must escape C# reserved keywords
"""
import pytest
from pathlib import Path
import tempfile
import os
import re

import sys
from pathlib import Path

# Add parent directory to path (tools/converter-generator)
parent_dir = Path(__file__).parent.parent
sys.path.insert(0, str(parent_dir))

from generate_converters import (
    CSharpModelParser,
    ConverterGenerator,
    PropertyInfo,
    ModelInfo
)


class TestConverterGenerator:
    """Test ConverterGenerator with production-like models"""
    
    def setup_method(self):
        """Set up test fixtures"""
        self.parser = CSharpModelParser()
        template_dir = Path(__file__).parent.parent / "templates"
        self.generator = ConverterGenerator(template_dir)
    
    def test_generate_simple_converter(self):
        """Test generating a simple converter for a model with Option<> properties"""
        model_info = ModelInfo(
            name="GetSimpleResponse",
            namespace="Kinde.Api.Model",
            properties=[
                PropertyInfo(
                    name="code",
                    pascal_name="Code",
                    json_name="code",
                    csharp_type="string",
                    is_option=True,
                    is_nullable=True,
                    is_required=False,
                    enum_type=None
                ),
                PropertyInfo(
                    name="message",
                    pascal_name="Message",
                    json_name="message",
                    csharp_type="string",
                    is_option=True,
                    is_nullable=True,
                    is_required=False,
                    enum_type=None
                )
            ],
            required_params=[]
        )
        
        converter_code = self.generator.generate(model_info)
        
        # Check basic structure
        assert "public class GetSimpleResponseNewtonsoftConverter" in converter_code
        assert "ReadJson" in converter_code
        assert "WriteJson" in converter_code
        
        # Check ReadJson
        assert 'string? code = default(string?);' in converter_code
        assert 'jsonObject["code"]' in converter_code
        assert 'new Option<string?>(code)' in converter_code
        
        # Check WriteJson
        assert 'value.CodeOption.IsSet' in converter_code
        assert 'value.Code' in converter_code
        assert 'writer.WritePropertyName("code")' in converter_code
    
    def test_generate_converter_with_required_params(self):
        """Test generating a converter with required (non-Option) parameters"""
        model_info = ModelInfo(
            name="CreateSimpleRequest",
            namespace="Kinde.Api.Model",
            properties=[
                PropertyInfo(
                    name="description",
                    pascal_name="Description",
                    json_name="description",
                    csharp_type="string",
                    is_option=True,
                    is_nullable=True,
                    is_required=False,
                    enum_type=None
                )
            ],
            required_params=[
                PropertyInfo(
                    name="name",
                    pascal_name="Name",
                    json_name="name",
                    csharp_type="string",
                    is_option=False,
                    is_nullable=False,
                    is_required=True,
                    enum_type=None
                )
            ]
        )
        
        converter_code = self.generator.generate(model_info)
        
        # Check required parameter in ReadJson
        assert 'string name = default(string);' in converter_code
        assert 'jsonObject["name"]' in converter_code
        
        # Check required parameter in constructor
        assert 'name: name' in converter_code
        
        # Check Option property
        assert 'new Option<string?>(description)' in converter_code
    
    def test_generate_converter_with_enum(self):
        """Test generating a converter with enum property"""
        model_info = ModelInfo(
            name="GetEnumResponse",
            namespace="Kinde.Api.Model",
            properties=[
                PropertyInfo(
                    name="type",
                    pascal_name="Type",
                    json_name="type",
                    csharp_type="GetEnumResponse.TypeEnum",
                    is_option=True,
                    is_nullable=True,
                    is_required=False,
                    enum_type="TypeEnum"
                )
            ],
            required_params=[]
        )
        
        converter_code = self.generator.generate(model_info)
        
        # Check enum handling in ReadJson
        assert 'GetEnumResponse.TypeEnum?' in converter_code
        assert 'TypeEnumFromString' in converter_code
        
        # Check enum handling in WriteJson
        assert 'TypeEnumToJsonValue' in converter_code
        assert 'value.Type' in converter_code
    
    def test_generate_converter_with_list(self):
        """Test generating a converter with List<> property"""
        model_info = ModelInfo(
            name="GetListResponse",
            namespace="Kinde.Api.Model",
            properties=[
                PropertyInfo(
                    name="items",
                    pascal_name="Items",
                    json_name="items",
                    csharp_type="List<string>",
                    is_option=True,
                    is_nullable=True,
                    is_required=False,
                    enum_type=None
                )
            ],
            required_params=[]
        )
        
        converter_code = self.generator.generate(model_info)
        
        # Check List handling
        assert 'List<string>? items = default(List<string>?);' in converter_code
        assert 'ToObject<List<string>?>(serializer)' in converter_code
        assert 'new Option<List<string>?>(items)' in converter_code
    
    def test_generate_converter_with_dictionary(self):
        """Test generating a converter with Dictionary<> property"""
        model_info = ModelInfo(
            name="CreateOrgRequest",
            namespace="Kinde.Api.Model",
            properties=[
                PropertyInfo(
                    name="featureFlags",
                    pascal_name="FeatureFlags",
                    json_name="feature_flags",
                    csharp_type="Dictionary<string, CreateOrgRequest.InnerEnum>",
                    is_option=True,
                    is_nullable=True,
                    is_required=False,
                    enum_type=None
                )
            ],
            required_params=[]
        )
        
        converter_code = self.generator.generate(model_info)
        
        # Check Dictionary handling
        assert 'Dictionary<string, CreateOrgRequest.InnerEnum>?' in converter_code
        # The generator may use ToObject with serializer or direct deserialization
        assert 'Dictionary<string, CreateOrgRequest.InnerEnum>' in converter_code
        assert 'new Option<Dictionary<string, CreateOrgRequest.InnerEnum>?>(featureFlags)' in converter_code
    
    def test_generate_converter_with_reserved_keyword(self):
        """Test generating a converter with C# reserved keyword (event)"""
        model_info = ModelInfo(
            name="GetEventResponse",
            namespace="Kinde.Api.Model",
            properties=[
                PropertyInfo(
                    name="@event",
                    pascal_name="Event",
                    json_name="event",
                    csharp_type="GetEventResponseEvent",
                    is_option=True,
                    is_nullable=True,
                    is_required=False,
                    enum_type=None
                )
            ],
            required_params=[]
        )
        
        converter_code = self.generator.generate(model_info)
        
        # Check reserved keyword escaping
        assert '@event = default' in converter_code
        assert '@event: @event' in converter_code
        assert 'value.Event' in converter_code  # PascalCase property name
    
    def test_generate_converter_with_snake_case_json_name(self):
        """Test generating a converter with snake_case JSON property name"""
        model_info = ModelInfo(
            name="GetResponse",
            namespace="Kinde.Api.Model",
            properties=[
                PropertyInfo(
                    name="nextToken",
                    pascal_name="NextToken",
                    json_name="next_token",
                    csharp_type="string",
                    is_option=True,
                    is_nullable=True,
                    is_required=False,
                    enum_type=None
                )
            ],
            required_params=[]
        )
        
        converter_code = self.generator.generate(model_info)
        
        # Check snake_case JSON name is preserved
        assert 'jsonObject["next_token"]' in converter_code
        assert 'writer.WritePropertyName("next_token")' in converter_code
        assert 'value.NextToken' in converter_code  # PascalCase property access
    
    def test_generate_converter_code_structure(self):
        """Test that generated converter has correct C# structure"""
        model_info = ModelInfo(
            name="TestResponse",
            namespace="Kinde.Api.Model",
            properties=[],
            required_params=[]
        )
        
        converter_code = self.generator.generate(model_info)
        
        # Check required using statements
        assert "using System;" in converter_code
        assert "using System.Collections.Generic;" in converter_code
        assert "using Newtonsoft.Json;" in converter_code
        assert "using Newtonsoft.Json.Linq;" in converter_code
        assert "using Kinde.Api.Model;" in converter_code
        assert "using Kinde.Api.Client;" in converter_code
        
        # Check namespace
        assert "namespace Kinde.Api.Converters" in converter_code
        
        # Check class declaration
        assert "public class TestResponseNewtonsoftConverter" in converter_code
        assert "JsonConverter<TestResponse>" in converter_code
        
        # Check methods
        assert "public override bool CanRead => true;" in converter_code
        assert "public override bool CanWrite => true;" in converter_code
        assert "public override TestResponse ReadJson" in converter_code
        assert "public override void WriteJson" in converter_code
    
    def test_generate_converter_nullable_handling(self):
        """Test that nullable types are handled correctly"""
        model_info = ModelInfo(
            name="TestResponse",
            namespace="Kinde.Api.Model",
            properties=[
                PropertyInfo(
                    name="nullableString",
                    pascal_name="NullableString",
                    json_name="nullable_string",
                    csharp_type="string",
                    is_option=True,
                    is_nullable=True,
                    is_required=False,
                    enum_type=None
                ),
                PropertyInfo(
                    name="nonNullableString",
                    pascal_name="NonNullableString",
                    json_name="non_nullable_string",
                    csharp_type="string",
                    is_option=True,
                    is_nullable=False,
                    is_required=False,
                    enum_type=None
                )
            ],
            required_params=[]
        )
        
        converter_code = self.generator.generate(model_info)
        
        # Check nullable handling
        assert 'string? nullableString = default(string?);' in converter_code
        assert 'new Option<string?>(nullableString)' in converter_code
        
        # Check non-nullable handling
        assert 'string nonNullableString = default(string);' in converter_code
        assert 'new Option<string>(nonNullableString)' in converter_code

