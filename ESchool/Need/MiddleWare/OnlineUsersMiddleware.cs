using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using UAParser;

namespace ESchool.Need.MiddleWare
{
    public static class OnlineUsersMiddlewareExtensions
    {
        public static void UseOnlineUsers(this IApplicationBuilder app, string cookieName = "UserGuid", int lastActivityMinutes = 20)
        {
            app.UseMiddleware<OnlineUsersMiddleware>(cookieName, lastActivityMinutes);
        }
    }

    public class OnlineUsersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _cookieName;
        private readonly int _lastActivityMinutes = 20;
        private static readonly ConcurrentDictionary<string, bool> _allKeys = new ConcurrentDictionary<string, bool>();

        public OnlineUsersMiddleware(RequestDelegate next, string cookieName = "UserGuid", int lastActivityMinutes = 20)
        {
            _next = next;
            _cookieName = cookieName;
            _lastActivityMinutes = lastActivityMinutes;
        }

        public Task InvokeAsync(HttpContext context, IMemoryCache memoryCache)
        {
            try
            {



                if (context.Request.Cookies.TryGetValue(_cookieName, out var userGuid) == false)
                {
                    userGuid = Guid.NewGuid().ToString();
                    context.Response.Cookies.Append(_cookieName, userGuid, new CookieOptions { HttpOnly = true, MaxAge = TimeSpan.FromMinutes(20) });
                }

                memoryCache.GetOrCreate(userGuid, cacheEntry =>
                {
                    if (_allKeys.TryAdd(userGuid, true) == false)
                    {
                    //if add key faild, setting expiration to the past cause dose not cache
                    cacheEntry.AbsoluteExpiration = DateTimeOffset.MinValue;


                    }
                    else
                    {
                        cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(_lastActivityMinutes);
                        cacheEntry.RegisterPostEvictionCallback(RemoveKeyWhenExpired);
                        context.Session.SetString("SessID", new Guid().ToString());

                        using (SqlConnection sql = new SqlConnection(Startup._connection))
                        {
                            using (SqlCommand cmd = new SqlCommand("SessionInsert", sql))
                            {

                                var userAgent = context.Request.Headers["User-Agent"];
                                var uaParser = Parser.GetDefault();
                                ClientInfo c = uaParser.Parse(userAgent);

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("@Ticket", context.Session.Id));
                                cmd.Parameters.Add(new SqlParameter("@BrowserName", c.UA.Family));
                                cmd.Parameters.Add(new SqlParameter("@DeviceName", c.Device.Family));


                                cmd.Parameters.Add(new SqlParameter("@IPs", context.Connection.RemoteIpAddress.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@BrowserVersion", c.UA.Major + "." + c.UA.Minor));
                                cmd.Parameters.Add(new SqlParameter("@CookieName", userGuid));


                                sql.Open();
                                cmd.ExecuteNonQuery();

                                sql.Close();


                            }
                        }
                    }
                    return string.Empty;
                });

               
            }
            catch(Exception ex)
            {

            }
            return _next(context);
        }

        private void RemoveKeyWhenExpired(object key, object value, EvictionReason reason, object state)
        {
            try
            {

                var strKey = (string)key;

               
                //try to remove key from dictionary
                if (!_allKeys.TryRemove(strKey, out _))
                    //if not possible to remove key from dictionary, then try to mark key as not existing in cache
                    _allKeys.TryUpdate(strKey, false, true);
                using (SqlConnection sql = new SqlConnection(Startup._connection))
                {
                    using (SqlCommand cmd = new SqlCommand("SessionEnd", sql))
                    {



                        cmd.Parameters.Add(new SqlParameter("@CookieName", strKey));


                        sql.Open();
                        cmd.ExecuteNonQuery();

                        sql.Close();


                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public static int GetOnlineUsersCount()
        {
            return _allKeys.Count(p => p.Value);
        }
    }
}
