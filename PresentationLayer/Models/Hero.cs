using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class Hero
    {
        //HEROES
        public int HeroID { get; set; }
        public string HeroName { get; set; }
        public string HeroType { get; set; }
    }
}