namespace Dekra.Todo.Api.Infrastructure.Config.Authentication.Object
{
    public static class SettingConst
    {
        public const string AppSettingFileName = "appsettings";
        public const string AppSettingFileNameFullExtension = AppSettingFileName + ".json";

        public static class CorsConst
        {
            public const string AllowOrigins = "CORS:AllowOrigins";
        }

        public static class ConnectionStringsConst
        {
            public const string Database = "ConnectionStrings:Database";
        }

        public static class IDPConst
        {
            private const string IDP = "IDP";

            public const string Domain = IDP + ":Domain";
            public const string ValidAudiences = IDP + ":ValidAudiences";
        }
    }
}
