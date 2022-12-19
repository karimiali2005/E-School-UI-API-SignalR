using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESchool.AppServer
{
    public class MessageSend
    {
        public string Id { get; set; }        
        public string Sender { get; set; }
        public string SenderName { get; set; }
        public string SenderTime { get; set; }        
        public string RoomId { get; set; }  
        public string CourseId { get; set; }
        public string Message { get; set; }
        public string Filename { get; set; }
        public string MimeType { get; set; }
        public bool Tag { get; set; }
        public string SenderNameParent { get; set; }
        public string MessageParent { get; set; }
        public string ParentId { get; set; }
        public string CRUD { get; set; }
        public string AddressDownload { get; set; }
    }
}
