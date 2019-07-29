using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using Opten.Web.Infrastructure.Pdf.Table;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Web.Infrastructure.Pdf.Extensions
{
	internal static class TableExtensions
	{

		internal static bool IsTHead(this Cell cell)
		{
			return cell.Style == PdfStyleNames.Table.THead;
		}

		internal static bool IsTBody(this Cell cell)
		{
			return cell.Style == PdfStyleNames.Table.TBody;
		}

		internal static bool IsTFoot(this Cell cell)
		{
			return cell.Style == PdfStyleNames.Table.TFoot;
		}

		internal static double[] CalculateColumnWidths(
			IPdfStyling pdfStyling,
			Document document,
			IEnumerable<IPdfTableHeaderCell> tHead,
			IEnumerable<PdfTableRow> tBody,
			bool fitToDocument,
			Font font = null)
		{
			// Only calculate when not all widths are set!
			if (tHead.Count() == tHead.Count(o => o.WidthInCentimeter > 0))
			{
				return tHead.Select(o => Unit.FromCentimeter(o.WidthInCentimeter).Point).ToArray();
			}

			if (font == null)
			{
				font = document.Styles[StyleNames.Normal].Font.Clone();
			}

			// Start calculating the widths
			TextMeasurement tm = new TextMeasurement(font);
			double maxWidth = pdfStyling.MaxWidthInPoints;
			double maxWidthPerColumn = maxWidth / tHead.Count();

			//TODO: Do we need that? It will even work if it's commented out...
			if (tHead.Any(o => o.WidthInCentimeter > 0))
			{
				double widths = tHead.Sum(o => Unit.FromCentimeter(o.WidthInCentimeter).Point);
				maxWidthPerColumn = (maxWidth - widths) / tHead.Count();
			}

			List<PdfTableColumn> columns = new List<PdfTableColumn>();
			PdfTableColumn column;
			IPdfTableCell cell;
			double width, colWidth;
			for (int i = 0; i < tHead.Count(); i++)
			{
				column = new PdfTableColumn();

				if (tHead.ElementAt(i).WidthInCentimeter > 0)
				{
					column.Width = Unit.FromCentimeter(tHead.ElementAt(i).WidthInCentimeter).Point;
				}
				else
				{
					// Measure header text to get the width of the column
					// http://forum.pdfsharp.net/viewtopic.php?f=2&t=747
					width = tHead.ElementAt(i).Text == null
								? 0
								: tm.MeasureString(tHead.ElementAt(i).Text.Trim()).Width;

					// but we also have to check the body text width
					foreach (PdfTableRow row in tBody)
					{
						cell = i < row.Cells.Count ? row.Cells[i] : null;

						if (cell == null) continue;

						//TODO: This is a QUICK HACK! We should calculate the real width of the colspan!
						if (cell.Colspan > 1) continue;

						colWidth = cell.Text == null
									? 0
									: tm.MeasureString(cell.Text.Trim()).Width;

						if (colWidth > width) width = colWidth;
					}

					if (width > maxWidthPerColumn)
					{
						// Add the maximum width per column
						column.Width = maxWidthPerColumn;
						column.Fit = false; // But if we have more space left we will add it later...
					}
					else
					{
						// We add 10 to be sure we have enough
						column.Width = width + 10;
					}
				}

				columns.Add(column);
			}

			double currentWidth = columns.Sum(o => o.Width);

			if (fitToDocument && currentWidth < maxWidth)
			{
				double available = 0;

				// If there is a column that doesn't fit - make it fit (first priority)
				// otherwise we will fit the full table (second priority)
				if (columns.Any(o => o.Fit == false))
				{
					available = (maxWidth - currentWidth) / columns.Count(o => o.Fit == false);
					foreach (PdfTableColumn col in columns.Where(o => o.Fit == false))
					{
						col.Width += available;
					}
				}
				else
				{
					available = (maxWidth - currentWidth) / columns.Count();
					foreach (PdfTableColumn col in columns)
					{
						col.Width += available;
					}
				}
			}

			return columns.Select(o => o.Width).ToArray();
		}

	}
}