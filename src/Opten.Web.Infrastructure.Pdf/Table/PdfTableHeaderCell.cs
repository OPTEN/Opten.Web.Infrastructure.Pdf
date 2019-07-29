using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;

namespace Opten.Web.Infrastructure.Pdf.Table
{
	/// <summary>
	/// A PDF table header cell.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Table.PdfTableCell" />
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfTableCell" />
	public class PdfTableHeaderCell : PdfTableCell, IPdfTableHeaderCell
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell"/> class.
		/// </summary>
		/// <param name="alignment">The alignment.</param>
		public PdfTableHeaderCell(
			Alignment alignment)
			: this(alignment, 0)
		{
			// This is when you want to have empty header cells and just the columns
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell"/> class.
		/// </summary>
		/// <param name="alignment">The alignment.</param>
		/// <param name="widthInCentimeter">The width in centimeter.</param>
		public PdfTableHeaderCell(
			Alignment alignment,
			double widthInCentimeter)
			: this(string.Empty, true, alignment, widthInCentimeter)
		{
			// This is when you want to have empty header cells and just the columns
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		public PdfTableHeaderCell(
			string text)
			: this(text, true) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell" /> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="alignment">The alignment.</param>
		public PdfTableHeaderCell(
			string text,
			Alignment alignment)
			: this(text, alignment, 0) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="widthInCentimeter">The width in centimeter.</param>
		public PdfTableHeaderCell(
			string text,
			double widthInCentimeter)
			: this(text, Alignment.Inherit, widthInCentimeter) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="widthInCentimeter">The width in centimeter.</param>
		public PdfTableHeaderCell(
			string text,
			Alignment alignment,
			double widthInCentimeter)
			: this(text, true, alignment, widthInCentimeter) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		/// <param name="widthInCentimeter">The width in centimeter.</param>
		public PdfTableHeaderCell(
			string text,
			bool isBold,
			double widthInCentimeter)
			: this(text, isBold, Alignment.Inherit, widthInCentimeter) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		public PdfTableHeaderCell(
			string text,
			bool isBold)
			: this(text, isBold, Alignment.Inherit) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		/// <param name="alignment">The alignment.</param>
		public PdfTableHeaderCell(
			string text,
			bool isBold,
			Alignment alignment)
			: this(text, isBold, alignment, 1, 0) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="widthInCentimeter">The width in centimeter.</param>
		public PdfTableHeaderCell(
			string text,
			bool isBold,
			Alignment alignment,
			double widthInCentimeter)
			: this(text, isBold, alignment, 1, widthInCentimeter) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableHeaderCell"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="colspan">The colspan.</param>
		/// <param name="widthInCentimeter">The width in centimeter.</param>
		public PdfTableHeaderCell(
			string text,
			bool isBold,
			Alignment alignment,
			int colspan,
			double widthInCentimeter)
			: base(text, isBold, alignment, colspan)
		{
			this.WidthInCentimeter = widthInCentimeter;
		}

		/// <summary>
		/// Gets or sets the width in centimeter.
		/// </summary>
		/// <value>
		/// The width in centimeter.
		/// </value>
		public double WidthInCentimeter { get; set; }

	}
}