using michieloppenheimer_io.web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MO.Core;
using MO.DataStore.EFCore.Utilities;
using MO.UseCases.Interfaces;

namespace michieloppenheimer_io.web.ViewComponents
{
	//  This is the code part of the BlogGridViewComponent.It makes use of
	//  the ViewPostsUseCase via dependency injection system to resolve
	//  the implementation of IViewPostsUseCase which provides dependency
	//  injection of IPostRepository and is injected into the constructor
	//  of the view component class. The InvokeAsync method obtains a
	//  List<Post> from the service and passes it to the view.
	[ViewComponent]
	public class BlogGridViewComponent : ViewComponent
	{
		//private IViewBlogEntiresByFilterUseCase ViewBlogEntriesByFilterUseCase { get; }
		private readonly ILogger<BlogGridViewComponent>? _logger;
		public PaginatedList<Post>? Posts { get; set; }
		//private IConfiguration Configuration { get; }
		public int? PageIndex { get; set; }

		public BlogGridViewComponent(
			//IViewBlogEntiresByFilterUseCase viewBlogEntriesByFilterUseCase,
			ILogger<BlogGridViewComponent> logger
			//IConfiguration configuration
			)
		{
			//ViewBlogEntriesByFilterUseCase = viewBlogEntriesByFilterUseCase;
			_logger = logger;
			//Configuration = configuration;
		}

		public IViewComponentResult Invoke(
			int? pageIndex,
			PaginatedList<Post> posts
			)
		{
			PageIndex = pageIndex;
			Posts = posts;
			//await GetDataAsync();

			return View(Posts);
		}

		/*private async Task GetDataAsync()
		{
			if(PageIndex == null || !PageIndex.HasValue)
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
		}*/
	}
}
