using Opten.Web.Infrastructure.Pdf.Interfaces;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;

namespace Opten.Web.Infrastructure.Pdf.Pages
{
	/// <summary>
	/// Add other pdf to the document.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfPage" />
	public class PdfConcatenate : IPdfPage
	{

		private readonly string _filePath;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfConcatenate"/> class.
		/// </summary>
		/// <param name="filePath">The path of the pdf which will be concatenated.</param>
		public PdfConcatenate(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));

			_filePath = filePath;
		}

		/// <summary>
		/// Renders the pages.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="document">The document.</param>
		public void Render(IPdfStyling pdfStyling, PdfDocument document)
		{
			try
			{
				PdfDocument pdf = PdfReader.Open(_filePath, PdfDocumentOpenMode.Import);

				for (int i = 0; i < pdf.PageCount; i++)
				{
					PdfPage page = pdf.Pages[i];
					
					document.AddPage(page);
				}
			}
			catch
			{
				//LOG?!
			}
		}

	}
}
