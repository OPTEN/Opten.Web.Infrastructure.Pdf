using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Page number element
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfPageNumber : IPdfElement
	{

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			section.PageSetup.StartingNumber = 1;

			TextFrame frame = section.Footers.Primary.AddTextFrame();

			frame.Width = new Unit(pdfStyling.MaxWidthInPoints);
			frame.Height = new Unit(10); //TODO: Why?
			frame.MarginTop = pdfStyling.PageNumberMarginTop; //TODO: Why?
			//frame.MarginTop = new Unit(69.5);

			Paragraph paragraph = frame.AddParagraph();
			paragraph.Format.Alignment = pdfStyling.PageNumberAlignment;
			//paragraph.AddText("Page "); //TODO: Multilingual
			paragraph.AddPageField();
			paragraph.AddText(" / ");
			paragraph.AddNumPagesField();
			paragraph.SetStyle(PdfStyleNames.Paragraph.Small);
		}

	}
}