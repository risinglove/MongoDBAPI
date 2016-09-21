using API.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonDotNetValueProviderFactory());
            GlobalConfiguration.Configuration.Filters.Add(new WebApiErrorHandleAttribute());

            GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonpMediaTypeFormatter());  //自定义支持JSONP的方法
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var req = HttpContext.Current.Request;
            if (req.HttpMethod == "OPTIONS")//过滤options请求，用于js跨域
            {
                Response.StatusCode = 200;
                Response.SubStatusCode = 200;
                Response.End();
            }
        }

    }
}
