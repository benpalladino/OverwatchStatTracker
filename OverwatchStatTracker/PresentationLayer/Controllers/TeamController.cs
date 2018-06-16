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
    public class TeamController : Controller
    {
        static TeamDataAccess TeamDataAccess = new TeamDataAccess();
        static Mapper Mapper = new Mapper();

        //GET TEAMS
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddTeam()
        {
            return View();
        }

        //ADD TEAMS
        [HttpPost]
        public ActionResult AddTeam(TeamViewModel teamViewModel)
        {
            if (Session["RoleID"] != null)
            {
                TeamDataAccess.AddTeam(Mapper.teamMap(teamViewModel.SingleTeam));
                return RedirectToAction("ViewTeams", "Team");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        //VIEW TEAMS
        public ActionResult ViewTeams()
        {
            if(Session["RoleID"] != null)
            {
                TeamViewModel teamViewModel = new TeamViewModel();
                teamViewModel.TeamList = Mapper.teamMap(TeamDataAccess.GetAllTeams());
                return View(teamViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        //UPDATE TEAMS
        [HttpGet]
        public ActionResult UpdateTeam(int TeamID)
        {
            TeamViewModel teamViewModel = new TeamViewModel();
            teamViewModel.SingleTeam = Mapper.teamMap(TeamDataAccess.GetTeamByID(TeamID));
            return View(teamViewModel);
        }

        [HttpPost]
        public ActionResult UpdateTeam(TeamViewModel teamViewModel)
        {
            TeamDataAccess.UpdateTeam(Mapper.teamMap(teamViewModel.SingleTeam));
            return RedirectToAction("ViewTeams");
        }

        //DELETE TEAM
        public ActionResult DeleteTeam(int TeamID)
        {
            TeamDataAccess.DeleteTeam(TeamID);
            return RedirectToAction("ViewTeams");
        }
    }
}