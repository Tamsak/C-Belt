using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CSharpExam.Models;

namespace CSharpExam.Controllers
{
    public class HomeController : Controller
    {
        private myContext _context;
 
        public HomeController(myContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return View();
            }
        return RedirectToAction("ActivityList");
        }
        [HttpPost("craeteuser")]
        public IActionResult CreateUser(RegisUser user)
        {
            if(ModelState.IsValid)
            {
                PasswordHasher<RegisUser> Hasher = new PasswordHasher<RegisUser>();
                user.Password = Hasher.HashPassword(user, user.Password);
                User NewUser = new User
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    Password = user.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
            
                _context.Add(NewUser);
                _context.SaveChanges();
                User justCreated =  _context.Users.FirstOrDefault(User => user.Email == user.Email);
                HttpContext.Session.SetInt32("UserId", justCreated.UserId);
                HttpContext.Session.SetString("Firstname", justCreated.Firstname);
            return RedirectToAction("ActivityList");
            }
        return View("Index");
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginUser model)
        {
            var user = _context.Users.SingleOrDefault(User => User.Email == model.LogEmail);
            if(user != null && model.Confirm != null)
            {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, model.Confirm))
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("Firstname", user.Firstname);
                    return RedirectToAction("ActivityList");
                }
            }
        return View("Index");
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpGet("ActivityList")]
        public IActionResult ActivityList()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index");
            }
            User Users =_context.Users.SingleOrDefault(u=>u.UserId== HttpContext.Session.GetInt32("UserId"));
            List <Activity> Activities =_context.Activities
                                        .Include(a=>a.Creator)
                                        .Include(a=>a.participants)
                                            .ThenInclude(p=>p.User)
                                        .Where(a=>a.DateTime>DateTime.Now)
                                        .OrderBy(a=>a.DateTime)
                                        .ToList();
            ViewBag.Activities = Activities;

        return View(Users);
        }
        [HttpGet("CreateActivity")]
        public IActionResult CreateActivity()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index");
            }
        return View();
        }
        [HttpPost("Create")]
        public IActionResult Create(Activity model)
        {
            if(ModelState.IsValid)
            {
                Activity NewActivity = new Activity
                {
                    Title = model.Title,
                    DateTime = model.DateTime,
                    Duration = model.Duration,
                    Unit = model.Unit,
                    Description = model.Description,
                    UserId = (int)HttpContext.Session.GetInt32("UserId"),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
            
                _context.Add(NewActivity);
                _context.SaveChanges();
            return RedirectToAction("ActivityList");
            }
        return View("CreateActivity");
        }

        [HttpPost("Join")]
        public IActionResult Join(int ActivityId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index");
            }

            User user = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            Activity activity = _context.Activities.SingleOrDefault(a => a.ActivityId == ActivityId);

            
            Activitycenter addActivity = new Activitycenter {
                UserId = user.UserId,
                ActivityId = activity.ActivityId
            };
            _context.Add(addActivity);
            _context.SaveChanges();

        return RedirectToAction("ActivityList");
        }
        [HttpPost("Leave")]
        public IActionResult Leave(int ActivityId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index");
            }
            User user = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            Activity activity = _context.Activities.SingleOrDefault(a => a.ActivityId == ActivityId);

            Activitycenter RemoveActivity = _context.Activitycenters
                                            .Where(ac=>ac.UserId==user.UserId)
                                            .SingleOrDefault(ac=>ac.ActivityId==activity.ActivityId);
            _context.Activitycenters.Remove(RemoveActivity);
            _context.SaveChanges();

        return RedirectToAction("ActivityList");
        }
        [HttpPost("Delete")]
        public IActionResult Delete(int ActivityId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index");
            }
            User user = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            Activity activity = _context.Activities.SingleOrDefault(a => a.ActivityId == ActivityId);
            List<Activitycenter> activities = _context.Activitycenters.Where(ac=>ac.ActivityId==activity.ActivityId).ToList();
            _context.Activitycenters.RemoveRange(activities);
            _context.SaveChanges();
            _context.Remove(activity);
            _context.SaveChanges();
            return RedirectToAction("ActivityList");
        }
        [HttpGet("{ActivityId}")]
        public IActionResult ActivityDetail(int ActivityId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index");
            }
            Activity Activity =_context.Activities
                                          .Where(a=>a.ActivityId==ActivityId)
                                          .Include(a=>a.Creator)
                                          .FirstOrDefault();
            List <Activitycenter> Activities = _context.Activitycenters
                                          .Where(ac=>ac.ActivityId==ActivityId)
                                          .Include(ac=>ac.User)
                                          .ToList();
            @ViewBag.Activities = Activities;
            return View(Activity);
        }
    }
}