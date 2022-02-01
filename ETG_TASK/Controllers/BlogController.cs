using ETG_TASK.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ETG_TASK.Models;
using ETG_TASK.Models.ViewModels;

namespace ETG_TASK.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _db;
        [Obsolete]
        private readonly IHostingEnvironment Environment;

        [Obsolete]
        public BlogController(ApplicationDbContext db, IHostingEnvironment _environment)
        {
            _db = db;
            Environment = _environment;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                var data = _db.Blogs.ToList();
                return View(data);   
            }

            return RedirectToAction("LogIn", "Account");
        }

        [HttpPost]
        public IActionResult Index(Blog data)
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                if(data.Title != null)
                {
                    var search = _db.Blogs.Where(s => s.Title.Contains(data.Title)).ToList();
                    ModelState.Clear();
                    return View(search);
                }
                else
                {
                    var filtereddata = _db.Blogs.Where(d => d.Category.Equals(data.Category)).ToList();
                    ModelState.Clear();
                    return View(filtereddata); 
                }
                
            }
            ModelState.Clear();
            return RedirectToAction("LogIn", "Account");
        }
        //Used this route so I can pass BlogID as an id from the view to the SinglePost action
        [Route("/Blog/SinglePost/{BlogID}")]
        public IActionResult SinglePost(int BlogID)
        {
            HttpContext.Session.SetInt32("BlogID", BlogID);
            if(HttpContext.Session.GetInt32("BlogID") != null)
            {
                var tables = new BlogWithComment
                {
                    Blogs = _db.Blogs.ToList(),
                    Comments = _db.Comments.ToList()
                };
                return View(tables);
            }
            return RedirectToAction("Index");      
        }

        //HttpPost action to recieve the comment string submitted from SinglePost View.
        [HttpPost]
        public IActionResult SinglePost(BlogWithComment comment)
        { 
                var data = new Comments { Comment = comment.UserComment,
                Commenter = HttpContext.Session.GetString("Username"),
                BlogID = (int)HttpContext.Session.GetInt32("BlogID") };
                _db.Comments.Add(data);
                _db.SaveChanges();
                return View();
        }


        public IActionResult UploadPost()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        //HttpPost action to receive the picture and the blog object.
        [HttpPost]
        [Obsolete]
        public IActionResult UploadPost(IFormFile postedFiles,Blog blog)

        {

            if (HttpContext.Session.GetString("Username") != null)
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                string path = Path.Combine(this.Environment.WebRootPath, "images");
            
                string fileName = Path.GetFileName(postedFiles.FileName);
                using (FileStream stream = new(Path.Combine(path, fileName), FileMode.Create))   
                postedFiles.CopyTo(stream);
                ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);

                blog.Admin = HttpContext.Session.GetString("Username");
                var post = new Blog { Category = blog.Category, ImagePath = Path.Combine("~/images/", fileName), Text = blog.Text, Title = blog.Title, Admin = blog.Admin};
                _db.Blogs.Add(post);
                _db.SaveChanges();
                
                return View();
            }
            return RedirectToAction("Index");

            
            
        }
        //Clearing the session after log out so another user can log in with a different BlogID
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn", "Account");
        }
    }
}
