using System.Web.Http.Filters;
using Utility;

namespace API
{
    public sealed class WebApiErrorHandleAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            LogHelper.WriteLog("_ErrorMsg:" + actionExecutedContext.Exception, "ApplicationError");
        }
    }
}