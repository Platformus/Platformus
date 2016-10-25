using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Platformus.Security
{
    public static class SecurityBuilderExtension
    {
        private static CookieAuthenticationOptions GetOptions(string loginPath)
        {
            return new CookieAuthenticationOptions()
            {
                AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                CookieName = "PLATFORMUS",
                ExpireTimeSpan = new System.TimeSpan(1, 0, 0),
                LoginPath = new PathString(loginPath)
            };
        }
        public static IApplicationBuilder UsePlatformusCookie(this IApplicationBuilder app, Func<HttpContext, bool> condition, string path)
        {
            var builder = app.New();
            builder.UseCookieAuthentication(GetOptions(path));

            return app.Use(main =>
            {                
                builder.Run(main);
                var result = builder.Build();
                return context =>
                {
                    if (condition(context)) return result(context);
                    else return main(context);
                };
            }
            );
        }
    }
}
