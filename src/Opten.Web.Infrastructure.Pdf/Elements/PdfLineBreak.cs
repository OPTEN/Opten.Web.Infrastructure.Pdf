using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Interfaces;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Line break element (like a %lt;br/%gt;). 
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfLineBreak : IPdfElement
	{

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			Paragraph paragraph = section.AddParagraph();
			paragraph.AddLineBreak(); //TODO: Necessary?
		}

	}
}