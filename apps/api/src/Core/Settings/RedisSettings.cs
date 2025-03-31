using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Core.Options;

namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record RedisSettings : ConnectionStringOptions
{
    public override string BuildConnectionString() => this.BuildRedisConnectionString();
}