using MO.Core;
using MO.UseCases.Response;

namespace MO.UseCases.PostResponse
{
	public class PostEntryResponse : BaseUseCaseResponse
	{
		public Post? PostEntry { get; set; }
	}
}
