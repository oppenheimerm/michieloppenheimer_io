using MO.UseCases.PostResponse;

namespace MO.UseCases.Interfaces
{
    public interface IViewBlogEntryBySlug
    {
        Task<PostEntryResponse> ExecuteAsync(string? slug);
    }
}