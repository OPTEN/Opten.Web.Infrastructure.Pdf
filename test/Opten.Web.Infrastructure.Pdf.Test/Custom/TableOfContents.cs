using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Web.Infrastructure.Pdf.Test.Custom
{
	public class TableOfContents : IPdfElement
	{

		public void Render(IPdfStyling pdfStyling, Section section)
		{
			Unit height = Unit.FromCentimeter(3) + Unit.FromCentimeter(0.7 * 10); // 3cm from top and bottom + cm per line

			TextFrame space = section.AddTextFrame();
			space.Height = ((pdfStyling.MaxHeightInPoints - height) / 2) - Unit.FromCentimeter(1); // -1cm from margin-top of horizontal rule
			space.Width = pdfStyling.MaxHeightInPoints;

			HorizontalRule(pdfStyling, section);

			Paragraph h2 = section.AddParagraph();
			h2.AddText("INHALTSVERZEICHNIS");
			h2.Format.Font.Size = 12;
			h2.Format.SpaceBefore = 0;
			h2.Format.SpaceAfter = Unit.FromCentimeter(0.5);
			h2.Format.LeftIndent = Unit.FromCentimeter(1);
			h2.Format.Alignment = ParagraphAlignment.Left;

			MigraDoc.DocumentObjectModel.Tables.Table table = section.AddTable();

			table.TopPadding = 0;
			table.RightPadding = 0;
			table.BottomPadding = 0;
			table.LeftPadding = 0;
			table.Borders.Width = 0;

			table.Rows.LeftIndent = Unit.FromCentimeter(1);
			table.AddColumn(pdfStyling.MaxWidthInPoints / 2);
			table.AddColumn(pdfStyling.MaxWidthInPoints / 2);

			Row row = table.AddRow();

			IDictionary<int, string> pages = new Dictionary<int, string>
			{
				{ 1, "frische Babyananas" },
				{ 2, "frische Babyananas" },
				{ 3, "frische Babyananas" },
				{ 4, "frische Babyananas" },
				{ 5, "frische Babyananas" },
				{ 6, "frische Babyananas" },
				{ 7, "frische Babyananas" },
				{ 8, "frische Babyananas" },
				{ 9, "frische Babyananas" },
				{ 10, "frische Babyananas" },
				{ 11, "frische Babyananas" },
				{ 12, "frische Babyananas" },
				{ 13, "frische Babyananas" },
				{ 14, "frische Babyananas" },
				{ 15, "frische Babyananas" },
				{ 16, "frische Babyananas" },
				{ 17, "frische Babyananas" },
				{ 18, "frische Babyananas" },
				{ 19, "frische Babyananas" },
				{ 20, "frische Babyananas" }
			};

			decimal numberOfGroups = 2;
			int counter = 0;
			int groupSize = Convert.ToInt32(Math.Ceiling(pages.Count / numberOfGroups));

			var result = pages.GroupBy(x => counter++ / groupSize);

			int cellIndex = 0;
			Font font;
			Paragraph paragraph;
			Hyperlink hyperlink;
			foreach (IEnumerable<KeyValuePair<int, string>> group in result)
			{
				foreach (KeyValuePair<int, string> page in group)
				{
					paragraph = row.Cells[cellIndex].AddParagraph();
					paragraph.Format.Font.Size = 10;
					paragraph.Format.Font.Color = pdfStyling.ParagraphSmallColor;
					if (cellIndex == 1)
					{
						paragraph.Format.LeftIndent = Unit.FromCentimeter(1);
					}

					font = paragraph.Format.Font.Clone();
					font.Color = pdfStyling.FontColor;

					paragraph.Format.AddTabStop("2.5cm", TabAlignment.Left, TabLeader.Lines);

					hyperlink = paragraph.AddHyperlink("Receipt1");
					hyperlink.AddFormattedText(page.Key.ToString(), font);
					hyperlink.AddTab();
					hyperlink.AddFormattedText(page.Value, font);
				}
				cellIndex++;
			}

			HorizontalRule(pdfStyling, section);

			Footer(pdfStyling, section);

			section.AddPageBreak();
		}

		private void Footer(IPdfStyling pdfStyling, Section section)
		{
			TextFrame footer = section.AddTextFrame();
			footer.RelativeVertical = RelativeVertical.Margin;

			footer.Height = Unit.FromCentimeter(3.5);
			footer.Width = pdfStyling.MaxWidthInPoints + Unit.FromCentimeter(0.15);
			footer.Top = pdfStyling.MaxHeightInPoints - Unit.FromCentimeter(1);

			Paragraph p = footer.AddParagraph();
			p.AddText("Alle Rezepte auf");
			p.Format.SpaceBefore = 0;
			p.Format.SpaceAfter = 0;
			p.Format.Font.Color = pdfStyling.ParagraphSmallColor;
			p.Format.Alignment = ParagraphAlignment.Center;
			p.Format.LeftIndent = Unit.FromCentimeter(-0.15); // Because we have +1cm for the page

			Paragraph website = footer.AddParagraph();
			website.AddText("www.rimuss.ch");
			website.Format.SpaceBefore = 0;
			website.Format.SpaceAfter = 0;
			website.Format.Font.Color = Color.FromCmyk(38, 46, 82, 36);
			website.Format.Alignment = ParagraphAlignment.Center;
			website.Format.LeftIndent = Unit.FromCentimeter(-0.15); // Because we have +1cm for the page

			Paragraph page = footer.AddParagraph();
			page.AddText("3");
			page.Format.Font.Size = 9;
			page.Format.SpaceBefore = Unit.FromCentimeter(0.5);
			page.Format.SpaceAfter = 0;
			page.Format.Alignment = ParagraphAlignment.Right;
		}

		private void HorizontalRule(IPdfStyling pdfStyling, Section section)
		{
			TextFrame hr = section.AddTextFrame();
			hr.Width = pdfStyling.MaxWidthInPoints;
			hr.Height = Unit.FromCentimeter(1);
			hr.Top = Unit.FromCentimeter(1);

			Paragraph ruler = hr.AddParagraph();
			ruler.Format.Borders.Top.Color = pdfStyling.HorizontalRuleColor;
			ruler.Format.Borders.Top.Width = 0.5;
		}

	}
}