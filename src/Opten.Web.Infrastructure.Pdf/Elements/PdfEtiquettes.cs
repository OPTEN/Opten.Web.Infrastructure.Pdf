using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Etiquettes element.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfEtiquettes : IPdfElement
	{

		private readonly Unit _width;
		private readonly Unit _height;
		private readonly Unit[] _margin;
		private readonly Alignment _alignment;
		private readonly Color? _borderColor;
		private readonly IEnumerable<IEnumerable<TextLine>> _textLines;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfEtiquettes"/> class.
		/// </summary>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="alignment">The alignment.</param>
		/// <param name="textLines">The text lines.</param>
		/// <param name="margin">The margin.</param>
		/// <param name="borderColor">Color of the border.</param>
		public PdfEtiquettes(
			Unit width,
			Unit height,
			Alignment alignment,
			IEnumerable<IEnumerable<TextLine>> textLines,
			Unit[] margin,
			Color? borderColor)
		{
			_width = width;
			_height = height;
			_alignment = alignment;
			_margin = margin;
			_borderColor = borderColor;
			_textLines = textLines;
		}

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		/// <exception cref="System.NotImplementedException">The format:  X  is not implemented for generating Etiquettes.</exception>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			if (pdfStyling.PageFormat == PageFormat.A4 &&
				pdfStyling.Orientation == Orientation.Portrait)
			{
				int cells = (int)(21 / _width.Centimeter);
				int maxRows = (int)(29.7 / _height.Centimeter);

				int rows = (int)Math.Ceiling((double)_textLines.Count() / cells);

				int rowIndex = 0; // For absolute positioning
				int textLineIndex = 1;
				for (int r = 0; r < rows; r++)
				{
					for (int c = 0; c < cells; c++)
					{
						TextFrame frame = section.AddTextFrame();

						frame.Top = new Unit(rowIndex * _height);
						frame.Left = new Unit(c * _width);
						frame.Width = _width;
						frame.Height = _height;
						frame.RelativeVertical = RelativeVertical.Page;

						if (_margin != null && _margin.Length == 4)
						{
							frame.MarginTop = _margin[0];
							frame.MarginRight = _margin[1];
							frame.MarginBottom = _margin[2];
							frame.MarginLeft = _margin[3];
						}

						if (_borderColor.HasValue)
						{
							frame.LineFormat.Width = 0.5;
							frame.LineFormat.Color = _borderColor.Value;
						}

						if (_textLines.Count() >= textLineIndex)
						{
							IEnumerable<TextLine> textLines = _textLines.ElementAt(textLineIndex - 1);

							Paragraph paragraph;
							foreach (TextLine textLine in textLines)
							{
								paragraph = frame.AddParagraph();
								paragraph.Format.Alignment = _alignment.GetAlignment();

								// We add the space but not the text when it's null...
								if (textLine == null) continue;
								
								paragraph.AddFormattedText(
									text: textLine.Text,
									textFormat: textLine.IsBold ? TextFormat.Bold : TextFormat.NotBold);
							}
						}

						textLineIndex++;  //TODO: Calculateable?
					}

					rowIndex++;

					// Add new page if maximum reached
					if (rows > maxRows && (r + 1) % maxRows == 0)
					{
						section.AddPageBreak();
						rowIndex = 0; // Reset row to zero because new page's offset starts on top
					}
				}
			}
			else
			{
				throw new NotImplementedException("The format: " + pdfStyling.PageFormat + " is not implemented for generating Etiquettes.");
			}
		}

	}
}