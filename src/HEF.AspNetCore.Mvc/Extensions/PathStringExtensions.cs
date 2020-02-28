using System;

namespace Microsoft.AspNetCore.Http
{
	public static class PathStringExtensions
	{
        #region EndsWith
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
        #endregion

        #region Routing
        public static bool IsRouting(this PathString path, string startPath, string endPath)
		{
			return path.IsRouting(new PathString(startPath), new PathString(endPath));
		}

		public static bool IsRouting(this PathString path, PathString startPath, PathString endPath)
		{
			return path.StartsWithSegments(startPath, out PathString remainingPath)
				 && remainingPath.EndsWithSegments(endPath);
		}
		#endregion

		#region Replace
		public static PathString StartReplace(this PathString path, string oldPath, string newPath)
		{
			return path.StartReplace(new PathString(oldPath), new PathString(newPath));
		}

		public static PathString StartReplace(this PathString path, PathString oldPath, PathString newPath)
		{
			if (path.StartsWithSegments(oldPath, out PathString remainingPath))
			{
				return newPath + remainingPath;
			}

			return path;
		}

		public static PathString EndReplace(this PathString path, string oldPath, string newPath)
		{
			return path.EndReplace(new PathString(oldPath), new PathString(newPath));
		}

		public static PathString EndReplace(this PathString path, PathString oldPath, PathString newPath)
		{
			if (path.EndsWithSegments(oldPath, out PathString remainingPath))
			{
				return remainingPath + newPath;
			}

			return path;
		}
		#endregion
	}
}
