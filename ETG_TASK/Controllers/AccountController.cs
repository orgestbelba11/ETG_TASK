using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETG_TASK.Models;
using ETG_TASK.Data;
using ETG_TASK.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace ETG_TASK.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult LogIn()
        {
            var model = new Account();
            return View(model);
        }

        //HttpPost Action to check if the username and password are valid.
        [HttpPost]
        public IActionResult LogIn(Account model)
        {
            var data = _db.Users.Where(s => s.Username.Equals(model.Username) && s.Password.Equals(model.Password)).ToList();
            if (data.Count == 1)
            {
                ViewBag.Message += string.Format("Logged in successfully<br />");
                foreach(var u in _db.Users)
                {
                    if(u.Username == model.Username && u.Password == u.Password)
                    {
                        HttpContext.Session.SetString("Username", u.Username);
                    }
                }
                
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ViewBag.Message += string.Format("Wrong password or username!<br />");
                return View();
            }
        }
        public IActionResult SignUp()
        {
            var model = new Account();
            return View(model);
        }

        //HttpPost action to check if a user is already register and if not, to save the new user at database.
        [HttpPost]
        public IActionResult SignUp(Account model)
        {
            
            var data = _db.Users.Where(s => s.Username.Equals(model.Username)).ToList();

            if (data.Count > 0)
            {
                ViewBag.Message += string.Format("Username already taken!<br />");
                return View();
            }
            else
            {
                var account = new Account { Username = model.Username, Password = model.Password };
                _db.Users.Add(account);
                _db.SaveChanges();
                ViewBag.Message += string.Format("Registered Successfuly!<br />");
                return RedirectToAction("Index", "Blog");
            }
        }
        
    }
}
