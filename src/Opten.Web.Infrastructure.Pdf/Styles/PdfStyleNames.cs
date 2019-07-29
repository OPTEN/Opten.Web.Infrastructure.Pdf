using MigraDoc.DocumentObjectModel;

namespace Opten.Web.Infrastructure.Pdf.Styles
{
	/// <summary>
	/// The PDF Style Names for CI/CD.
	/// </summary>
	public static class PdfStyleNames
	{

		/// <summary>
		/// The normal/default style for all elements.
		/// </summary>
		public const string Normal = MigraDoc.DocumentObjectModel.StyleNames.Normal;
		/// <summary>
		/// The header style.
		/// </summary>
		public const string Header = MigraDoc.DocumentObjectModel.StyleNames.Header;
		/// <summary>
		/// The footer  style.
		/// </summary>
		public const string Footer = MigraDoc.DocumentObjectModel.StyleNames.Footer;

		/// <summary>
		/// Styles for titles.
		/// </summary>
		public static class Title
		{
			private const string _heading = "Heading";

			/// <summary>
			/// The default style for titles.
			/// </summary>
			public const string Default = "DefaultTitle";

			/// <summary>
			/// The heading style for title.
			/// </summary>
			/// <param name="index">The index.</param>
			/// <returns></returns>
			public static string Heading(int index)
			{
				index = index <= 0 ? 1 : index >= 6 ? 6 : index;
				return PdfStyleNames.Title._heading + index;
			}

			/// <summary>
			/// The header style for tile.
			/// </summary>
			public const string Header = "HeaderTitle";
		}

		/// <summary>
		/// Styles for paragraphs.
		/// </summary>
		public static class Paragraph
		{
			/// <summary>
			/// The default style for a paragraph.
			/// </summary>
			public const string Default = MigraDoc.DocumentObjectModel.StyleNames.DefaultParagraphFont;
			/// <summary>
			/// The small style for a paragraph.
			/// </summary>
			public const string Small = "ParagraphSmall";
		}

		/// <summary>
		/// Styles for lists.
		/// </summary>
		public static class List
		{
			private const string _unorderedList = "UnorderedList";

			private const string _orderedList = "OrderedList";

			/// <summary>
			/// The unordered style for a list.
			/// </summary>
			/// <param name="index">The index.</param>
			/// <returns></returns>
			public static string UnorderedList(int index)
			{
				index = index <= 0 ? 1 : index;
				return PdfStyleNames.List._unorderedList + index;
			}

			/// <summary>
			/// The ordered style for a list.
			/// </summary>
			/// <param name="index">The index.</param>
			/// <returns></returns>
			public static string OrderedList(int index)
			{
				index = index <= 0 ? 1 : index;
				return PdfStyleNames.List._orderedList + index;
			}
		}

		/// <summary>
		/// Styles for tables.
		/// </summary>
		public static class Table
		{
			/// <summary>
			/// The thead style.
			/// </summary>
			public const string THead = "THeadTR";

			/// <summary>
			/// The tbody style.
			/// </summary>
			public const string TBody = "TBodyTD";

			/// <summary>
			/// The tfoot style.
			/// </summary>
			public const string TFoot = "TFootTD";
		}

		/// <summary>
		/// Miscellaneous styles.
		/// </summary>
		public static class Misc
		{
			/// <summary>
			/// The style for table of contents.
			/// </summary>
			public const string TableOfContents = "TOC";

			/// <summary>
			/// The horizontal rule style.
			/// </summary>
			public const string HorizontalRule = "HorizontalRule";
		}

	}
}