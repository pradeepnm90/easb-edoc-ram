using Markel.Pricing.Service.Runtime.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using System.Web.Http;

// FYI: http://stackoverflow.com/questions/22405584/owin-startup-not-working
[assembly: OwinStartup(typeof(Markel.Pricing.Service.Runtime.Startup))]
namespace Markel.Pricing.Service.Runtime
{
    public class Startup
    {
        private static string TIMEOUT_HOURS = ConfigurationManager.AppSettings["TimeoutHours"];
        private static string TIMEOUT_MINUTES = ConfigurationManager.AppSettings["TimeoutMinutes"];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            int timeoutHours = 0;
            int timeoutMinutes = 0;
            if (!int.TryParse(TIMEOUT_HOURS, out timeoutHours) && !int.TryParse(TIMEOUT_MINUTES, out timeoutMinutes))
            {
                // Default to one day
                timeoutHours = 24;
            }
            TimeSpan timeout = TimeSpan.FromHours(timeoutHours) + TimeSpan.FromMinutes(timeoutMinutes);

            // Token Generation
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = timeout,
                AuthorizationCodeExpireTimeSpan = timeout,
                Provider = new PricingAuthProvider()
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.Use(async (context, next) =>
            {
                IOwinRequest req = context.Request;
                IOwinResponse res = context.Response;
                if (req.Path.StartsWithSegments(new PathString("/token")))
                {
                    res.Headers.Set("Access-Control-Allow-Origin", "*");

                    if (req.Method == "OPTIONS")
                    {
                        res.StatusCode = 200;
                        res.Headers.AppendCommaSeparatedValues("Access-Control-Allow-Methods", "GET", "POST");
                        res.Headers.AppendCommaSeparatedValues("Access-Control-Allow-Headers", "authorization", "content-type");
                        return;
                    }
                }
                await next();
            });
        }
    }
}