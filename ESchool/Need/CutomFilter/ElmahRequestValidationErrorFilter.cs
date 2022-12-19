using ElmahCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESchool.Need.CutomFilter
{
    public class ElmahRequestValidationErrorFilter : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            /*MyErrorLog myErrorLog = new MyErrorLog();
            myErrorLog.Log(new Error(context.Exception));*/
        }
    }
}