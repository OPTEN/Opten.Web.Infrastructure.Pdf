using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Header title element.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfHeaderTitle : IPdfElement
	{

		private readonly string _title;
		private readonly ParagraphAlignment _alignment;
		private readonly TextFormat _textFormat;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfHeaderTitle" /> class.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="format">The text format.</param>
		public PdfHeaderTitle(string title, ParagraphAlignment alignment, TextFormat format = TextFormat.NotBold)
		{
			_title = title;
			_alignment = alignment;
			_textFormat = format;
		}

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			TextFrame frame = section.Headers.Primary.AddTextFrame();

			frame.Width = new Unit(pdfStyling.MaxWidthInPoints);
			frame.Height = new Unit(pdfStyling.HeaderTitleFontSize * 2); //TODO: Why?
			frame.MarginTop = new Unit(pdfStyling.HeaderTitleFontSize + 3); //TODO: Why?

			Paragraph paragraph = frame.AddParagraph();
			
			paragraph.AddFormattedText(_title, _textFormat);
			paragraph.Format.Alignment = _alignment;
			paragraph.SetStyle(PdfStyleNames.Title.Header);
		}

	}
}