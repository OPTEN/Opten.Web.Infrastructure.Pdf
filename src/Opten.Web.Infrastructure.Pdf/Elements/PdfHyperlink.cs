using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Interfaces;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Hyperlink element.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfHyperlink : IPdfElement
	{

		private readonly string _text;

		private readonly HyperlinkType _hyperlinkType;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfHyperlink"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="hyperlinkType">Type of the hyperlink.</param>
		public PdfHyperlink(string text, HyperlinkType hyperlinkType)
		{
			_text = text;
			_hyperlinkType = hyperlinkType;
		}

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			Paragraph paragraph = section.AddParagraph();

			Hyperlink hyperlink = paragraph.AddHyperlink(this._text, this._hyperlinkType);
			hyperlink.AddText(this._text);
		}

	}
}