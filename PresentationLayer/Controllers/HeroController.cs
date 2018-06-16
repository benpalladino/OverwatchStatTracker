using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class HeroController : Controller
    {
        static HeroDataAccess HeroDataAccess = new HeroDataAccess();
        static Mapper Mapper = new Mapper();

        // GET: Hero
        public ActionResult Index()
        {
            return View();
        }


        //VIEW HEROES
        public ActionResult ViewHeroes()
        {
            if (Session["RoleID"] != null)
            {
                HeroViewModel viewModel = new HeroViewModel();
                viewModel.HeroList = Mapper.HeroMap(HeroDataAccess.GetAllHeroes());
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //UPDATE HEROES
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

        [HttpPost]
        public ActionResult AddHero(HeroViewModel viewModel)
        {
            if (Session["RoleID"] != null)
            {
                HeroDataAccess.AddHero(Mapper.HeroMap(viewModel.SingleHero));
                return RedirectToAction("ViewHeroes");
            }
            else
            {
                return RedirectToAction("ViewHeroes");
            }
        }

        //UPDATEHERO
        [HttpGet]
        public ActionResult UpdateHero(int HeroID)
        {
            if (Session["RoleID"] != null)
            {
                HeroViewModel heroViewModel = new HeroViewModel();
                heroViewModel.SingleHero = Mapper.HeroMap(HeroDataAccess.GetHeroByID(HeroID));
                return View(heroViewModel);
            }
            else
            {
                return RedirectToAction("ViewHeroes");
            }
        }

        [HttpPost]
        public ActionResult UpdateHero(HeroViewModel heroViewModel)
        {
            if (Session["RoleID"] != null)
            {
                HeroDataAccess.UpdateHero(Mapper.HeroMap(heroViewModel.SingleHero));
                return RedirectToAction("ViewHeroes");
            }
            else
            {
                return RedirectToAction("ViewHeroes");
            }
        }

        //DELETEHERO
        public ActionResult DeleteHero(int HeroID)
        {
            HeroDataAccess.DeleteHero(HeroID);
            return RedirectToAction("ViewHeroes");
        }
    }
}