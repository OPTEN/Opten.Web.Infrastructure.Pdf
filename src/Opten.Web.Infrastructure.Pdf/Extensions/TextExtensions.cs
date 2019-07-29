using Opten.Common.Extensions;
using Opten.Web.Infrastructure.Pdf.Elements;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Opten.Web.Infrastructure.Pdf.Extensions
{
	internal static class TextExtensions
	{

		internal static string RemoveReference(this string text)
		{
			if (text.Contains("!#") && text.Contains("#!"))
			{
				return text.RemoveBetween("!#", "#!");
			}
			return text;
		}

		internal static string GetReference(this string text)
		{
			if (text.Contains("!#") && text.Contains("#!"))
			{
				// Get reference between !# #!
				return Regex.Match(text, @"\!#([^)]*)\#!").Groups[1].Value;
			}
			else return string.Empty;
		}

		internal static bool IsBold(this string text)
		{
			return text.StartsWith("**") && text.EndsWith("**");
		}

		internal static string RemoveBold(this string text)
		{
			if (text.IsBold()) return text.Replace("**", string.Empty);
			else return text;
		}

		internal static TextLine Longest(this IEnumerable<TextLine> textLines)
		{
			if (textLines == null || textLines.Any() == false)
			{
				return new TextLine(string.Empty);
			}

			return textLines.OrderByDescending(o => o.Text.Length).First();
		}

	}
}