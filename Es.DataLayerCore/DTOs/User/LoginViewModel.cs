using System.ComponentModel.DataAnnotations;

namespace Es.DataLayerCore.DTOs.User
{
    public class LoginViewModel
    {
        [Required]
        public string UsersName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string UsersPass { get; set; }
        public bool RememberMe { get; set; }
        public string PlatfornName { get; set; }
        public string mobileName { get; set; }
        public string TokenFireBase { get; set; }
       
    }
}
