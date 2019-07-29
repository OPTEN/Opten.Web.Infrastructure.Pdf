using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using Opten.Web.Infrastructure.Pdf.Interfaces;

namespace Opten.Web.Infrastructure.Pdf.Test.Custom
{
	public class Cover : IPdfElement
	{

		public void Render(IPdfStyling pdfStyling, Section section)
		{
			Paragraph logoWrapper = section.AddParagraph();
			Image logo = logoWrapper.AddImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\logo_rimuss.png");
			logo.LockAspectRatio = true;
			logo.Height = Unit.FromCentimeter(2);
			logoWrapper.Format.SpaceBefore = Unit.FromCentimeter(4.2) - (pdfStyling.Margin[0] + Unit.FromCentimeter(0.5));
			logoWrapper.Format.SpaceAfter = Unit.FromCentimeter(8.8) + Unit.FromCentimeter(0.5);
			logoWrapper.Format.Alignment = ParagraphAlignment.Center;

			Image bottle1 = section.AddImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\1001_nacht.png");
			bottle1.LockAspectRatio = true;
			bottle1.Height = Unit.FromCentimeter(6);
			bottle1.Top = Unit.FromCentimeter(7.6) - pdfStyling.Margin[0];
			bottle1.Left = Unit.FromCentimeter(10) - pdfStyling.Margin[3];
			bottle1.RelativeVertical = RelativeVertical.Margin;

			Image bottle2 = section.AddImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\lavendel_traum.png");
			bottle2.LockAspectRatio = true;
			bottle2.Height = Unit.FromCentimeter(6);
			bottle2.Top = Unit.FromCentimeter(7.6) - pdfStyling.Margin[0];
			bottle2.Left = Unit.FromCentimeter(11.9) - pdfStyling.Margin[3];
			bottle2.RelativeVertical = RelativeVertical.Margin;

			Image bottle3 = section.AddImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\drink_winterbowle.jpg");
			bottle3.LockAspectRatio = true;
			bottle3.Height = Unit.FromCentimeter(6);
			bottle3.Top = Unit.FromCentimeter(7.6) - pdfStyling.Margin[0];
			bottle3.Left = Unit.FromCentimeter(13.8) - pdfStyling.Margin[3];
			bottle3.RelativeVertical = RelativeVertical.Margin;

			Image bottle4 = section.AddImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\igeho_rimberry.png");
			bottle4.LockAspectRatio = true;
			bottle4.Height = Unit.FromCentimeter(6);
			bottle4.Top = Unit.FromCentimeter(7.6) - pdfStyling.Margin[0];
			bottle4.Left = Unit.FromCentimeter(15.8) - pdfStyling.Margin[03];
			bottle4.RelativeVertical = RelativeVertical.Margin;

			Image bottle5 = section.AddImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\drink_pinkgrapefruit.jpg");
			bottle5.LockAspectRatio = true;
			bottle5.Height = Unit.FromCentimeter(6);
			bottle5.Top = Unit.FromCentimeter(7.6) - pdfStyling.Margin[0];
			bottle5.Left = Unit.FromCentimeter(17.8) - pdfStyling.Margin[3];
			bottle5.RelativeVertical = RelativeVertical.Margin;

			Paragraph h1 = section.AddParagraph();
			h1.AddText("ALKOHOLFREIE DRINK-REZEPTE");
			h1.Format.Font.Size = 16;
			h1.Format.SpaceBefore = 0;
			h1.Format.SpaceAfter = Unit.FromCentimeter(0.5);
			h1.Format.Alignment = ParagraphAlignment.Center;

			Paragraph p = section.AddParagraph();
			p.Format.Font.Color = pdfStyling.ParagraphSmallColor;
			p.Format.LineSpacing = Unit.FromCentimeter(0.5);
			p.Format.LineSpacingRule = LineSpacingRule.AtLeast;
			p.Format.SpaceAfter = Unit.FromCentimeter(-0.25); // Line spacing
			p.AddText("Abwechslungsreiche Drinks zum selber mixen.\nUnkompliziert und fein.");
			p.Format.Alignment = ParagraphAlignment.Center;

			Footer(pdfStyling, section);

			section.AddPageBreak();
		}

		private void Footer(IPdfStyling pdfStyling, Section section)
		{
			TextFrame footer = section.AddTextFrame();
			footer.RelativeVertical = RelativeVertical.Margin;

			footer.Height = Unit.FromCentimeter(3.5);
			footer.Width = pdfStyling.MaxWidthInPoints;
			footer.Top = pdfStyling.MaxHeightInPoints - Unit.FromCentimeter(1);

			Paragraph p = footer.AddParagraph();
			p.AddText("Alle Rezepte auf");
			p.Format.SpaceBefore = 0;
			p.Format.SpaceAfter = 0;
			p.Format.Font.Color = pdfStyling.ParagraphSmallColor;
			p.Format.Alignment = ParagraphAlignment.Center;

			Paragraph website = footer.AddParagraph();
			website.AddText("www.rimuss.ch");
			website.Format.SpaceBefore = 0;
			website.Format.SpaceAfter = 0;
			website.Format.Font.Color = Color.FromCmyk(38, 46, 82, 36);
			website.Format.Alignment = ParagraphAlignment.Center;
		}

	}
}