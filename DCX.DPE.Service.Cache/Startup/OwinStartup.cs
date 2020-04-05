using Microsoft.Owin;
using Owin;
using System.Net;
using System.Web.Http;
using System.Web.UI.WebControls;
using Unity.WebApi;

[assembly: OwinStartup(typeof(DCX.DPE.Service.Cache.OwinStartup))]

namespace DCX.DPE.Service.Cache
{
    /// <summary>
    /// Einstiegspunkt für die WebApi
    /// </summary>
    internal class OwinStartup
    {
        /// <summary>
        /// Configurations the specified application builder.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpListener listener = (HttpListener)appBuilder.Properties["System.Net.HttpListener"];
            //listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "DPE/{controller}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );

            //config.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());

            appBuilder.UseWebApi(config);
        }
    }
}