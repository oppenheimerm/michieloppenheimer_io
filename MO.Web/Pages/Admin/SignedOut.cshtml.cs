using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MO.Web.Pages.Admin
{
    public class SignedOutModel : PageModel
    {
        public void OnGet()
        {
			if (User.Identity.IsAuthenticated)
			{
				// Redirect to home page if the user is authenticated.
				RedirectToPage("/Index");
			}
		}
    }
}
