using DownNotifier.BackgroundJob;
using DownNotifier.Business.Abstract;
using DownNotifier.Entities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DownNotifier.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAccountService accountService;
        private readonly INotifierService notifierService; 
        public HomeController(IAccountService service, INotifierService notifierService)
        {
            this.accountService = service;
            this.notifierService = notifierService; 
        }
        [Route("/")]
        public IActionResult Index()
        {
            
            return View();
        }

        #region user
        [Route("UserList")]
        public async Task<IActionResult> UserList()
        {
            var users = await accountService.GetAllAsync(new(a => !a.IsDelete));
            return View(users);
        }
        [Route("UserProcess/{Id}")]
        public async Task<IActionResult> UserProcess(int Id)
        {
            Account entity;
            if (Id == 0)
            {
                entity = new Account();
            }
            else
            {
                entity = await accountService.GetAsync(new(a => a.Id.Equals(Id)));
            }
            return View(entity);
        }
        [Route("UserProcess"), HttpPost]
        public async Task<IActionResult> UserProcess(Account entity)
        {
            if (entity.Id == 0)
            {
                await accountService.AddAsync(entity);
            }
            else
            {
                await accountService.UpdateAsync(entity);
            }
            return Redirect("UserList");
        }
        [Route("UserDelete/{Id}"), HttpPost]
        public async Task<JsonResult> UserDelete(int Id)
        {
            var entity = await accountService.GetAsync(new(a => a.Id.Equals(Id)));
            entity.IsDelete = true;
            var result = await accountService.UpdateAsync(entity);

            return Json(result);
        }
        #endregion

        #region app
        [Route("AppList")]
        public async Task<IActionResult> AppList()
        {
            var notifiers = await notifierService.GetAllAsync(new(a => !a.IsDelete));
            return View(notifiers);
        }
        [Route("AppProcess/{Id}")]
        [Obsolete]
        public async Task<IActionResult> AppProcess(int Id)
        {
            Notifier entity;
            if (Id == 0)
            {
                entity = new Notifier();
            }
            else
            {
                entity = await notifierService.GetAsync(new(a => a.Id.Equals(Id)));
            }
            DownNotifierJobSchedule.PrepareJobs();
            return View(entity);
        }
        [Route("AppProcess"), HttpPost]
        [Obsolete]
        public async Task<IActionResult> AppProcess(Notifier entity)
        {
            if (entity.Id == 0)
            {
                await notifierService.AddAsync(entity);
            }
            else
            {
                await notifierService.UpdateAsync(entity);
            }
            DownNotifierJobSchedule.PrepareJobs();
            return Redirect("AppList");
        }
        [Route("AppDelete/{Id}"), HttpPost]
        [Obsolete]
        public async Task<JsonResult> AppDelete(int Id)
        {
            var entity = await notifierService.GetAsync(new(a => a.Id.Equals(Id)));
            entity.IsDelete = true;
            var result = await notifierService.UpdateAsync(entity);
            DownNotifierJobSchedule.PrepareJobs();
            return Json(result);
        }
        #endregion

        #region login 
        [Route("Login"), AllowAnonymous]
        public IActionResult Login()
        {
            return View();

        }
        [Route("Account/Login"), AllowAnonymous]
        public async Task<IActionResult> Login(string MailAddress, string Password)
        {
            var user = await accountService.GetAsync(new(a => a.MailAddress.Equals(MailAddress) && a.Password.Equals(Password)));
            if (user != null)
            {
                var claims = new List<Claim>
                 {
                    new Claim(ClaimTypes.Name,user.Name),
                 };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("");
            }
            else
            {
                TempData["Message"] = new HtmlString(@"<div class='alert alert-danger alert-dismissable'>
                                <button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>
                                Kullanıcı bilgilerinizi kontrol ediniz. 
                            </div>");
                return View();
            }

        }
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/");
        }
        #endregion
    }
}