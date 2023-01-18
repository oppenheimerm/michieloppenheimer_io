using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MO.Web.Pages.Account;

namespace MO.Web.Pages.Admin
{
    public class LogoutModel : PageModel
    {

		private readonly ILogger<LogoutModel>? _logger;
		public LogoutModel(ILogger<LogoutModel> logger)
		{
			_logger = logger;
		}
		public async Task OnGetAsync()
        {
			_logger.LogInformation("User {Name} logged out at {Time}.",
				User.Identity.Name, DateTime.UtcNow);
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			RedirectToPage("/SignedOut");
		}
    }
}
