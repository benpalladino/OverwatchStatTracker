using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class HeroViewModel
    {
        public Hero SingleHero { get; set; }
        public List<Hero> HeroList { get; set; }

        public HeroViewModel()
        {
            SingleHero = new Hero();
            HeroList = new List<Hero>();
        }
    }
}