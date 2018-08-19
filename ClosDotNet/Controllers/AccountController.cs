namespace ClosDotNet.Controllers
{
    using ClosDotNet.Models;
    using ClosDotNet.Resources.Resources;
    using Common.Logging;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using NHibernate;
    using NHibernate.AspNet.Identity;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    
    [Authorize]
    public class AccountController : Controller
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AccountController));
        
        public ISessionFactory SessionFactory { private get; set; }

        public UserManager<IdentityUser> UserManager { get; private set; }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public AccountController(ISessionFactory sessionFactory)
        {
            UserManager = new UserManager<IdentityUser>(
                new UserStore<IdentityUser>(sessionFactory.OpenSession()));
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (HttpContext.Request.IsAuthenticated)
            {
                return RedirectToAction("Welcome", "Home");
            }

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindAsync(model.UserName, model.Password);
            if (user != null)
            {
                await SignInAsync(user, false);
                return RedirectToAction("Welcome", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", CommonResources.MessageLoginFailed);
            }

            // If we got this far, something failed; hence, just redisplay form.
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            AuthenticationManager.SignOut();
            
            return RedirectToAction("Login", "Account");
        }

        private async Task SignInAsync(IdentityUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }

            base.Dispose(disposing);
        }
    }
}