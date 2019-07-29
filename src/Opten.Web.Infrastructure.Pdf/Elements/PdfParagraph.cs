using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Paragraph element.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfParagraph : IPdfElement
	{

		private readonly IEnumerable<TextLine> _paragraphs;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfParagraph"/> class.
		/// </summary>
		/// <param name="paragraphs">The paragraphs.</param>
		public PdfParagraph(IEnumerable<TextLine> paragraphs)
		{
			_paragraphs = paragraphs;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfParagraph"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		public PdfParagraph(string text)
		{
			_paragraphs = new TextLine[] { new TextLine(text: text, isBold: false) };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfParagraph"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		public PdfParagraph(string text, bool isBold)
		{
			_paragraphs = new TextLine[] { new TextLine(text: text, isBold: isBold) };
		}

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			Paragraph paragraph = section.AddParagraph();

			TextLine line;
			for (int i = 0; i < _paragraphs.Count(); i++)
			{
				line = _paragraphs.ElementAt(i);

				if (i > 0)
				{
					paragraph.AddLineBreak();
				}

				//TODO: Extension > AsFormattedText?
				paragraph.AddFormattedText(
					text: line.Text,
					textFormat: line.IsBold ? TextFormat.Bold : TextFormat.NotBold);

				//paragraph.AddLineBreak();
			}
		}
	}
}