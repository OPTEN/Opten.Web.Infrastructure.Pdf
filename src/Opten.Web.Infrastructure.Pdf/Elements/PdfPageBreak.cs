using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Interfaces;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Page Break element (to start a new page).
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfPageBreak : IPdfElement
	{

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			section.AddPageBreak();
		}

	}
}