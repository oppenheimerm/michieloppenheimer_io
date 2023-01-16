using MO.Core;
using MO.UseCases.Response;

namespace MO.UseCases.PostResponse
{
    public class PostQueryResponse : BaseUseCaseResponse
    {
        // Collection PostEntries to prevent collision with post plural.
        public IQueryable<Post>? PostsEntries { get; set; }    
    }
}
