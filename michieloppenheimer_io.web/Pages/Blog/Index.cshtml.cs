using michieloppenheimer_io.web.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MO.Core;
using MO.DataStore.EFCore.Utilities;
using MO.UseCases.Interfaces;

namespace michieloppenheimer_io.web.Pages.Blog
{
    public class IndexModel : PageModel
    {
		public IndexModel(
			IViewBlogEntiresByFilterUseCase viewBlogEntriesByFilterUseCase,
			//ILogger<IndexModel> logger,
			IConfiguration configuration
			)
		{
			ViewBlogEntriesByFilterUseCase = viewBlogEntriesByFilterUseCase;
			//Logger = logger;
			Configuration = configuration;
		}

        public int? PageIndex { get; set; }
		public PaginatedList<Post>? Posts { get; set; }
		private IViewBlogEntiresByFilterUseCase? ViewBlogEntriesByFilterUseCase { get; }
		
		//private ILogger<IndexModel> Logger { get; }
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
