using System;
using System.Collections.Generic;

namespace DatabaseLayer.Models
{
    public partial class RoomSetting
    {
        public int RoomSettingId { get; set; }
        public int RoomId { get; set; }
        public int TeacherId { get; set; }
        public bool CloseChat { get; set; }
        public bool PermissionCloseChat { get; set; }
        public bool PermissionStudentChatEdit { get; set; }
        public bool PermissionStudentChatDelete { get; set; }
        public int PinRoomChatId { get; set; }
    }
}
