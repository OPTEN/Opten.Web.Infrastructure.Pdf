using PdfSharp.Pdf;

namespace Opten.Web.Infrastructure.Pdf.Interfaces
{
	/// <summary>
	/// A PDF Page.
	/// </summary>
	public interface IPdfPage
	{

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="document">The document.</param>
		void Render(IPdfStyling pdfStyling, PdfDocument document);

	}
}