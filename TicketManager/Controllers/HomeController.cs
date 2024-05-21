using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TicketManager.Models;
using TicketManager.Models.ViewModels;
using TicketManager.Services;

namespace TicketManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login(string? message) {
            ViewBag.Message = message;
            return View(new User());
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public IActionResult Login([Bind("Email")] User users)
        {
            if (!string.IsNullOrEmpty(users.Email))
            {
                var user = UserService.GetByUserEmail(users.Email);
                if (user != null)
                {
                    _httpContextAccessor.HttpContext.Session.SetString("Admin", user.IsAdmin.ToString());
                    _httpContextAccessor.HttpContext.Session.SetString("UserName", user.UserName);
                    _httpContextAccessor.HttpContext.Session.SetString("Islogin", "true");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction(nameof(Login), new { message = "Vueillez crée un utilisateur ou saissir un email valide" });
        }
            
        public IActionResult Index(string? message)
		{
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("Admin")))
            {
                return NotFound();
                
            }
            ViewBag.Message = message;
            return View(TicketService.GetAll());
        }

		public IActionResult User(string? message)
		{
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("Admin")))
            {
                return View("Error");
            }
            ViewBag.Message = message;
            return View(UserService.GetAll());
            
            
		}

        public IActionResult CreateEditTicket(int id)
        {

            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("Admin")))
            {
                return NotFound();
            }
            var ticket = new Ticket();
            var sessionAdmin = _httpContextAccessor.HttpContext.Session.GetString("Admin");

            if (sessionAdmin != null && sessionAdmin == "True")
            {
                var userTicketViewModel = UserService.GetAll();
                if (id == 0)
                {
                    if (userTicketViewModel != null)
                    {
                        ViewBag.Users = userTicketViewModel._users;
                        ticket.isEditMode = false;
                        return View(ticket);
                    }
                }
                else
                {
                    ticket = TicketService.GetById(id);
                    if (ticket != null && userTicketViewModel != null)
                    {
                        ViewBag.Users = userTicketViewModel._users;
                        ticket.isEditMode = true;
                        return View(ticket);
                    }
                }
            }
            return RedirectToAction(nameof(Index), new { message = "Vous n'avez le droit de faire cette action." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEditTicket([Bind("IdTicket,Title,Description,UserId,Status,isEditMode")] Ticket ticket)
        {
            
            if (ModelState.IsValid)
            {
                
                try
                {
                    if (ticket.isEditMode)
                    {
                        TicketService.Update(ticket);
                        if (ticket.UserId != null)
                        {
                            TicketService.AssignTicket(ticket.IdTicket, ticket.UserId);
                        }
                    }
                    else
                    {
                        TicketService.Save(ticket);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    
                    throw;
                    
                }
            }
            var userTicketViewModel = UserService.GetAll();
            ViewBag.Users = userTicketViewModel._users;
            return View(ticket);
        }

        public IActionResult CreateEditUser(int id)
        {

            var sessionAdmin = _httpContextAccessor.HttpContext.Session.GetString("Admin");

            if (sessionAdmin != null && sessionAdmin == "True")
            {
                if (id == 0)
                {
                    return View(new User());
                }
                else
                {
                    var user = UserService.GetById(id);
                    if (user != null)
                    {
                        user.isEditMode = true;
                        return View(user);
                    }
                }
            }
            else
            {
                return View(new User());
            }

            return RedirectToAction(nameof(User), new { message = "Vous n'avez le droit de faire cette action." }); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEditUser([Bind("IdUser", "UserName,Email,isEditMode")] User user)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.isEditMode)
                    {
                        UserService.Update(user.IdUser, user);
                    }
                    else
                    {
                        UserService.Save(user);
                    }
                    return RedirectToAction(nameof(User));
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            
            return View(user);
        }

        [HttpGet("/Home/Error")]
        public IActionResult Error(int? statusCode)
        {
            if (statusCode.HasValue && statusCode.Value == 404)
            {
                return View("Error404");
            }

            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
