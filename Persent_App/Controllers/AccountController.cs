using App.Services;
using Domin.Model;
using Domin.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using web.ViewModel;

namespace Persent_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSenderServices _emailSender;
        private readonly IViewRenderService _viewRenderService;


        //public AccountController(UserManager<IdentityUser> userManager, IEmailSenderServices emailSender, IViewRenderService viewRenderService, SignInManager<IdentityUser> signInManager)
        public AccountController(UserManager<IdentityUser> userManager, IEmailSenderServices emailSender, IViewRenderService viewRenderService, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
        }

        [HttpGet]
        public IActionResult CheckSession()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == null)
            {
                return Unauthorized(); // وضعیت سشن معتبر نیست
            }
            return Ok(); // وضعیت سشن معتبر است
        }



        [HttpGet]
        public IActionResult AccessDenied()
        
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.IsSent = false;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userManager.CreateAsync(new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email,

            }, model.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                    return View();
                }
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            string? callBackUrl = Url.ActionLink("ConfirmEmail", "Account", new { userId = user.Id, token = token },
                Request.Scheme);
            string body = await _viewRenderService.RenderToStringAsync("RegisterEmail", callBackUrl);
            await _emailSender.SendEmailAsync(new EmailModelViewModel(user.Email, "تایید حساب", body));
            ViewBag.IsSent = true;
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            ViewBag.IsConfirmed = result.Succeeded;/* result.Succeeded ? true : false;*/
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            #region oldcode
            //if (!ModelState.IsValid)
            //    return View(model);

            //// پیدا کردن کاربر
            //var user = await _userManager.FindByNameAsync(model.UserName);
            //// پیدا کردن کاربر
            //if (user == null)
            //{
            //    ModelState.AddModelError(string.Empty, "کاربری با این نام کاربری یا ایمیل وجود ندارد.");
            //    return View(model);
            //}

            //// بررسی پسورد
            //var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            //if (!isPasswordValid)
            //{
            //    ModelState.AddModelError(string.Empty, "پسورد واردشده صحیح نیست.");
            //    return View(model);
            //}

            //if (user == null)
            //{
            //    ModelState.AddModelError(string.Empty, "کاربری با این نام کاربری یا ایمیل وجود ندارد.");
            //    return View(model);
            //}

            //// ورود
            //var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);

            //if (result.Succeeded)
            //{
            //      // برای آزمایش از URL ثابت استفاده کنید
            //        return RedirectToAction("Index", "ManageUser");


            //}
            //else if (result.IsLockedOut)
            //{
            //    Console.WriteLine("User account is locked out: " + user.UserName);
            //    ModelState.AddModelError(string.Empty, "حساب شما قفل شده است.");
            //}
            //else
            //{
            //    Console.WriteLine("Login failed for user: " + user.UserName);
            //    ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است.");
            //}


            //return View(model);
            #endregion


            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // کاربر یافت نشد
                ModelState.AddModelError(string.Empty, "کاربر یافت نشد");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                // ایمیل کاربر تایید نشده
                ModelState.AddModelError(string.Empty, "ایمیل شما تأیید نشده است.");
                return View(model);
            }

            // تلاش برای لاگین کاربر با نام کاربری و رمز عبور
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            var roles = await _userManager.GetRolesAsync(user);

            if (result.Succeeded)
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                if (roles.Contains("admin"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "admin" });
                }
                else if (roles.Contains("user"))
                {
                    return RedirectToAction("CreateUrl", "Url"); // به صفحه‌ی مخصوص کاربران هدایت شود
                }
                else if (roles.Contains("admin") && roles.Contains("user")) 
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "admin" });

                }
                return RedirectToAction("Login", "Account");
            }
            else
            {
                // پیام خطا در صورت عدم موفقیت
                ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است.");
                return View(model);
            }
        }



        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewBag.IsSent = false;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsSent = false;
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "تلاش برای ارسال ایمیل ناموفق بود ");
                ViewBag.IsSent = false;
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            string? callBackUrl = Url.ActionLink("ResetPassword", "Account", new { email = user.Email, token = token },
                Request.Scheme);
            string body = await _viewRenderService.RenderToStringAsync("ResetPasswordEmail", callBackUrl);
            await _emailSender.SendEmailAsync(new EmailModelViewModel(user.Email, "بازیابی کلمه عبور ", body));
            ViewBag.IsSent = true;
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token)) return BadRequest();
            ResetPasswordVM model = new ResetPasswordVM()
            {
                Email = email,
                Token = token
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid) return View();
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "تلاش برای بازیابی کلمه عبور ناموفق بود ");
                    ViewBag.IsSent = false;
                    return View(model);
                }
                var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
                return RedirectToAction("Login");


            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();  // پاک کردن سشن‌ها
            //return RedirectToAction("Login", "Account");
            return new ContentResult
            {
                Content = "<script>localStorage.setItem('isLoggedOut', 'true'); window.location = '/Account/Login';</script>",
                ContentType = "text/html"
            };
            //await _signInManager.SignOutAsync();
            //return RedirectToAction("Login", "Account");
        }

    }
}
