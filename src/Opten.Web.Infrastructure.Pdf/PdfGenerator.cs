using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Opten.Web.Infrastructure.Pdf.Elements;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using PdfSharp.Pdf;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Opten.Web.Infrastructure.Pdf
{
	/// <summary>
	/// The PDF Generator.
	/// </summary>
	public class PdfGenerator : IPdfGenerator
	{

		#region Public properties

		/// <summary>
		/// Gets or sets the pdf elements.
		/// </summary>
		/// <value>
		/// The elements.
		/// </value>
		public List<IPdfElement> Elements { get; set; }

		/// <summary>
		/// Gets or sets the pdf pages.
		/// </summary>
		/// <value>
		/// The elements.
		/// </value>
		public List<IPdfPage> Pages { get; set; }

		#endregion

		#region Private fields

		private readonly string _title;
		private readonly string _author;
		private readonly string _subject;
		private readonly string _keywords;

		private readonly PdfBackground _background;

		private readonly IPdfStyling _pdfStyling;

		#endregion

		/// <summary>
		/// Generates a PDF.
		/// </summary>
		/// <param name="title">The document title.</param>
		/// <param name="author">The document author.</param>
		/// <param name="subject">The document subject.</param>
		/// <param name="keywords">The document keywords.</param>
		public PdfGenerator(
			string title,
			string author,
			string subject,
			string keywords)
			: this(title, author, subject, keywords, string.Empty)
		{ }

		/// <summary>
		/// Generates a PDF.
		/// </summary>
		/// <param name="title">The document title.</param>
		/// <param name="author">The document author.</param>
		/// <param name="subject">The document subject.</param>
		/// <param name="keywords">The document keywords.</param>
		/// <param name="absolutePathToPdfTemplate">The absolute path to PDF template.</param>
		public PdfGenerator(
			string title,
			string author,
			string subject,
			string keywords,
			string absolutePathToPdfTemplate)
			: this(title, author, subject, keywords, absolutePathToPdfTemplate, new PdfStyling())
		{ }

		/// <summary>
		/// Generates a PDF.
		/// </summary>
		/// <param name="title">The document title.</param>
		/// <param name="author">The document author.</param>
		/// <param name="subject">The document subject.</param>
		/// <param name="keywords">The document keywords.</param>
		/// <param name="pdfStyling">The PDF styling.</param>
		public PdfGenerator(
			string title,
			string author,
			string subject,
			string keywords,
			IPdfStyling pdfStyling)
			: this(title, author, subject, keywords, string.Empty, pdfStyling)
		{ }

		/// <summary>
		/// Generates a PDF.
		/// </summary>
		/// <param name="title">The document title.</param>
		/// <param name="author">The document author.</param>
		/// <param name="subject">The document subject.</param>
		/// <param name="keywords">The document keywords.</param>
		/// <param name="absolutePathToPdfTemplate">The absolute path to PDF template.</param>
		/// <param name="pdfStyling">The PDF styling.</param>
		public PdfGenerator(
			string title,
			string author,
			string subject,
			string keywords,
			string absolutePathToPdfTemplate,
			IPdfStyling pdfStyling)
		{
			if (string.IsNullOrWhiteSpace(absolutePathToPdfTemplate) == false)
			{
				_background = new PdfBackground(absoluteUrl: absolutePathToPdfTemplate);
			}

			this.Elements = new List<IPdfElement>();
			this.Pages = new List<IPdfPage>();

			_title = title;
			_author = author;
			_subject = subject;
			_keywords = keywords;

			_pdfStyling = pdfStyling;
		}

		/// <summary>
		/// Saves the PDF on disk.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public void SaveOnDisk(string filePath)
		{
			using (PdfDocument document = this.Render())
			{
				document.Save(path: filePath);
			}
		}

		/// <summary>
		/// Saves the PDF in memory.
		/// </summary>
		/// <returns></returns>
		public byte[] SaveInMemory()
		{
			using (PdfDocument document = this.Render())
			{
				using (MemoryStream ms = new MemoryStream())
				{
					document.Save(stream: ms, closeStream: false);
					return ms.ToArray();
				}
			}
		}

		/// <summary>
		/// Creates the document with styling.
		/// </summary>
		/// <returns></returns>
		public virtual Document CreateDocument()
		{
			// Create a new MigraDoc document
			Document document = new Document();

			document.DefaultPageSetup.PageFormat = _pdfStyling.PageFormat;
			document.DefaultPageSetup.Orientation = _pdfStyling.Orientation;

			document.DefaultPageSetup.TopMargin = _pdfStyling.Margin[0];
			document.DefaultPageSetup.LeftMargin = _pdfStyling.Margin[3];
			document.DefaultPageSetup.RightMargin = _pdfStyling.Margin[1];
			document.DefaultPageSetup.BottomMargin = _pdfStyling.Margin[2];

			// Add a section to the document
			Section section = document.AddSection();

			// Add styling
			_pdfStyling.Define(document: document);

			// Add background
			if (_background != null)
			{
				_background.Render(headersFooters: section.Headers);
			}

			// Add elements
			foreach (IPdfElement element in this.Elements)
			{
				element.Render(
					pdfStyling: _pdfStyling,
					section: section);
			}

			// Add page number
			if (_pdfStyling.ShowPageNumber)
			{
				new PdfPageNumber().Render(
					pdfStyling: _pdfStyling,
					section: section);
			}

			return document;
		}

		protected virtual PdfDocument Render()
		{
			// Create a MigraDoc document 
			Document document = this.CreateDocument();

			// Add document infos
			document.Info.Title = _title;
			document.Info.Author = _author;
			document.Info.Subject = _subject;
			document.Info.Keywords = _keywords;

			// Set default styles (actually not needed)
			document.Styles.Normal.Font.Name = _pdfStyling.FontName;
			document.Styles.Normal.Font.Size = _pdfStyling.FontSize;
			document.Styles.Normal.Font.Color = _pdfStyling.FontColor;

			document.UseCmykColor = true;

			// ===== Unicode encoding and font program embedding in MigraDoc is demonstrated here ===== 

			// A flag indicating whether to create a Unicode PDF or a WinAnsi PDF file. 
			// This setting applies to all fonts used in the PDF document. 
			// This setting has no effect on the RTF renderer. 
			const bool unicode = false;

			// An enum indicating whether to embed fonts or not. 
			// This setting applies to all font programs used in the document. 
			// This setting has no effect on the RTF renderer. 
			// (The term 'font program' is used by Adobe for a file containing a font. Technically a 'font file' 
			// is a collection of small programs and each program renders the glyph of a character when executed. 
			// Using a font in PDFsharp may lead to the embedding of one or more font programms, because each outline 
			// (regular, bold, italic, bold+italic, ...) has its own fontprogram) 
			const PdfFontEmbedding embedding = PdfFontEmbedding.Always;

			// ======================================================================================== 

			// Create a renderer for the MigraDoc document. 
			PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode, embedding)
			{
				// Associate the MigraDoc document with a renderer 
				Document = document
			};

			// Layout and render document to PDF 
			pdfRenderer.RenderDocument();

			PdfDocument pdf = pdfRenderer.PdfDocument;

			// Add pages
			if (Pages != null && Pages.Any())
			{
				foreach (IPdfPage page in Pages)
				{
					page.Render(_pdfStyling, pdf);
				}
			}

			return pdf;
		}
	}
}