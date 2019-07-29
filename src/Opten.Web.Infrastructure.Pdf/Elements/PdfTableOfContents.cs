using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using System.Collections.Generic;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Table of contents element.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfTableOfContents : IPdfElement
	{

		private readonly IEnumerable<PdfBookmark> _bookmarks;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableOfContents"/> class.
		/// </summary>
		/// <param name="bookmarks">The bookmarks.</param>
		public PdfTableOfContents(IEnumerable<PdfBookmark> bookmarks)
		{
			_bookmarks = bookmarks;
		}

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			RenderList(section, _bookmarks);

			section.AddPageBreak();
		}

		private void RenderList(Section section, IEnumerable<PdfBookmark> hyperlinks)
		{
			Paragraph paragraph;
			Hyperlink hyperlink;
			foreach (PdfBookmark link in hyperlinks)
			{
				paragraph = section.AddParagraph();
				paragraph.SetStyle(PdfStyleNames.Misc.TableOfContents + link.Level);

				hyperlink = paragraph.AddHyperlink(link.Name, HyperlinkType.Bookmark);
				hyperlink.AddText(link.Name);
				hyperlink.AddTab();
				hyperlink.AddPageRefField(link.RefName);

				RenderList(section, link.Children);
			}
		}
	}
}