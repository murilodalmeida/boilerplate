using System.Text.Json;
using System.Text.Json.Serialization;
using FwksLabs.Libs.Core.Configuration;
using FwksLabs.Libs.Tests.Common.Attributes;

namespace FwksLabs.Libs.Core.Tests.Configuration;

[Trait("Category", "Unit")]
[UnitTest]
public class JsonSerializerOptionsConfigurationTests
{
    [FactScenario("Default", "Should return configured options.")]
    public void Default_ShouldReturnConfiguredOptions()
    {
        // Act
        var options = JsonSerializerOptionsConfiguration.Default();

        // Assert
        Assert.NotNull(options);
        Assert.True(options.PropertyNameCaseInsensitive);
        Assert.Equal(JsonNamingPolicy.CamelCase, options.PropertyNamingPolicy);
        Assert.False(options.WriteIndented);
        Assert.Equal(ReferenceHandler.IgnoreCycles, options.ReferenceHandler);
        Assert.True(options.AllowTrailingCommas);
        Assert.Contains(options.Converters, converter => converter is JsonStringEnumConverter);
    }

    [FactScenario("Configure", "Should set options correctly.")]
    public void Configure_ShouldSetOptionsCorrectly()
    {
        // Arrange
        var options = new JsonSerializerOptions();

        // Act
        JsonSerializerOptionsConfiguration.Configure(options);

        // Assert
        Assert.True(options.PropertyNameCaseInsensitive);
        Assert.Equal(JsonNamingPolicy.CamelCase, options.PropertyNamingPolicy);
        Assert.False(options.WriteIndented);
        Assert.Equal(ReferenceHandler.IgnoreCycles, options.ReferenceHandler);
        Assert.True(options.AllowTrailingCommas);
        Assert.Contains(options.Converters, converter => converter is JsonStringEnumConverter);
    }
}
