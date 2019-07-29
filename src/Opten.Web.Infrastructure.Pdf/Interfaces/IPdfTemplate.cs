using Opten.Web.Infrastructure.Pdf;

namespace Opten.Web.Infrastructure.Pdf.Interfaces
{
	/// <summary>
	/// The PDF Template.
	/// </summary>
	public interface IPdfTemplate : IPdfGenerator
	{

		/// <summary>
		/// Defines the document.
		/// </summary>
		/// <param name="title">The document title.</param>
		/// <param name="author">The document author.</param>
		/// <param name="subject">The document subject.</param>
		/// <param name="keywords">The document keywords.</param>
		/// <param name="absolutePathToPdfTemplate">The absolute path to PDF template.</param>
		/// <param name="pdfStyling">The PDF styling.</param>
		void Define(
			string title,
			string author,
			string subject,
			string keywords,
			string absolutePathToPdfTemplate = null,
			IPdfStyling pdfStyling = null);

		/// <summary>
		/// The file name.
		/// </summary>
		/// <returns></returns>
		string FileName();

	}
}