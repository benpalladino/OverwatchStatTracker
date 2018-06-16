using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class User
    {
        [Required(ErrorMessage ="This field is required.")]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string BattleNet { get; set; }
        public int HeroID { get; set; }
        public int RoleID { get; set; }
        public int StatsID { get; set; }
        public int TeamID { get; set; }
    }
}