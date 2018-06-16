// AUTHOR - BEN PALLADINO - ONSHORE OUTSOURCING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using PresentationLayer.Models;
using DataAccessLayer.DataObjects;
using BusinessLogicLayer;

namespace PresentationLayer.Controllers
{
    public class UserController : Controller
    {
        static UserDataAccess UserDataAccess = new UserDataAccess();
        static LeaderboardLogic logicLayer = new LeaderboardLogic();
        static Mapper Mapper = new Mapper();
        static PasswordLogic PasswordLogic = new PasswordLogic();

        //ADMIN PANEL ACTION RESULTS
        public ActionResult AdminPanel()
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

        //PROFILE ACTION RESULT
        public ActionResult Profile()
        {
            if (Session["RoleID"] != null)
            {
                UserViewModel viewModel = new UserViewModel();
                viewModel.SingleUser = Mapper.Map(UserDataAccess.GetUserByID((int)Session["UserID"]));
                viewModel.SingleUser = Mapper.LogicMap(logicLayer.GetUserWL(Mapper.LogicMap(viewModel.SingleUser)));
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //LEADERBOARDS ACTION RESULTS
        public ActionResult Leaderboards()
        {
            if (Session["RoleID"] != null)
            {
                UserViewModel viewModel = new UserViewModel();
                viewModel.UserList = Mapper.Map(UserDataAccess.GetAllUsers());
                viewModel.UserList = Mapper.LogicMap(logicLayer.GetUserWL(Mapper.LogicMap(viewModel.UserList)));
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //UPDATE USERS FAVORITE HERO
        [HttpGet]
        public ActionResult UpdateUserHero(int UserID)
        {
            if (Session["RoleID"] != null)
            {
                UserViewModel userViewModel = new UserViewModel();
                userViewModel.SingleUser = Mapper.Map(UserDataAccess.GetUserByID(UserID));
                return View(userViewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult UpdateUserHero(UserViewModel userViewModel)
        {
            if (Session["RoleID"] != null)
            {
                UserDataAccess.UpdateUsersFavoriteHero(userViewModel.SingleUser.UserID, userViewModel.SingleUser.HeroID);
                return RedirectToAction("Profile");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //UPDATE STATS ACTION RESULTS
        public ActionResult UpdateStats()
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

        //GET USER
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
        public ActionResult Register()
        {
            return View();
        }

        //ADD USER
        [HttpPost]
        public ActionResult Register(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.SingleUser.Password = PasswordLogic.PasswordHash(viewModel.SingleUser.Password);
                viewModel.SingleUser.RoleID = 1;
                viewModel.SingleUser.TeamID = 1;
                viewModel.SingleUser.HeroID = 1;

                UserDataAccess.AddUser(Mapper.Map(viewModel.SingleUser));
            }
            return RedirectToAction("Login", "User");
        }

        //VIEW USERS
        public ActionResult ViewUsers()
        {
            if (Session["RoleID"] != null)
            {
                UserViewModel viewModel = new UserViewModel();
                viewModel.UserList = Mapper.Map(UserDataAccess.GetAllUsers());
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //UPDATE USER
        [HttpGet]
        public ActionResult UpdateUsers(int UserID)
        {
            if (Session["RoleID"] != null)
            {
                UserViewModel userViewModel = new UserViewModel();
                userViewModel.SingleUser = Mapper.Map(UserDataAccess.GetUserByID(UserID));
                return View(userViewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult UpdateUsers(UserViewModel userViewModel)
        {
            if (Session["RoleID"] != null)
            {
                UserDataAccess.UpdateUsers(Mapper.Map(userViewModel.SingleUser));
                return RedirectToAction("ViewUsers");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //DELETE USER
        public ActionResult DeleteUser(int UserID, int StatsID)
        {
            if (Session["RoleID"] != null)
            {
                UserDataAccess.DeleteUser(UserID, StatsID);
                return RedirectToAction("ViewUsers");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //LOGOUT USER
        public ActionResult Logout()
        {
            if (Session["RoleID"] != null)
            {
                Session.Abandon();
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //LOGIN USER
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User validateUser = new User();
                validateUser = Mapper.Map(UserDataAccess.GetUserByUsername(viewModel.SingleUser.Username));
                bool isValid = PasswordLogic.ValidatePasswords(viewModel.SingleUser.Password, validateUser.Password);
                if (isValid)
                {
                    Session["UserID"] = validateUser.UserID;
                    Session["Username"] = validateUser.Username;
                    Session["RoleID"] = validateUser.RoleID;
                    return RedirectToAction("Profile", validateUser);
                }
                else
                {
                    ViewBag.ErrorMessage = "Username and/or Password are incorrect.";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}