using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Notifications;
using Markel.Pricing.Service.Infrastructure.Providers;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

// FYI: http://stackoverflow.com/questions/22405584/owin-startup-not-working
[assembly: OwinStartup(typeof(Markel.Pricing.Service.Infrastructure.Startup))]
namespace Markel.Pricing.Service.Infrastructure
{
    public class Startup
    {
        public virtual void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            // Add SignalR to the OWIN pipeline
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new UnityHubActivator(UnityConfig.GetConfiguredContainer()));
            GlobalHost.HubPipeline.AddModule(new HubAuthorizationHandler());
            app.MapSignalR(new HubConfiguration() { EnableDetailedErrors = false }).UseCors(CorsOptions.AllowAll);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            TimeSpan accessTokenLifetime = MarkelConfiguration.AccessTokenLifetime;

            // Token Generation
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = MarkelConfiguration.IsDebugMode,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = accessTokenLifetime,
                AuthorizationCodeExpireTimeSpan = accessTokenLifetime,
                Provider = new AuthProvider()
            });
            app.UseOAuthBearerAuthentication(AuthProvider.BearerOptions);
        }
    }
}