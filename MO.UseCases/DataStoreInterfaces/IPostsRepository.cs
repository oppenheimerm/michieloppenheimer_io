using MO.UseCases.PostResponse;

namespace MO.UseCases.DataStoreInterfaces
{
    public interface IPostsRepository
    {
        PostQueryResponse GetAllPosts();
        Task<PostEntryResponse> GetPostBySlug(string slug);
        PostQueryResponse GetPostsByTag(string tagNameEncoded);

	}
}