using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;


[assembly: OwinStartup(typeof(Markel.GlobalRe.WebServer.App_Start.Startup))]

namespace Markel.GlobalRe.WebServer.App_Start
{
    /// <summary>
    /// This OWIN startup class creates 2 services: one File Server to serve files from "./ng" subfolder,
    /// and a Web API service implementing calls for user authentication.
    /// File Server is using "anonymous" authentication, and Web API is using Windows authentication, so web server hosting this
    /// needs to allow both Anonymous and Windows.
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            //add hosting of Web API
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            //add File Server for hosting Angular app

            // to have all requests to be server through OWIN pipeline add this to web.config:
            //<configuration><system.webServer><handlers><add name="Owin" verb="*" path="*" type="Microsoft.Owin.Host.SystemWeb.OwinHttpHandler, Microsoft.Owin.Host.SystemWeb"/>

            var fso = new FileServerOptions();
            fso.RequestPath = new PathString("/ng"); // to serve Angular app from non-root URL "ng build --base-href /ng/" can be used
            fso.FileSystem = new PhysicalFileSystem(@".\ng"); // can come from config or pre-defined subfolder
            fso.StaticFileOptions.ServeUnknownFileTypes = true;
            fso.EnableDirectoryBrowsing = true;

            app.UseFileServer(fso);
        }
    }
}
