using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Styles;
using System;

namespace Opten.Web.Infrastructure.Pdf.Extensions
{
	/// <summary>
	/// The Paragraph extensions.
	/// </summary>
	public static class ParagraphExtensions
    {

		/// <summary>
		/// Sets the style.
		/// </summary>
		/// <param name="paragraph">The paragraph.</param>
		/// <param name="style">The style.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">
		/// paragraph
		/// or
		/// style
		/// </exception>
		public static Paragraph SetStyle(this Paragraph paragraph, string style)
        {
            if (paragraph == null)
            {
                throw new ArgumentNullException("paragraph");
            }
            if (string.IsNullOrEmpty(style))
            {
                throw new ArgumentNullException("style");
            }

            paragraph.Style = style;
            return paragraph;
        }

		internal static Paragraph AsParagraph(this string text, bool isBold)
		{
			Paragraph paragraph = new Paragraph();

			paragraph.AddFormattedText(
				text == null ? string.Empty : text.Trim(),
				isBold ? TextFormat.Bold : TextFormat.NotBold);

			return paragraph;
		}

		internal static Paragraph AsParagraphSmall(this string text)
		{
			Paragraph paragraph = new Paragraph();

			paragraph.AddFormattedText(
				text.Trim(),
				PdfStyleNames.Paragraph.Small);

			return paragraph;
		}

    }
}