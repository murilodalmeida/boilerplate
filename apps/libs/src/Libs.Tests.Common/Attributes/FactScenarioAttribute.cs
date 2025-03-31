using Xunit;

namespace FwksLabs.Libs.Tests.Common.Attributes;

public sealed class FactScenarioAttribute : FactAttribute
{
    public FactScenarioAttribute(string target, string description) =>
        DisplayName = $"\"{target}\", {description}";
}
