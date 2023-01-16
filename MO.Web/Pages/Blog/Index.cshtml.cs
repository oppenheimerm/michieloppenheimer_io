using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MO.Core;
using MO.DataStore.EFCore.Utilities;
using MO.UseCases.Interfaces;

namespace MO.Web.Pages.Blog
{
	public class IndexModel : PageModel
	{
		public IndexModel(
			IViewBlogEntiresByFilterUseCase viewBlogEntriesByFilterUseCase,
			IConfiguration configuration
		)
		{
			ViewBlogEntriesByFilterUseCase = viewBlogEntriesByFilterUseCase;
			Configuration = configuration;
		}

		public int? PageIndex { get; set; }
		public PaginatedList<Post>? Posts { get; set; }
		private IViewBlogEntiresByFilterUseCase? ViewBlogEntriesByFilterUseCase { get; }

		private IConfiguration Configuration { get; }

		public async Task OnGet(int? pageIndex)
		{
			PageIndex = pageIndex;
			await GetDataAsync();
		}

		private async Task GetDataAsync()
		{
			if (PageIndex == null || !PageIndex.HasValue)
			{
				PageIndex = 1;
			}

			{
				var pageSize = Configuration.GetValue("pageSize", 12);
				Posts = await PaginatedList<Post>.CreateAsync(
					ViewBlogEntriesByFilterUseCase.Execute().PostsEntries.AsNoTracking(),
					PageIndex ?? 1, pageSize
					);
			}
		}
	}
}
