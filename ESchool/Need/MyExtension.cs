using ElmahCore;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace System
{
    public static class MyExtension
    {
        public static void ToSaveElmah(this System.Exception exception)
        {
            /*MyHttpContext.Current.RiseError(exception);*/
        }

       
    }
}