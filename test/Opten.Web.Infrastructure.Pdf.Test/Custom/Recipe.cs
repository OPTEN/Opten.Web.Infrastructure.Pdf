using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using System.Collections.Generic;

namespace Opten.Web.Infrastructure.Pdf.Test.Custom
{
	public class Recipe : IPdfElement
	{

		private Unit _leftSideWidth { get { return _tableLeftWidth + _tableRightWidth; } }
		private Unit _tableLeftWidth = Unit.FromCentimeter(3.5);
		private Unit _tableRightWidth { get { return Unit.FromCentimeter(10) - _tableLeftWidth; } }

		public void Render(IPdfStyling pdfStyling, Section section)
		{
			LeftSide(pdfStyling, section);

			RightSide(pdfStyling, section);

			Footer(pdfStyling, section);
		}

		private void LeftSide(IPdfStyling pdfStyling, Section section)
		{
			Image bottle = section.AddImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\drink_winterbowle.jpg");
			bottle.LockAspectRatio = true;
			bottle.Height = pdfStyling.MaxHeightInPoints - (pdfStyling.Margin[0] * 2);
			bottle.Top = pdfStyling.Margin[0] - Unit.FromCentimeter(0.3); //TODO: Why 0.3?
			bottle.Left = pdfStyling.Margin[0] * 2;
			bottle.RelativeVertical = RelativeVertical.Margin;
		}

		private void RightSide(IPdfStyling pdfStyling, Section section)
		{
			MigraDoc.DocumentObjectModel.Tables.Table table = section.AddTable();

			table.TopPadding = 0;
			table.RightPadding = 0;
			table.BottomPadding = 0;
			table.LeftPadding = 0;
			table.Borders.Width = 0;

			table.Rows.LeftIndent = GetTableIndent(pdfStyling);
			table.AddColumn(_tableLeftWidth);
			table.AddColumn(_tableRightWidth);

			Row row = table.AddRow();

			Cell header = row.Cells[0];
			header.MergeRight = 1;

			Paragraph logoWrapper = header.AddParagraph();
			logoWrapper.Format.Alignment = ParagraphAlignment.Center;

			Image logo = logoWrapper.AddImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\logo_rimuss_secco.jpg");
			logo.LockAspectRatio = true;
			logo.Height = Unit.FromMillimeter(18);
			logoWrapper.Format.SpaceBefore = 0;
			logoWrapper.Format.SpaceAfter = Unit.FromCentimeter(1);

			Paragraph h1 = header.AddParagraph();
			h1.AddText("WINTER-BOWLE");
			h1.Format.Font.Size = 16;
			//h1.Format.Font.Bold = true;
			h1.Format.SpaceBefore = 0;
			h1.Format.SpaceAfter = Unit.FromCentimeter(0.5);
			h1.Format.Alignment = ParagraphAlignment.Center;

			h1.AddBookmark("Receipt1");
			h1.Format.OutlineLevel = OutlineLevel.Level1;

			TextFrame pWrapper = header.AddTextFrame();
			pWrapper.Height = Unit.FromCentimeter(1);
			pWrapper.Width = _leftSideWidth;
			Paragraph p = pWrapper.AddParagraph();
			p.Format.Font.Color = pdfStyling.ParagraphSmallColor;
			p.Format.LineSpacing = Unit.FromCentimeter(0.5);
			p.Format.LineSpacingRule = LineSpacingRule.AtLeast;
			p.Format.SpaceAfter = Unit.FromCentimeter(-0.25); // Line spacing
			p.AddText("Warum eigentlich eine Fruchtbowle nur im Sommer geniessen?\nDiese Bowleiessen?\nDiese Bowle verhilft auch an kalten und unliessen?\nDiese Bowle verhilft auch an kalten und unl verhilft auch an kalten und unlustigen Winterabenden zu\neiner beschwingten Leichtigkeit.");
			p.Format.Alignment = ParagraphAlignment.Center;

			HorizontalRule(pdfStyling, header);

			Paragraph h2 = header.AddParagraph();
			h2.AddText("ZUBEREITUNG FÜR 1 GLAS");
			h2.Format.Font.Size = 12;
			h2.Format.SpaceBefore = 0;
			h2.Format.SpaceAfter = Unit.FromCentimeter(0.5);
			h2.Format.LeftIndent = Unit.FromCentimeter(1);
			h2.Format.Alignment = ParagraphAlignment.Left;

			IDictionary<string, string> rows = new Dictionary<string, string> { 
				{ "frische Babyananas", "1" },
				{ "Kiwi", "1" },
				{ "Clementine, alle Früchte schneiden", "1" },
				{ "Zitrone, Saft", "1" },
				{ "Esslöffel Zucker, ales mischen", "1" },
				{ "Secco dazugiessen, zugedeckt", "7 dl" },
				{ "kühl stellen", "ca. 1 Std." },
				{ "kaltes Mineralwasser kurz vor dem Servieren dazugiessen", "3 dl" }			
			};

			Table(pdfStyling, table, rows);

			row = table.AddRow();

			Cell bottom = row.Cells[0];
			bottom.MergeRight = 1;

			Paragraph small = bottom.AddParagraph();
			small.AddText("Deko: 1 Schnitz Grapefruit");
			small.Format.SpaceBefore = Unit.FromCentimeter(0.5);
			small.Format.SpaceAfter = 0;
			small.Format.LeftIndent = Unit.FromCentimeter(1);
			small.Format.Font.Size = 10;
			small.Format.Font.Color = pdfStyling.ParagraphSmallColor;
			small.Format.Alignment = ParagraphAlignment.Left;

			HorizontalRule(pdfStyling, bottom);
		}

		private void Footer(IPdfStyling pdfStyling, Section section)
		{
			TextFrame footer = section.AddTextFrame();
			footer.RelativeVertical = RelativeVertical.Margin;

			footer.Height = Unit.FromCentimeter(3.5);
			footer.Width = _leftSideWidth + Unit.FromCentimeter(1);
			footer.Top = pdfStyling.MaxHeightInPoints - Unit.FromCentimeter(1);
			footer.Left = GetTableIndent(pdfStyling);

			Paragraph p = footer.AddParagraph();
			p.AddText("Alle Rezepte auf");
			p.Format.SpaceBefore = 0;
			p.Format.SpaceAfter = 0;
			p.Format.Font.Color = pdfStyling.ParagraphSmallColor;
			p.Format.Alignment = ParagraphAlignment.Center;
			p.Format.LeftIndent = Unit.FromCentimeter(-1); // Because we have +1cm for the page

			Paragraph website = footer.AddParagraph();
			website.AddText("www.rimuss.ch");
			website.Format.SpaceBefore = 0;
			website.Format.SpaceAfter = 0;
			website.Format.Font.Color = Color.FromCmyk(38, 46, 82, 36);
			website.Format.Alignment = ParagraphAlignment.Center;
			website.Format.LeftIndent = Unit.FromCentimeter(-1); // Because we have +1cm for the page
			
			Paragraph page = footer.AddParagraph();
			page.AddText("3");
			page.Format.Font.Size = 9;
			page.Format.SpaceBefore = Unit.FromCentimeter(0.5);
			page.Format.SpaceAfter = 0;
			page.Format.Alignment = ParagraphAlignment.Right;
		}

		private Unit GetTableIndent(IPdfStyling pdfStyling)
		{
			return (pdfStyling.MaxWidthInPoints / 2) + pdfStyling.Margin[3];
		}

		private void HorizontalRule(IPdfStyling pdfStyling, Cell cell)
		{
			TextFrame hr = cell.AddTextFrame();
			hr.Width = _leftSideWidth;
			hr.Height = Unit.FromCentimeter(1);
			hr.Top = Unit.FromCentimeter(1);

			Paragraph ruler = hr.AddParagraph();
			ruler.Format.Borders.Top.Color = pdfStyling.HorizontalRuleColor;
			ruler.Format.Borders.Top.Width = 0.5;
		}

		private void Table(IPdfStyling pdfStyling, MigraDoc.DocumentObjectModel.Tables.Table table, IDictionary<string, string> rows)
		{
			Row row;
			Font font;
			Paragraph paragraph;
			foreach (KeyValuePair<string, string> item in rows)
			{
				row = table.AddRow();

				row.Cells[0].Format.LeftIndent = Unit.FromCentimeter(1);

				paragraph = row.Cells[0].AddParagraph();
				paragraph.Format.Font.Size = 10;
				paragraph.Format.Font.Color = pdfStyling.ParagraphSmallColor;

				font = paragraph.Format.Font.Clone();
				font.Color = pdfStyling.FontColor;

				paragraph.Format.AddTabStop("3.5cm", TabAlignment.Left, TabLeader.Dots);

				paragraph.AddFormattedText(item.Value, font);
				paragraph.AddTab();

				paragraph = row.Cells[1].AddParagraph();
				paragraph.AddFormattedText(item.Key, font);
			}
		}

	}
}