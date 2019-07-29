using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Heading element.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfHeading : IPdfElement
	{

		private readonly string _text;
		/*private readonly int _top; // You can do spacings with PdfSpace!
		private readonly int _left;*/
		private readonly string _style;
		//private readonly RelativeVertical _relativeVertical;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfHeading"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="headingIndex">Index of the heading.</param>
		public PdfHeading(string text, int headingIndex)
			: this(text, string.Empty)
		{
			this._style = PdfStyleNames.Title.Heading(headingIndex);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfHeading"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="style">The style.</param>
		public PdfHeading(string text, string style)
		{
			this._text = text;
			this._style = style;
		}

		/*public PdfHeading(
			string text,
			string style)
			: this(text, 0, 10, style, RelativeVertical.Paragraph) { }*/

		/*public PdfHeading(
			string text,
			int top,
			int left,
			string style,
			RelativeVertical relativeVertical)
		{
			_text = text;
			_top = top;
			_left = left;
			_style = style;
			_relativeVertical = relativeVertical;
		}*/

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			/*TextFrame frame = section.AddTextFrame();

			frame.Width = new Unit(pdfStyling.MaxWidthInPoints);
			frame.Height = new Unit(50); //TODO: Better way

			if (_relativeVertical == RelativeVertical.Paragraph)
			{
				frame.MarginTop = new Unit(_top);
				//frame.MarginBottom = new Unit(_left);
			}
			else if (_relativeVertical == RelativeVertical.Page)
			{
				frame.Top = new Unit(_top);
				frame.Left = new Unit(_left);
			}

			frame.RelativeVertical = _relativeVertical;*/

			Paragraph paragraph = section.AddParagraph();
			paragraph.AddText(_text);
			if (string.IsNullOrWhiteSpace(_style) == false) paragraph.SetStyle(_style);
		}

	}
}