using MigraDoc.DocumentObjectModel;

namespace Opten.Web.Infrastructure.Pdf.Interfaces
{
	/// <summary>
	/// A PDF Element.
	/// </summary>
	public interface IPdfElement
	{

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		void Render(IPdfStyling pdfStyling, Section section);

	}
}