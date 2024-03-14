using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Dekra.Todo.Api.Infrastructure.Config.Authentication
{
    public static class AuthenticationConfig
    {
        public static HttpMessageHandler? BackChannelHandler { get; set; }

        public static void AddAuthenticationConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.Authority = config[Object.SettingConst.IDPConst.Domain];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudiences = config.GetSection(Object.SettingConst.IDPConst.ValidAudiences).Get<string[]>(),
                };
            });
        }
    }
}
