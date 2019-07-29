using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Elements;
using Opten.Web.Infrastructure.Pdf.Interfaces;

namespace Opten.Web.Infrastructure.Pdf.Templates
{

	/// <summary>
	/// The PDF Template with Header and Heading.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Templates.PdfTemplateBasic" />
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfTemplate" />
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfGenerator" />
	public class PdfTemplateWithHeaderAndHeading : PdfTemplateBasic, IPdfTemplate, IPdfGenerator
	{

		/// <summary>
		/// Defines the PDF.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="author"></param>
		/// <param name="subject"></param>
		/// <param name="keywords"></param>
		/// <param name="absolutePathToPdfTemplate"></param>
		/// <param name="pdfStyling"></param>
		public override void Define(
			string title,
			string author,
			string subject,
			string keywords,
			string absolutePathToPdfTemplate = null,
			IPdfStyling pdfStyling = null)
		{
			base.Define(title, author, subject, keywords, absolutePathToPdfTemplate, pdfStyling);

			if (string.IsNullOrWhiteSpace(title) == false)
			{
				base.Elements.Add(new PdfHeaderTitle(title, ParagraphAlignment.Center));
			}

			if (title.Equals(subject) == false && string.IsNullOrWhiteSpace(subject) == false)
			{
				base.Elements.Add(new PdfHeading(subject, 6));

				base.Elements.Add(new PdfHorizontalRule());
			}
		}
	}

}