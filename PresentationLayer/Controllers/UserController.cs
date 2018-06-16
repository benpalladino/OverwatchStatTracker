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
    public class UserController : Controller
    {
        static UserDataAccess UserDataAccess = new UserDataAccess();
        static Mapper Mapper = new Mapper();
        static PasswordLogic PasswordLogic = new PasswordLogic();

        //GET USER
        public ActionResult Index()
        {
            return View();
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
            return View();
        }

        public ActionResult ViewUsers()
        {
            UserViewModel viewModel = new UserViewModel();
            viewModel.UserList = Mapper.Map(UserDataAccess.GetAllUsers());
            return View(viewModel);
        }

        //UPDATE USER
        [HttpGet]
        public ActionResult UpdateUser(int UserID)
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.SingleUser = Mapper.Map(UserDataAccess.GetUserByID(UserID));
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserViewModel userViewModel)
        {
            UserDataAccess.UpdateUser(Mapper.Map(userViewModel.SingleUser));
            return RedirectToAction("ViewUsers");
        }

        //DELETE USER
        public ActionResult DeleteUser(int UserID)
        {
            UserDataAccess.DeleteUser(UserID);
            return RedirectToAction("ViewUsers");
        }

        //LOGOUT USER
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
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
                    return RedirectToAction("Index", "Home");
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