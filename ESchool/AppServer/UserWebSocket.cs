using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ESchool.AppServer
{
    public class UserWebSocket
    {
        public string UserId { get; set; }        
        public string TeacherId { get; set; }
        public string CourseId { get; set; }
        public WebSocket Websocket { get; set; }

    }
}
