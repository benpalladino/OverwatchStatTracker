// AUTHOR - BEN PALLADINO - ONSHORE OUTSOURCING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using PresentationLayer.Models;
using BusinessLogicLayer;


namespace PresentationLayer.Controllers
{
    public class HeroController : Controller
    {
        static HeroDataAccess HeroDataAccess = new HeroDataAccess();
        static Mapper Mapper = new Mapper();

        //GET HEROES
        public ActionResult Index()
        {
            if (Session["RoleID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult AddHero()
        {
            if (Session["RoleID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //ADD HEROES
        [HttpPost]
        public ActionResult AddHero(HeroViewModel heroViewModel)
        {
            if (Session["RoleID"] != null)
            {
                HeroDataAccess.AddHero(Mapper.heroMap(heroViewModel.SingleHero));
            }
            else
            {
                return RedirectToAction("ViewHeroes", "Hero");
            }
            return RedirectToAction("ViewHeroes", "Hero");

        }

        //VIEW HEROES
        public ActionResult ViewHeroes()
        {
            if (Session["RoleID"] != null)
            {
                HeroViewModel heroViewModel = new HeroViewModel();
                heroViewModel.HeroList = Mapper.heroMap(HeroDataAccess.GetAllHeroes());
                return View(heroViewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //UPDATE HEROES
        [HttpGet]
        public ActionResult UpdateHero(int HeroID)
        {
            if (Session["RoleID"] != null)
            {
                HeroViewModel heroViewModel = new HeroViewModel();
                heroViewModel.SingleHero = Mapper.heroMap(HeroDataAccess.GetHeroByID(HeroID));
                return View(heroViewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult UpdateHero(HeroViewModel heroViewModel)
        {
            if (Session["RoleID"] != null)
            {
                HeroDataAccess.UpdateHero(Mapper.heroMap(heroViewModel.SingleHero));
                return RedirectToAction("ViewHeroes");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        //DELETE HEROES
        public ActionResult DeleteHero(int HeroID)
        {
            if (Session["RoleID"] != null)
            {
                HeroDataAccess.DeleteHero(HeroID);
                return RedirectToAction("ViewHeroes");
            }
            else
            {
                return RedirectToAction("ViewHeroes");
            }

        }
    }
}