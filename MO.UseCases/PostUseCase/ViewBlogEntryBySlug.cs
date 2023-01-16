
using MO.Core;
using MO.UseCases.DataStoreInterfaces;
using MO.UseCases.Interfaces;
using MO.UseCases.PostResponse;

namespace MO.UseCases.PostUseCase
{
    public class ViewBlogEntryBySlug : IViewBlogEntryBySlug
	{
		private readonly IPostsRepository postsRepository;

		public ViewBlogEntryBySlug(IPostsRepository postsRepository)
		{
			this.postsRepository = postsRepository;
		}

		public async Task<PostEntryResponse> ExecuteAsync(string? slug)
		{
			var postEntry = await postsRepository.GetPostBySlug(slug);
			return postEntry;
		}
	}
}
