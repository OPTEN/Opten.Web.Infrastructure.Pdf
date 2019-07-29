using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Elements;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using System.Collections.Generic;

namespace Opten.Web.Infrastructure.Pdf.Templates
{
	/// <summary>
	/// The PDF Template for Etiquettes.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Templates.PdfTemplateBase" />
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfTemplate" />
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfGenerator" />
	public class PdfTemplateEtiquettes : PdfTemplateBase, IPdfTemplate, IPdfGenerator
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTemplateEtiquettes"/> class.
		/// </summary>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="textLines">The text lines.</param>
		/// <param name="margin">The margin.</param>
		/// <param name="borderColor">Color of the border.</param>
		public PdfTemplateEtiquettes(
			Unit width,
			Unit height,
			Alignment alignment,
			IEnumerable<IEnumerable<TextLine>> textLines,
			Unit[] margin = null,
			Color? borderColor = null)
		{
			this.Elements.Add(new PdfEtiquettes(
				width: width,
				height: height,
				alignment: alignment,
				textLines: textLines,
				margin: margin,
				borderColor: borderColor));
		}

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
			if (pdfStyling == null)
			{
				pdfStyling = new PdfStyling(
					orientation: Orientation.Portrait,
					margin: new Unit[4] { 0, 0, 0, 0 },
					showPageNumber: false);
			}

			base.Define(title, author, subject, keywords, absolutePathToPdfTemplate, pdfStyling);
		}

	}
}