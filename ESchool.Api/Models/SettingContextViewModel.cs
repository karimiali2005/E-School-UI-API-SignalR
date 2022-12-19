using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ESchool.Api.Need.SettingContext;

namespace ESchool.Api.Models
{
    public class SettingContextViewModel
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public RoomChatSatus RoomChatSatus { get; set; }
        public AdminPanel AdminPanel { get; set; }
        public PathStoreFiles PathStoreFiles { get; set; }
        public UploadSize UploadSize { get; set; }
        public PathStoreProfileImage PathStoreProfileImage { get; set; }
      
    }
}
