using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// Renders an adderss.
	/// </summary>
	public class PdfAddress : IPdfElement
	{

		private readonly string _title;
		private readonly IEnumerable<TextLine> _addressLines;
		private readonly int _top;
		private readonly int _left;
		//private readonly int _width;
		private readonly int _height;
		private readonly RelativeVertical _relativeVertical;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfAddress" /> class.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="addressLines">The address lines.</param>
		/// <param name="top">The top.</param>
		/// <param name="left">The left.</param>
		/// <param name="height">The height.</param>
		/// <param name="relativeVertical">The relative vertical.</param>
		public PdfAddress(
			string title,
			IEnumerable<TextLine> addressLines,
			int top = 0,
			int left = 0,
			//int width = 205,
			int height = 120,
			RelativeVertical relativeVertical = RelativeVertical.Line)
		{
			_title = title;
			_addressLines = addressLines;
			_top = top;
			_left = left;
			//_width = width;
			_height = height;
			_relativeVertical = relativeVertical;
		}

		/// <summary>
		/// Renders the specified PDF styling.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			TextFrame frame = section.AddTextFrame();

			frame.Top = new Unit(_top);
			frame.Left = new Unit(_left);
			frame.Width = pdfStyling.MaxWidthInPoints;
			frame.Height = new Unit(_height);
			//frame.RelativeVertical = RelativeVertical.Margin;
			frame.RelativeVertical = _relativeVertical;

			Paragraph paragraph = frame.AddParagraph();

			if (string.IsNullOrWhiteSpace(_title) == false)
			{
				paragraph.AddFormattedText(_title.Trim(), TextFormat.Bold);
				paragraph.AddLineBreak();
			}

			int count = _addressLines.Count();
			TextLine line;
			for (int i = 0; i < _addressLines.Count(); i++)
			{
				line = _addressLines.ElementAt(i);

				paragraph.AddLineBreak();
				paragraph.AddFormattedText(
					text: line.Text,
					textFormat: line.IsBold ? TextFormat.Bold : TextFormat.NotBold);
			}
		}
	}
}