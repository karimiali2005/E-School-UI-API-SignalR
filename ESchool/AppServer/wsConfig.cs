using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ESchool.AppServer
{
    public class wsConfig
    {
        public static int BufferSize = 8192 * 8192;
        public static string wsChat = "/wschat";

        public static WebSocketOptions GetOptions()
        {
            WebSocketOptions wsOptions = new WebSocketOptions()
            {
                ReceiveBufferSize = BufferSize,
                KeepAliveInterval = TimeSpan.FromMinutes(90)              
            };
            return wsOptions;
        }


    }
}
