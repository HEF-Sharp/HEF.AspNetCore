using System;

namespace Microsoft.AspNetCore.Http
{
	public static class PathStringExtensions
	{
		public static bool EndsWithSegments(this PathString pathString, PathString other)
		{
			return EndsWithSegments(pathString, other, StringComparison.OrdinalIgnoreCase);
		}

		public static bool EndsWithSegments(this PathString pathString, PathString other, StringComparison comparisonType)
		{
			string text = pathString.Value ?? string.Empty;
			string text2 = other.Value ?? string.Empty;
			if (text.EndsWith(text2, comparisonType))
			{
				if (text.Length == text2.Length)
					return true;

				if (text2.StartsWith('/'))
					return true;

				return text[text.Length - text2.Length - 1] == '/';

			}
			return false;
		}

		public static bool EndsWithSegments(this PathString pathString, PathString other, out PathString remaining)
		{
			return EndsWithSegments(pathString, other, StringComparison.OrdinalIgnoreCase, out remaining);
		}

		public static bool EndsWithSegments(this PathString pathString, PathString other,
			StringComparison comparisonType, out PathString remaining)
		{
			string text = pathString.Value ?? string.Empty;
			string text2 = other.Value ?? string.Empty;
			if (text.EndsWith(text2, comparisonType)
				&& (text.Length == text2.Length || text2.StartsWith('/') || text[text.Length - text2.Length - 1] == '/'))
			{
				remaining = new PathString(text.Substring(0, text.Length - text2.Length));
				return true;
			}
			remaining = PathString.Empty;
			return false;
		}
	}
}
