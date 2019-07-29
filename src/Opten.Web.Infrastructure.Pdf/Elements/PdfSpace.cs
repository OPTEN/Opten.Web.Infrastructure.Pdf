using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Interfaces;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// An element to make spaces in the desired size.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfSpace : IPdfElement
	{

		private readonly int _spaceBefore;
		private readonly int _spaceAfter;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfSpace"/> class.
		/// </summary>
		/// <param name="spaceBefore">The space before.</param>
		/// <param name="spaceAfter">The space after.</param>
		public PdfSpace(int spaceBefore, int spaceAfter)
		{
			_spaceBefore = spaceBefore;
			_spaceAfter = spaceAfter;
		}

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			Paragraph paragraph = section.AddParagraph();
			paragraph.Format.SpaceBefore = _spaceBefore;
			paragraph.Format.SpaceAfter = _spaceAfter;
		}
	}
}