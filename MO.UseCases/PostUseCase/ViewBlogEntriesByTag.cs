
using MO.UseCases.DataStoreInterfaces;
using MO.UseCases.Interfaces;
using MO.UseCases.PostResponse;

namespace MO.UseCases.PostUseCase
{
    public class ViewBlogEntriesByTag : IViewBlogEntriesByTag
	{
		private readonly IPostsRepository postsRepository;

		public ViewBlogEntriesByTag(IPostsRepository postsRepository)
		{
			this.postsRepository = postsRepository;
		}

		public PostQueryResponse ExecuteAsync(string tagNameEncoded)
		{
			var postEntries = postsRepository.GetPostsByTag(tagNameEncoded);
			return postEntries;
		}
	}
}
