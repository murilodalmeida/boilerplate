#pragma warning disable CS9113 // Parameter is unread.
using Xunit.Sdk;

namespace FwksLabs.Libs.Tests.Common.Attributes;

[TraitDiscoverer("Xunit.Sdk.TraitDiscoverer", "xunit.core")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class IntegrationTestAttribute(string Name = "Category", string Value = "Integration") : Attribute, ITraitAttribute;