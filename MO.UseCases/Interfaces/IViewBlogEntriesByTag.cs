using MO.UseCases.PostResponse;

namespace MO.UseCases.Interfaces
{
    public interface IViewBlogEntriesByTag
    {
		PostQueryResponse ExecuteAsync(string? tagNameEncoded);

	}
}