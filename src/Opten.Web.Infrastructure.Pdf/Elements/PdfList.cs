using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The List type.
	/// </summary>
	public enum PdfListType
	{
		/// <summary>
		/// An unordered list
		/// </summary>
		UnorderedList,
		/// <summary>
		/// An ordered list (like 1. 2. 3.)
		/// </summary>
		OrderedList
	}

	/// <summary>
	/// The List element.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfList : IPdfElement
	{

		private readonly IEnumerable<TextLine> _textLines;

		private readonly PdfListType _listType;

		private readonly HyperlinkType _hyperlinkType;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfList"/> class.
		/// </summary>
		/// <param name="textLines">The text lines.</param>
		/// <param name="listType">Type of the list.</param>
		public PdfList(IEnumerable<TextLine> textLines, PdfListType listType)
		{
			_textLines = textLines;
			_listType = listType;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfList"/> class.
		/// </summary>
		/// <param name="textLines">The text lines.</param>
		/// <param name="listType">Type of the list.</param>
		/// <param name="hyperlinkType">Type of the hyperlink.</param>
		public PdfList(IEnumerable<TextLine> textLines, PdfListType listType, HyperlinkType hyperlinkType)
		{
			_textLines = textLines;
			_listType = listType;
			_hyperlinkType = hyperlinkType;
		}

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			RenderList(section, _textLines);
		}

		private void RenderList(Section section, IEnumerable<TextLine> textLines)
		{
			string listStyle = _listType == PdfListType.UnorderedList
								? "UnorderedList"
								: "OrderedList";

			int level = 1; //TODO: Make multiple levels possible
			listStyle += level;

			TextLine line;
			for (int i = 0; i < textLines.Count(); i++)
			{
				line = textLines.ElementAt(i);

				Paragraph listItem = section.AddParagraph().SetStyle(listStyle);

				//TODO: Nicer way? E.g. .Render() returns the element instead of directly adding it?!
				//	    So we have more power to use the rendered element and have less code?! Like the HTML Converter from SIHF.
				if (_hyperlinkType == HyperlinkType.Web)
				{
					listItem.AddHyperlink(name: line.Text, type: _hyperlinkType);
				}

				//TODO: Extension > AsFormattedText?
				listItem.AddFormattedText(
					text: line.Text,
					textFormat: line.IsBold ? TextFormat.Bold : TextFormat.NotBold);

				listItem.Format.ListInfo.ContinuePreviousList = i > 0;
			}
		}
	}
}