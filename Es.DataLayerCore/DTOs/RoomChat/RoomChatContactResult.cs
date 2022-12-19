using System.ComponentModel.DataAnnotations.Schema;

namespace Es.DataLayerCore.DTOs.RoomChat
{
    public partial class RoomChatContactResult
    {
        public int UserID { get; set; }
        public string PicName { get; set; }
        public string FullName { get; set; }
    }
    public partial class RoomChatContactResult
    {
        [NotMapped]
        public string PicNameShort { get; set; }
    }
}
