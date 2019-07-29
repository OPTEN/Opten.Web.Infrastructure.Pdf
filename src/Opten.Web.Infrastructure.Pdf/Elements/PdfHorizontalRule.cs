using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Horizontal rule element.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfHorizontalRule : IPdfElement
	{

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			TextFrame hr = section.AddTextFrame();
			hr.Width = new Unit(pdfStyling.MaxWidthInPoints);
			hr.Height = new Unit(10); //TODO: Custom height?
			hr.RelativeVertical = RelativeVertical.Line;

			Paragraph ruler = hr.AddParagraph();
			ruler.SetStyle(PdfStyleNames.Misc.HorizontalRule);
		}

	}
}