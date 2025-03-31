using FwksLabs.Libs.Core.Attributes;
using FwksLabs.Libs.Tests.Common.Attributes;

namespace FwksLabs.Libs.Core.Tests.Attributes;

[Trait("Category", "Unit")]
[Trait("Group", "Attributes")]
public class EnumMetadataAttributeTests
{
    [FactScenario("Symbol Property", "Should set and get correctly.")]
    public void SymbolProperty_GetSet()
    {
        // Arrange
        var attribute = new EnumMetadataAttribute();
        var expectedSymbol = "TestSymbol";

        // Act
        attribute.Symbol = expectedSymbol;
        var actualSymbol = attribute.Symbol;

        // Assert
        Assert.Equal(expectedSymbol, actualSymbol);
    }

    [FactScenario("Description Property", "Should set and get correctly.")]
    public void DescriptionProperty_GetSet()
    {
        // Arrange
        var attribute = new EnumMetadataAttribute();
        var expectedDescription = "TestDescription";

        // Act
        attribute.Description = expectedDescription;
        var actualDescription = attribute.Description;

        // Assert
        Assert.Equal(expectedDescription, actualDescription);
    }
}
