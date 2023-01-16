using System.Security.Policy;

namespace michieloppenheimer_io.web.Helpers
{
	public static class ViewHelpers
	{
		public static string GetPostBackgroundImage(string? imageSrc)
		{
			return $"/img/posts/{imageSrc}";
		}

		public static string GetPostLink(string? postSlug) => $"/blog/{postSlug}/";
	}
}
