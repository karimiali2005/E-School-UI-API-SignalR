using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ESchool.AppServer
{
    public class UserList
    {
        public static ConcurrentDictionary<string, UserWebSocket> sockets = new ConcurrentDictionary<string, UserWebSocket>();
    }
}
