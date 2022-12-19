using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESchool.AppServer
{
    public class MessageRecieve
    {
        public string Id { get; set; }
        public string Reciever { get; set; }
        public string RoomId { get; set; }
        public string Teacher { get; set; }
        public string Course { get; set; }
        public string Message { get; set; }
        public string Filename { get; set; }
        public string MimeType { get; set; }        
        public bool Tag { get; set; }
        public string ParentId { get; set; }
        public string CRUD { get; set; }
    }
}

