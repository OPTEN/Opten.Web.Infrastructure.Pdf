using Opten.Web.Infrastructure.Pdf.Elements;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using System.Collections.Generic;

namespace Opten.Web.Infrastructure.Pdf.Table
{
	/// <summary>
	/// A PDF Table cell with text lines.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfTableCell" />
	public class PdfTableCellWithTextLines : IPdfTableCell, IPdfTableCellWithTextLines
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCellWithTextLines"/> class.
		/// </summary>
		/// <param name="textLines">The text lines.</param>
		public PdfTableCellWithTextLines(
			IEnumerable<TextLine> textLines)
			: this(textLines, Alignment.Inherit) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCellWithTextLines"/> class.
		/// </summary>
		/// <param name="textLines">The text lines.</param>
		/// <param name="alignment">The alignment.</param>
		public PdfTableCellWithTextLines(
			IEnumerable<TextLine> textLines,
			Alignment alignment)
			: this(textLines, alignment, 1) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCellWithTextLines"/> class.
		/// </summary>
		/// <param name="textLines">The text lines.</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="colspan">The colspan.</param>
		public PdfTableCellWithTextLines(
			IEnumerable<TextLine> textLines,
			Alignment alignment,
			int colspan)
		{
			this.TextLines = textLines;
			this.Alignment = alignment;
			this.Colspan = colspan;
		}

		/// <summary>
		/// Gets or sets the text lines.
		/// </summary>
		/// <value>
		/// The text lines.
		/// </value>
		public IEnumerable<TextLine> TextLines { get; set; }

		/// <summary>
		/// Gets the alignment.
		/// </summary>
		/// <value>
		/// The alignment.
		/// </value>
		public Alignment Alignment { get; private set; }

		/// <summary>
		/// Gets the col span.
		/// </summary>
		/// <value>
		/// The col span.
		/// </value>
		public int Colspan { get; private set; }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text
		{
			get { return this.TextLines.Longest().Text; } // This is for the calculation
		}

		/// <summary>
		/// Gets a value indicating whether this instance is bold.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is bold; otherwise, <c>false</c>.
		/// </value>
		public bool IsBold
		{
			get { return this.TextLines.Longest().IsBold; } // This is for the calculation
		}

	}
}