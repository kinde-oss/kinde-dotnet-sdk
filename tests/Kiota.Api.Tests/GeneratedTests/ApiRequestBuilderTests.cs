using Xunit;
using Kiota.Api;
using System.Threading.Tasks;
using Microsoft.Kiota.Http.HttpClientLibrary;

public class ApiRequestBuilderTests
{
    [Fact]
    public async Task ApiRequestBuilder_GetAsync_ShouldReturnSomething()
    {
        // Arrange
        var accessToken = "your access token";
        var authProvider = new StaticAccessTokenProvider(accessToken);
        var requestAdapter = new HttpClientRequestAdapter(authProvider);

        var client = new ApiClient(requestAdapter);
    
        requestAdapter.BaseUrl = "https://yourbusiness.kinde.com";
        // Act
        var result = await client.Api.V1.Apis.GetAsync(); 

        // Assert
        Assert.NotNull(result); 
        // You can also assert more specific fields from your model
        // Example: Assert.Equal("expectedValue", result.SomeProperty);
    }
    [Fact]
    public async Task ApiRequestBuilder_PostAsync_ShouldReturnSomething()
    {
        // Arrange
        // TODO: Instantiate ApiClient and call PostAsync

        // Act
        // var result = await new ApiClient(...).Api.PostAsync(new object());

        // Assert
        // TODO: Add assertions
    }
}
