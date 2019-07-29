using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Styles;

namespace Opten.Web.Infrastructure.Pdf.Extensions
{
	internal static class EnumExtensions
	{

		internal static ParagraphAlignment GetAlignment(this Alignment alignment)
		{
			switch (alignment)
			{
				case Alignment.Left:
					return ParagraphAlignment.Left;
				case Alignment.Center:
					return ParagraphAlignment.Center;
				case Alignment.Right:
					return ParagraphAlignment.Right;
				case Alignment.Justify:
					return ParagraphAlignment.Justify;
				default:
					return ParagraphAlignment.Left;
			}
		}

		/*internal static RelativeVertical GetRelativeVertical(this Relative format)
		{
			switch (format)
			{
				case Relative.Line:
					return RelativeVertical.Line;
				case Relative.Margin:
					return RelativeVertical.Margin;
				case Relative.Page:
					return RelativeVertical.Page;
				case Relative.Paragraph:
					return RelativeVertical.Paragraph;
				default:
					return RelativeVertical.Paragraph;
			}
		}*/

	}
}