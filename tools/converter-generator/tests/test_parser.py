"""
Unit tests for CSharpModelParser

Tests based on production Kinde requirements:
- Must extract Option<> properties correctly
- Must extract required (non-Option) parameters
- Must handle nested generics (List<>, Dictionary<>)
- Must escape C# reserved keywords
- Must find JSON property names from [JsonPropertyName] attributes
- Must handle nullable types correctly
"""
import pytest
from pathlib import Path
import tempfile
import os

import sys
from pathlib import Path

# Add parent directory to path (tools/converter-generator)
parent_dir = Path(__file__).parent.parent
sys.path.insert(0, str(parent_dir))

from generate_converters import CSharpModelParser, PropertyInfo, ModelInfo


class TestCSharpModelParser:
    """Test CSharpModelParser with production-like C# models"""
    
    def setup_method(self):
        """Set up test fixtures"""
        self.parser = CSharpModelParser()
    
    def test_parse_simple_response_model(self):
        """Test parsing a simple response model with Option<> properties"""
        model_content = """
namespace Kinde.Api.Model
{
    public partial class GetSimpleResponse
    {
        [JsonConstructor]
        public GetSimpleResponse(Option<string?> code = default, Option<string?> message = default)
        {
            CodeOption = code;
            MessageOption = message;
            OnCreated();
        }

        [JsonIgnore]
        public Option<string?> CodeOption { get; private set; }

        [JsonPropertyName("code")]
        public string? Code { get { return this.CodeOption; } set { this.CodeOption = new Option<string?>(value); } }

        [JsonIgnore]
        public Option<string?> MessageOption { get; private set; }

        [JsonPropertyName("message")]
        public string? Message { get { return this.MessageOption; } set { this.MessageOption = new Option<string?>(value); } }
    }
}
"""
        with tempfile.NamedTemporaryFile(mode='w', suffix='.cs', delete=False) as f:
            f.write(model_content)
            f.flush()
            model_path = Path(f.name)
        
        try:
            model_info = self.parser.parse_model(model_path)
            
            assert model_info is not None
            assert model_info.name == "GetSimpleResponse"
            assert model_info.namespace == "Kinde.Api.Model"
            assert len(model_info.properties) == 2
            assert len(model_info.required_params) == 0
            
            # Check first property
            code_prop = next((p for p in model_info.properties if p.name == "code"), None)
            assert code_prop is not None
            assert code_prop.json_name == "code"
            assert code_prop.csharp_type == "string"
            assert code_prop.is_option is True
            assert code_prop.is_nullable is True
            assert code_prop.pascal_name == "Code"
            
            # Check second property
            message_prop = next((p for p in model_info.properties if p.name == "message"), None)
            assert message_prop is not None
            assert message_prop.json_name == "message"
            assert message_prop.csharp_type == "string"
            assert message_prop.is_option is True
        finally:
            os.unlink(model_path)
    
    def test_parse_model_with_required_params(self):
        """Test parsing a model with required (non-Option) parameters"""
        model_content = """
namespace Kinde.Api.Model
{
    public partial class CreateSimpleRequest
    {
        [JsonConstructor]
        public CreateSimpleRequest(string name, Option<string?> description = default)
        {
            Name = name;
            DescriptionOption = description;
            OnCreated();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public Option<string?> DescriptionOption { get; private set; }

        [JsonPropertyName("description")]
        public string? Description { get { return this.DescriptionOption; } set { this.DescriptionOption = new Option<string?>(value); } }
    }
}
"""
        with tempfile.NamedTemporaryFile(mode='w', suffix='.cs', delete=False) as f:
            f.write(model_content)
            f.flush()
            model_path = Path(f.name)
        
        try:
            model_info = self.parser.parse_model(model_path)
            
            assert model_info is not None
            assert model_info.name == "CreateSimpleRequest"
            assert len(model_info.properties) == 1
            assert len(model_info.required_params) == 1
            
            # Check required parameter
            name_param = model_info.required_params[0]
            assert name_param.name == "name"
            assert name_param.json_name == "name"
            assert name_param.csharp_type == "string"
            assert name_param.is_option is False
            assert name_param.is_required is True
            
            # Check Option property
            desc_prop = model_info.properties[0]
            assert desc_prop.name == "description"
            assert desc_prop.is_option is True
        finally:
            os.unlink(model_path)
    
    def test_parse_model_with_list_property(self):
        """Test parsing a model with List<> property"""
        model_content = """
namespace Kinde.Api.Model
{
    public partial class GetListResponse
    {
        [JsonConstructor]
        public GetListResponse(Option<List<string>?> items = default)
        {
            ItemsOption = items;
            OnCreated();
        }

        [JsonIgnore]
        public Option<List<string>?> ItemsOption { get; private set; }

        [JsonPropertyName("items")]
        public List<string>? Items { get { return this.ItemsOption; } set { this.ItemsOption = new Option<List<string>?>(value); } }
    }
}
"""
        with tempfile.NamedTemporaryFile(mode='w', suffix='.cs', delete=False) as f:
            f.write(model_content)
            f.flush()
            model_path = Path(f.name)
        
        try:
            model_info = self.parser.parse_model(model_path)
            
            assert model_info is not None
            assert len(model_info.properties) == 1
            
            items_prop = model_info.properties[0]
            assert items_prop.name == "items"
            assert items_prop.csharp_type == "List<string>"
            assert items_prop.is_list is True
            # Note: List<string>? is nullable, but the parser extracts List<string> as the type
            # The nullable is part of Option<List<string>?>, not the List itself
        finally:
            os.unlink(model_path)
    
    def test_parse_model_with_enum_property(self):
        """Test parsing a model with enum property"""
        model_content = """
namespace Kinde.Api.Model
{
    public partial class GetEnumResponse
    {
        [JsonConstructor]
        public GetEnumResponse(Option<GetEnumResponse.TypeEnum?> type = default)
        {
            TypeOption = type;
            OnCreated();
        }

        [JsonIgnore]
        public Option<GetEnumResponse.TypeEnum?> TypeOption { get; private set; }

        [JsonPropertyName("type")]
        public GetEnumResponse.TypeEnum? Type { get { return this.TypeOption; } set { this.TypeOption = new Option<GetEnumResponse.TypeEnum?>(value); } }
    }
}
"""
        with tempfile.NamedTemporaryFile(mode='w', suffix='.cs', delete=False) as f:
            f.write(model_content)
            f.flush()
            model_path = Path(f.name)
        
        try:
            model_info = self.parser.parse_model(model_path)
            
            assert model_info is not None
            assert len(model_info.properties) == 1
            
            type_prop = model_info.properties[0]
            assert type_prop.name == "type"
            assert type_prop.is_enum is True
            # The parser extracts the enum type from the full type name
            # GetEnumResponse.TypeEnum becomes "GetEnum" (model name prefix)
            assert "Enum" in type_prop.enum_type or type_prop.enum_type == "GetEnum"
        finally:
            os.unlink(model_path)
    
    def test_parse_model_with_reserved_keyword(self):
        """Test parsing a model with C# reserved keyword (event)"""
        model_content = """
namespace Kinde.Api.Model
{
    public partial class GetEventResponse
    {
        [JsonConstructor]
        public GetEventResponse(Option<GetEventResponseEvent?> @event = default)
        {
            EventOption = @event;
            OnCreated();
        }

        [JsonIgnore]
        public Option<GetEventResponseEvent?> EventOption { get; private set; }

        [JsonPropertyName("event")]
        public GetEventResponseEvent? Event { get { return this.EventOption; } set { this.EventOption = new Option<GetEventResponseEvent?>(value); } }
    }
}
"""
        with tempfile.NamedTemporaryFile(mode='w', suffix='.cs', delete=False) as f:
            f.write(model_content)
            f.flush()
            model_path = Path(f.name)
        
        try:
            model_info = self.parser.parse_model(model_path)
            
            assert model_info is not None
            assert len(model_info.properties) == 1
            
            event_prop = model_info.properties[0]
            # Should escape reserved keyword
            assert event_prop.name == "@event"
            assert event_prop.json_name == "event"
            assert event_prop.pascal_name == "Event"
        finally:
            os.unlink(model_path)
    
    def test_parse_model_with_dictionary_property(self):
        """Test parsing a model with Dictionary<> property"""
        model_content = """
namespace Kinde.Api.Model
{
    public partial class CreateOrgRequest
    {
        [JsonConstructor]
        public CreateOrgRequest(string name, Option<Dictionary<string, CreateOrgRequest.InnerEnum>?> featureFlags = default)
        {
            Name = name;
            FeatureFlagsOption = featureFlags;
            OnCreated();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public Option<Dictionary<string, CreateOrgRequest.InnerEnum>?> FeatureFlagsOption { get; private set; }

        [JsonPropertyName("feature_flags")]
        public Dictionary<string, CreateOrgRequest.InnerEnum>? FeatureFlags { get { return this.FeatureFlagsOption; } set { this.FeatureFlagsOption = new Option<Dictionary<string, CreateOrgRequest.InnerEnum>?>(value); } }
    }
}
"""
        with tempfile.NamedTemporaryFile(mode='w', suffix='.cs', delete=False) as f:
            f.write(model_content)
            f.flush()
            model_path = Path(f.name)
        
        try:
            model_info = self.parser.parse_model(model_path)
            
            assert model_info is not None
            assert len(model_info.required_params) == 1
            assert len(model_info.properties) == 1
            
            # Check required parameter
            name_param = model_info.required_params[0]
            assert name_param.name == "name"
            assert name_param.csharp_type == "string"
            
            # Check Dictionary property
            flags_prop = model_info.properties[0]
            assert flags_prop.name == "featureFlags"
            assert flags_prop.json_name == "feature_flags"
            assert flags_prop.csharp_type == "Dictionary<string, CreateOrgRequest.InnerEnum>"
            assert flags_prop.is_dictionary is True
        finally:
            os.unlink(model_path)
    
    def test_parse_model_with_snake_case_json_name(self):
        """Test parsing a model with snake_case JSON property names"""
        model_content = """
namespace Kinde.Api.Model
{
    public partial class GetResponse
    {
        [JsonConstructor]
        public GetResponse(Option<string?> nextToken = default)
        {
            NextTokenOption = nextToken;
            OnCreated();
        }

        [JsonIgnore]
        public Option<string?> NextTokenOption { get; private set; }

        [JsonPropertyName("next_token")]
        public string? NextToken { get { return this.NextTokenOption; } set { this.NextTokenOption = new Option<string?>(value); } }
    }
}
"""
        with tempfile.NamedTemporaryFile(mode='w', suffix='.cs', delete=False) as f:
            f.write(model_content)
            f.flush()
            model_path = Path(f.name)
        
        try:
            model_info = self.parser.parse_model(model_path)
            
            assert model_info is not None
            assert len(model_info.properties) == 1
            
            prop = model_info.properties[0]
            assert prop.name == "nextToken"
            assert prop.json_name == "next_token"  # Should preserve snake_case
            assert prop.pascal_name == "NextToken"
        finally:
            os.unlink(model_path)
    
    def test_parse_model_with_nested_inner_model(self):
        """Test parsing a model with nested Inner model property"""
        model_content = """
namespace Kinde.Api.Model
{
    public partial class GetApisResponse
    {
        [JsonConstructor]
        public GetApisResponse(Option<List<GetApisResponseApisInner>?> apis = default)
        {
            ApisOption = apis;
            OnCreated();
        }

        [JsonIgnore]
        public Option<List<GetApisResponseApisInner>?> ApisOption { get; private set; }

        [JsonPropertyName("apis")]
        public List<GetApisResponseApisInner>? Apis { get { return this.ApisOption; } set { this.ApisOption = new Option<List<GetApisResponseApisInner>?>(value); } }
    }
}
"""
        with tempfile.NamedTemporaryFile(mode='w', suffix='.cs', delete=False) as f:
            f.write(model_content)
            f.flush()
            model_path = Path(f.name)
        
        try:
            model_info = self.parser.parse_model(model_path)
            
            assert model_info is not None
            assert len(model_info.properties) == 1
            
            apis_prop = model_info.properties[0]
            assert apis_prop.name == "apis"
            assert apis_prop.csharp_type == "List<GetApisResponseApisInner>"
            assert apis_prop.is_list is True
            # Note: List<GetApisResponseApisInner>? is nullable, but the parser extracts List<...> as the type
            # The nullable is part of Option<List<...>?>, not the List itself
        finally:
            os.unlink(model_path)
    
    def test_split_parameters_with_nested_generics(self):
        """Test parameter splitting with nested generics"""
        params_str = "Option<string?> code = default, Option<List<GetApisResponseApisInner>?> apis = default, Option<Dictionary<string, CreateOrgRequest.InnerEnum>?> flags = default"
        
        params = self.parser._split_parameters(params_str)
        
        assert len(params) == 3
        assert "Option<string?> code = default" in params[0]
        assert "Option<List<GetApisResponseApisInner>?> apis = default" in params[1]
        assert "Option<Dictionary<string, CreateOrgRequest.InnerEnum>?> flags = default" in params[2]
    
    def test_find_json_property_name(self):
        """Test finding JSON property name from [JsonPropertyName] attribute"""
        content = """
        [JsonPropertyName("next_token")]
        public string? NextToken { get { return this.NextTokenOption; } set { this.NextTokenOption = new Option<string?>(value); } }
        """
        
        json_name = self.parser._find_json_property_name("nextToken", content)
        assert json_name == "next_token"
    
    def test_find_json_property_name_fallback(self):
        """Test fallback to snake_case conversion when [JsonPropertyName] not found"""
        content = """
        public string? SomeProperty { get; set; }
        """
        
        json_name = self.parser._find_json_property_name("someProperty", content)
        assert json_name == "some_property"

