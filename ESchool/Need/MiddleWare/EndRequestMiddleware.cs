using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ESchool.Need.MiddleWare
{
    public class EndRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private static int _countRequest = 0;

        public EndRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Do tasks before other middleware here, aka 'BeginRequest'
            // ...
            _countRequest++;
            // Let the middleware pipeline run
            await _next(context);
            _countRequest--;
            // Do tasks after middleware here, aka 'EndRequest'
            // ...
        }
        public static int GetCountRequest()
        {
            return _countRequest;
        }
    }
}
