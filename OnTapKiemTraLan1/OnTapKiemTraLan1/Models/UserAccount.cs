using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnTapKiemTraLan1.Models
{
    public class UserAccount
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 5 to 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
    }
}