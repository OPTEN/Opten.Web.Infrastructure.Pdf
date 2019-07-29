using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;

namespace Opten.Web.Infrastructure.Pdf.Table
{
	/// <summary>
	/// A PDF Table cell with subtext.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Table.PdfTableCell" />
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfTableCell" />
	public class PdfTableCellWithSubtext : PdfTableCell, IPdfTableCell
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCellWithSubtext"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="subtext">The subtext.</param>
		public PdfTableCellWithSubtext(
			string text,
			string subtext)
			: this(text, subtext, TextAlignment.Bottom, false) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCellWithSubtext"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="subtext">The subtext.</param>
		/// <param name="subtextAlignment">The subtext alignment.</param>
		public PdfTableCellWithSubtext(
			string text,
			string subtext,
			TextAlignment subtextAlignment)
			: this(text, subtext, subtextAlignment, false) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCellWithSubtext"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="subtext">The subtext.</param>
		/// <param name="subtextAlignment">The subtext alignment.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		public PdfTableCellWithSubtext(
			string text,
			string subtext,
			TextAlignment subtextAlignment,
			bool isBold)
			: this(text, subtext, subtextAlignment, isBold, Alignment.Inherit) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCellWithSubtext"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="subtext">The subtext.</param>
		/// <param name="subtextAlignment">The subtext alignment.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		/// <param name="alignment">The alignment.</param>
		public PdfTableCellWithSubtext(
			string text,
			string subtext,
			TextAlignment subtextAlignment,
			bool isBold,
			Alignment alignment)
			: this(text, subtext, subtextAlignment, isBold, alignment, 1) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableCellWithSubtext"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="subtext">The subtext.</param>
		/// <param name="subtextAlignment">The subtext alignment.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="colspan">The colspan.</param>
		public PdfTableCellWithSubtext(
			string text,
			string subtext,
			TextAlignment subtextAlignment,
			bool isBold,
			Alignment alignment,
			int colspan)
			: base(text, isBold, alignment, colspan)
		{
			this.Subtext = subtext;
			this.SubtextAlignment = subtextAlignment;
		}

		/// <summary>
		/// Gets or sets the subtext.
		/// </summary>
		/// <value>
		/// The subtext.
		/// </value>
		public string Subtext { get; set; }

		/// <summary>
		/// Gets or sets the subtext alignment.
		/// </summary>
		/// <value>
		/// The subtext alignment.
		/// </value>
		public TextAlignment SubtextAlignment { get; set; }

	}
}