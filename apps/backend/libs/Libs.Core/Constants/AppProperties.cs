namespace FwksLabs.Libs.Core.Constants;

public static class AppProperties
{
    public static class Otel
    {
        public const string ServiceName = "service.name";
        public const string ServiceNamespace = "service.namespace";
        public const string ServiceTeam = "service.team";
    }

    public static class Headers
    {
        public const string CorrelationId = "X-Correlation-Id";
    }

    public static class HealthCheck
    {
        public const string EndpointsLiveness = "/health/live";
        public const string EndpointsReadiness = "/health/ready";

        public const int TimeoutInSeconds = 10;

        public const string TagsLiveness = "liveness";
        public const string TagsReadiness = "readiness";
        public const string TagsCritical = "critical";
        public const string TagsNonCritical = "non-critical";
        public const string TagsTypeHttpService = "type-httpservice";
        public const string TagsTypeDatabase = "type-database";
    }

    public static class Errors
    {
        public const string UnreachableResource = "The requested resource is unreachable.";
    }
}