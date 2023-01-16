namespace MO.Web.Helpers
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
