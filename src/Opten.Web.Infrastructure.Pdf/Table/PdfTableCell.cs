using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;

namespace Opten.Web.Infrastructure.Pdf.Table
{
	/// <summary>
	/// A PDF Table cell.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfTableCell" />
	public class PdfTableCell : IPdfTableCell
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		public PdfTableCell(
			string text)
			: this(text, false) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="colspan">The colspan.</param>
		public PdfTableCell(
			string text,
			int colspan)
			: this(text, false, Alignment.Inherit, colspan) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		public PdfTableCell(
			string text,
			bool isBold)
			: this(text, isBold, Alignment.Inherit) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="alignment">The alignment.</param>
		public PdfTableCell(
			string text,
			Alignment alignment)
			: this(text, false, alignment, 1) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="colspan">The colspan.</param>
		public PdfTableCell(
			string text,
			Alignment alignment,
			int colspan)
			: this(text, false, alignment, colspan) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		/// <param name="alignment">The alignment.</param>
		public PdfTableCell(
			string text,
			bool isBold,
			Alignment alignment)
			: this(text, isBold, alignment, 1) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="colspan">The colspan.</param>
		public PdfTableCell(
			string text,
			bool isBold,
			Alignment alignment,
			int colspan)
		{
			this.Text = text;
			this.IsBold = isBold;
			this.Alignment = alignment;
			this.Colspan = colspan;
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text { get; private set; }

		/// <summary>
		/// Gets a value indicating whether this instance is bold.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is bold; otherwise, <c>false</c>.
		/// </value>
		public bool IsBold { get; private set; }

		//TODO: Alignment Vertical and Horizontal!

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

	}
}