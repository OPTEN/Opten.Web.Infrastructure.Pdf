using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using Opten.Web.Infrastructure.Pdf.Table;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The PDF Table element.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfTable : IPdfElement
	{

		private readonly int _spaceBefore;
		private readonly int _spaceAfter;
		private readonly TableStyle _style;
		private readonly bool _fitToDocument;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTable"/> class.
		/// </summary>
		/// <param name="fitToDocument">if set to <c>true</c> [fit table to document].</param>
		public PdfTable(bool fitToDocument) : this(TableStyle.Bordered, fitToDocument) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTable" /> class.
		/// </summary>
		/// <param name="style">The style.</param>
		/// <param name="fitToDocument">if set to <c>true</c> [fit table to document].</param>
		public PdfTable(
			TableStyle style,
			bool fitToDocument)
			: this(0, 0, style, fitToDocument) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTable"/> class.
		/// </summary>
		/// <param name="spaceBefore">The space before.</param>
		/// <param name="spaceAfter">The space after.</param>
		/// <param name="style">The style.</param>
		/// <param name="fitToDocument">if set to <c>true</c> [fit table to document].</param>
		public PdfTable(
			int spaceBefore,
			int spaceAfter,
			TableStyle style,
			bool fitToDocument)
		{
			_spaceBefore = spaceBefore;
			_spaceAfter = spaceAfter;
			_style = style;
			_fitToDocument = fitToDocument;

			this.THead = new List<IPdfTableHeaderCell>();
			this.TBody = new List<PdfTableRow>();
			this.TFoot = new List<PdfTableRow>();
		}

		/// <summary>
		/// Gets or sets the table head.
		/// </summary>
		/// <value>
		/// The t head.
		/// </value>
		public List<IPdfTableHeaderCell> THead { get; set; }

		/// <summary>
		/// Gets or sets the table body.
		/// </summary>
		/// <value>
		/// The t body.
		/// </value>
		public List<PdfTableRow> TBody { get; set; }

		/// <summary>
		/// Gets or sets the table foot.
		/// </summary>
		/// <value>
		/// The t foot.
		/// </value>
		public List<PdfTableRow> TFoot { get; set; }

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			MigraDoc.DocumentObjectModel.Tables.Table table = AddTable(
				pdfStyling: pdfStyling,
				section: section);

			switch (_style)
			{
				case TableStyle.Bordered:
					pdfStyling.TableStyling.BorderedStyle(table);
					break;
				case TableStyle.Grid:
					pdfStyling.TableStyling.GridStyle(table);
					break;
				case TableStyle.OnlyRowsBordered:
					pdfStyling.TableStyling.OnlyRowsBorderedStyle(table);
					break;
				case TableStyle.NoBorders:
					pdfStyling.TableStyling.NoBordersStyle(table);
					break;
				case TableStyle.StripedRows:
					pdfStyling.TableStyling.StripedRowsStyle(table);
					break;
				case TableStyle.Zebra:
					pdfStyling.TableStyling.ZebraStyle(table);
					break;
				case TableStyle.NoSpacing:
					pdfStyling.TableStyling.NoSpacingStyle(table);
					break;
			}
		}

		private MigraDoc.DocumentObjectModel.Tables.Table AddTable(IPdfStyling pdfStyling, Section section)
		{
			// This is a little bit faky, otherwise we can't add spaces before and after table
			if (this._spaceBefore > 0 || this._spaceAfter > 0)
			{
				IPdfElement pdfSpace = new PdfSpace(this._spaceBefore, this._spaceAfter);
				pdfSpace.Render(pdfStyling: pdfStyling, section: section);
			}

			// Add table to the section
			MigraDoc.DocumentObjectModel.Tables.Table table = section.AddTable();

			// This is a little bit faky, otherwise we can't add spaces before and after table
			if (this._spaceBefore > 0 || this._spaceAfter > 0)
			{
				IPdfElement pdfSpace = new PdfSpace(this._spaceBefore, this._spaceAfter);
				pdfSpace.Render(pdfStyling: pdfStyling, section: section);
			}

			//THead (and Columns)

			// Calculate widths if necessary
			double[] widths = TableExtensions.CalculateColumnWidths(
				pdfStyling: pdfStyling,
				document: section.Document,
				tHead: this.THead,
				tBody: this.TBody,
				fitToDocument: _fitToDocument);

			// Create columns from THead
			Column column;
			for (int c = 0; c < this.THead.Count; c++)
			{
				column = table.AddColumn(new Unit(widths[c]));
				column.Format.Alignment = this.THead[c].Alignment.GetAlignment();
			}

			// Create the header of the table
			Row thead;
			if (this.THead.Any(o => string.IsNullOrWhiteSpace(o.Text.Trim())) == false)
			{
				//HINT: If an error appears here because you did not added PdfTableHeaderCells -> you have to add but with empty strings :-)
				thead = table.AddRow();
				thead.TopPadding = Unit.FromPoint(5);
				//titleRow.BottomPadding = Unit.FromPoint(5);
				thead.HeadingFormat = true;

				for (int i = 0; i < this.THead.Count; i++)
				{
					AddText(
						pdfStyling: pdfStyling,
						pdfCell: this.THead[i],
						cell: thead.Cells[i]);

					thead.Cells[i].VerticalAlignment = VerticalAlignment.Top;
					thead.Cells[i].Style = PdfStyleNames.Table.THead;
				}
			}

			//TBody

			for (int i = 0; i < this.TBody.Count; i++)
			{
				AddCell(
					pdfStyling: pdfStyling,
					table: table,
					pdfRow: this.TBody[i],
					isTFoot: false);
			}

			//TFoot

			for (int i = 0; i < this.TFoot.Count; i++)
			{
				AddCell(
					pdfStyling: pdfStyling,
					table: table,
					pdfRow: this.TFoot[i],
					isTFoot: true);
			}

			return table;
		}

		private void AddCell(
			IPdfStyling pdfStyling,
			MigraDoc.DocumentObjectModel.Tables.Table table,
			PdfTableRow pdfRow,
			bool isTFoot)
		{
			// Each item fills one row
			Row row = table.AddRow();
			row.TopPadding = Unit.FromPoint(5);
			//row.BottomPadding = Unit.FromPoint(2);

			IPdfTableCell pdfCell;
			int pdfCellIndex, pdfCellIndexWithColspan;
			for (int j = 0; j < pdfRow.Cells.Count; j++)
			{
				pdfCellIndex = j;
				pdfCellIndexWithColspan = pdfCellIndex;
				pdfCell = pdfRow.Cells[j];

				if (pdfCellIndex > 0)
				{
					// If we have a colspan we have to update the index
					// http://www.pdfsharp.net/wiki/invoice-sample.ashx
					for (int y = 0; y < pdfCellIndex; y++)
					{
						if (pdfRow.Cells[y].Colspan > 1)
						{
							pdfCellIndexWithColspan += pdfRow.Cells[y].Colspan - 1;
						}
					}
				}

				AddText(
					pdfStyling: pdfStyling,
					pdfCell: pdfCell,
					cell: row.Cells[pdfCellIndexWithColspan]);

				row.Cells[pdfCellIndexWithColspan].VerticalAlignment = VerticalAlignment.Top;

				if (pdfCell.Alignment != Alignment.Inherit)
				{
					row.Cells[pdfCellIndexWithColspan].Format.Alignment = pdfCell.Alignment.GetAlignment();
				}

				if (pdfCell.Colspan > 1)
				{
					row.Cells[pdfCellIndexWithColspan].MergeRight = pdfCell.Colspan - 1;
				}

				if (isTFoot)
				{
					row.Cells[pdfCellIndexWithColspan].Style = PdfStyleNames.Table.TFoot;
				}
				else
				{
					row.Cells[pdfCellIndexWithColspan].Style = PdfStyleNames.Table.TBody;
				}
			}
		}

		private void AddText(IPdfStyling pdfStyling, IPdfTableCell pdfCell, Cell cell)
		{
			if (pdfCell is PdfTableCellWithSubtext)
			{
				AddTextWithSubtext(
					pdfStyling: pdfStyling,
					pdfCell: pdfCell as PdfTableCellWithSubtext,
					cell: cell);
			}
			else if (pdfCell is IPdfTableCellWithTextLines)
			{
				AddTextWithLines(
					pdfStyling: pdfStyling,
					pdfCell: pdfCell as IPdfTableCellWithTextLines,
					cell: cell);
			}
			else
			{
				cell.Add(pdfCell.Text.AsParagraph(pdfCell.IsBold));
			}
		}

		private void AddTextWithSubtext(IPdfStyling pdfStyling, PdfTableCellWithSubtext pdfCell, Cell cell)
		{
			double space = (pdfStyling as PdfStyling)._paragraphSpaceAfter / 2; //TODO: better way?

			if (pdfCell.SubtextAlignment == TextAlignment.Top)
			{
				cell.Add(pdfCell.Subtext.AsParagraphSmall());
			}

			Paragraph paragraph = pdfCell.Text.AsParagraph(pdfCell.IsBold);

			if (pdfCell.SubtextAlignment == TextAlignment.Top && space > 0)
			{
				// Half of paragraphs padding
				paragraph.Format.SpaceBefore = -space;
			}

			if (pdfCell.SubtextAlignment == TextAlignment.Left)
			{
				paragraph = new Paragraph(); // Empty the paragraph
				paragraph.AddFormattedText(pdfCell.Subtext.Trim(), PdfStyleNames.Paragraph.Small);
				paragraph.AddText(" ");
				paragraph.AddText(pdfCell.Text.Trim());
			}
			else if (pdfCell.SubtextAlignment == TextAlignment.Right)
			{
				paragraph.AddText(" ");
				paragraph.AddFormattedText(pdfCell.Subtext.Trim(), PdfStyleNames.Paragraph.Small);
			}

			cell.Add(paragraph);

			if (pdfCell.SubtextAlignment == TextAlignment.Bottom)
			{
				paragraph = pdfCell.Subtext.AsParagraphSmall();

				if (space > 0)
				{
					// Half of paragraphs padding
					paragraph.Format.SpaceBefore = -space;
				}

				cell.Add(paragraph);
			}
		}

		private void AddTextWithLines(IPdfStyling pdfStyling, IPdfTableCellWithTextLines pdfCell, Cell cell)
		{
			Paragraph paragraph;
			TextLine textLine;
			double space = (pdfStyling as PdfStyling)._paragraphSpaceAfter / 2; //TODO: better way?
			for (int i = 0; i < pdfCell.TextLines.Count(); i++)
			{
				textLine = pdfCell.TextLines.ElementAt(i);
				paragraph = textLine.Text.AsParagraph(textLine.IsBold);

				if (i > 0 && space > 0)
				{
					// Half of paragraphs padding
					paragraph.Format.SpaceBefore = -space;
				}

				cell.Add(paragraph);
			}
		}

	}
}