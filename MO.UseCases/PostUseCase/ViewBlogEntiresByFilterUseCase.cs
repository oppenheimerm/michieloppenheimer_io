
using MO.UseCases.DataStoreInterfaces;
using MO.UseCases.Interfaces;
using MO.UseCases.PostResponse;

namespace MO.UseCases.PostUseCase
{
	public class ViewBlogEntiresByFilterUseCase : IViewBlogEntiresByFilterUseCase
	{
		private readonly IPostsRepository postsRepository;

		public ViewBlogEntiresByFilterUseCase(IPostsRepository postsRepository)
		{
			this.postsRepository = postsRepository;
		}

		public PostQueryResponse Execute()
		{
			return postsRepository.GetAllPosts();
		}
	}
}
