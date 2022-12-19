using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Models
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ApplicationUser {



        [Required]                                
        public string FirstName { get; set; }


        [Required]                
        public string LastName { get; set; }


        [Required]
        [Column(TypeName = "varchar(11)")]
        [RegularExpression("^09[0-9]{9}", ErrorMessage = "{0} صحیح نمی باشد")]
        [Display(Name = "همراه")]
        [MaxLength(11, ErrorMessage = "طول {0} حداکثر 11 کاراکتر می باشد")]
        public string MobileNumber { get; set; }        
    
    }
}
