using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using MO.Core;
using MO.UseCases.Interfaces;

namespace MO.Web.Pages.Blog
{
    public class SlugModel : PageModel
    {
		private IViewBlogEntryBySlug? ViewBlogEntryBySlug { get; }
		public Post? Post { get; set; }

		public SlugModel(IViewBlogEntryBySlug? viewBlogEntryBySlug)
		{
			ViewBlogEntryBySlug = viewBlogEntryBySlug;
		}

		public async Task<ActionResult> OnGetAsync(string? slug)
		{
			if (!slug.IsNullOrEmpty())
			{
				var post = await ViewBlogEntryBySlug.ExecuteAsync(slug);
				if (post != null && post.Success == true)
				{
					Post = post.PostEntry;
					return Page();
				}
				else
				{
					return NotFound();
				}
			}
			else
			{
				return NotFound();
			}
		}
	}
}
