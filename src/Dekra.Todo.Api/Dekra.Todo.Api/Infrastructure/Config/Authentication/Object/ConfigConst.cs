namespace Dekra.Todo.Api.Infrastructure.Config.Authentication.Object
{
    public static class ConfigConst
    {
        public const string EnvironmentVariable = "ASPNETCORE_ENVIRONMENT";
        public const string CorsPolicy = "CorsPolicy";

        public const string AuthorizationHeaderKey = "Authorization";

        public static class EnvironmentConst
        {
            public const string Local = "local";
            public const string Production = "production";
        }
    }
}
