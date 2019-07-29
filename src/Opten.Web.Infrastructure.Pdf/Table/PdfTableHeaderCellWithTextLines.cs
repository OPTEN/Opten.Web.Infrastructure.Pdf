using Opten.Web.Infrastructure.Pdf.Elements;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using System.Collections.Generic;

namespace Opten.Web.Infrastructure.Pdf.Table
{
	/// <summary>
	/// A PDF Table header cell with text lines.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Table.PdfTableHeaderCell" />
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfTableHeaderCell" />
	public class PdfTableHeaderCellWithTextLines : PdfTableHeaderCell, IPdfTableHeaderCell, IPdfTableCellWithTextLines
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCellWithTextLines"/> class.
		/// </summary>
		/// <param name="textLines">The text lines.</param>
		public PdfTableHeaderCellWithTextLines(
			IEnumerable<TextLine> textLines)
			: this(textLines, Alignment.Inherit) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCellWithTextLines"/> class.
		/// </summary>
		/// <param name="textLines">The text lines.</param>
		/// <param name="alignment">The alignment.</param>
		public PdfTableHeaderCellWithTextLines(
			IEnumerable<TextLine> textLines,
			Alignment alignment)
			: this(textLines, alignment, 0) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCellWithTextLines"/> class.
		/// </summary>
		/// <param name="textLines">The text lines.</param>
		/// <param name="widthInCentimeter">The width in centimeter.</param>
		public PdfTableHeaderCellWithTextLines(
			IEnumerable<TextLine> textLines,
			double widthInCentimeter)
			: this(textLines, Alignment.Inherit, widthInCentimeter) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCellWithTextLines"/> class.
		/// </summary>
		/// <param name="textLines">The text lines.</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="widthInCentimeter">The width in centimeter.</param>
		public PdfTableHeaderCellWithTextLines(
			IEnumerable<TextLine> textLines,
			Alignment alignment,
			double widthInCentimeter)
			: this(textLines, alignment, 1, widthInCentimeter) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCellWithTextLines"/> class.
		/// </summary>
		/// <param name="textLines">The text lines.</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="colspan">The colspan.</param>
		/// <param name="widthInCentimeter">The width in centimeter.</param>
		public PdfTableHeaderCellWithTextLines(
			IEnumerable<TextLine> textLines,
			Alignment alignment,
			int colspan,
			double widthInCentimeter)
			: base(
				textLines.Longest().Text, // This is for the calculation
				textLines.Longest().IsBold, // This is for the calculation
				alignment,
				colspan,
				widthInCentimeter)
		{
			this.TextLines = textLines;
		}

		/// <summary>
		/// Gets or sets the text lines.
		/// </summary>
		/// <value>
		/// The text lines.
		/// </value>
		public IEnumerable<TextLine> TextLines { get; set; }

	}
}