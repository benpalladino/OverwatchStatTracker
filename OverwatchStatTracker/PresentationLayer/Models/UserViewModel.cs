﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PresentationLayer.Models;

namespace PresentationLayer.Models
{
    public class UserViewModel
    {
        public User SingleUser { get; set; }
        public List<User> UserList { get; set; }

        public UserViewModel()
        {
            SingleUser = new User();
            UserList = new List<User>();
        }
    }  
}