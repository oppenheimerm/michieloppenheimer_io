using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MO.Web.Models;
using MO.Web.Services;
using MO.Web.Helpers;

namespace MO.Web.Pages.Account
{
    public class Login : PageModel
    {
        private readonly ILogger<Login>? _logger;
		private readonly IBlogUserServices? blogUserServices;
		private readonly IConfiguration? config;

		public Login(ILogger<Login>? logger, IBlogUserServices? blogUserServices, IConfiguration? config)
        {
            _logger = logger;
			this.blogUserServices = blogUserServices;
			this.config = config;
		}


        [BindProperty]
        public LoginModel LoginModel { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGet(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
			ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                // Use LoginModel.Email and LoginModel.Password to authenticate the user
                // with our custom authentication logic, aganist the user fields in the 
                // secerets file
                //
                var user = blogUserServices.ValidateUser(LoginModel.Email, LoginModel.Password);
                if (user)
                {
					var claims = new List<Claim>
				    {
                        new Claim(ClaimTypes.Name, LoginModel.Email),
                        new Claim("FullName", config["User:fullname"].ToString()),
					    new Claim(ClaimTypes.Role, "Administrator"),
				    };

					var claimsIdentity = new ClaimsIdentity(
						claims, CookieAuthenticationDefaults.AuthenticationScheme);

					var authProperties = new AuthenticationProperties
					{
						//AllowRefresh = <bool>,
						// Refreshing the authentication session should be allowed.

						//ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
						// The time at which the authentication ticket expires. A 
						// value set here overrides the ExpireTimeSpan option of 
						// CookieAuthenticationOptions set with AddCookie.

						//IsPersistent = true,
						// Whether the authentication session is persisted across 
						// multiple requests. When used with cookies, controls
						// whether the cookie's lifetime is absolute (matching the
						// lifetime of the authentication ticket) or session-based.

						//IssuedUtc = <DateTimeOffset>,
						// The time at which the authentication ticket was issued.

						//RedirectUri = <string>
						// The full path or absolute URI to be used as an http 
						// redirect response value.
					};

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity),
						authProperties);


					_logger.LogInformation("User {Email} logged in at {Time}.",
						LoginModel.Email, DateTime.UtcNow);

					return LocalRedirect(Url.GetLocalUrl(returnUrl));
				}
                else
                {
					this.ModelState.AddModelError(string.Empty, "Username or password is invalid");
					return Page();
				}
			}
			// Something failed. Redisplay the form.
			return Page();
		}

	}
}
