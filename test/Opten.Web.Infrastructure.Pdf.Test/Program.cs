using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Elements;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using Opten.Web.Infrastructure.Pdf.Table;
using Opten.Web.Infrastructure.Pdf.Templates;
using System;
using System.Collections.Generic;

namespace Opten.Web.Infrastructure.Pdf.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			//MemoryTest();
			//Headings();
			//Images();
			//PdfWithInvoice();
			//PdfWithTable();
			//PdfWithEmptyCells();
			//PdfLandscape();
			//PdfWithEtiquette();
			RobertSpleiss_AG();
			RobertSpleiss_AG_2();
			//Rimuss_Recipe();

			//new SimpleMailer("info@opten.ch", "info@opten.ch", "m.airó@bluewin.ch")
			//	.SendFromWebConfigSettings("test", "test");
		}

		static void MemoryTest()
		{
			IPdfGenerator generator = new PdfGenerator("Heading tests", "Heading tests", "Heading tests", "Heading tests");

			generator.Elements.Add(new PdfHeading("HEADING", 1));

			generator.Elements.Add(new PdfHorizontalRule());

			generator.Elements.Add(new PdfHeading("HEADING", 2));

			generator.Elements.Add(new PdfHorizontalRule());

			generator.Elements.Add(new PdfHeading("HEADING", 6));

			generator.Elements.Add(new PdfHorizontalRule());

			generator.SaveInMemory();
			generator.SaveInMemory();
			generator.SaveInMemory();
			generator.SaveInMemory();

			Console.ReadLine();
		}

		static void Headings()
		{
			IPdfGenerator generator = new PdfGenerator("Heading tests", "Heading tests", "Heading tests", "Heading tests");

			generator.Elements.Add(new PdfHeading("HEADING", 1));

			generator.Elements.Add(new PdfHorizontalRule());

			generator.Elements.Add(new PdfHeading("HEADING", 2));

			generator.Elements.Add(new PdfHorizontalRule());

			generator.Elements.Add(new PdfHeading("HEADING", 6));

			generator.Elements.Add(new PdfHorizontalRule());

			generator.SaveOnDisk(@"C:\Users\cfrei\Desktop\" + Guid.NewGuid() + ".pdf");
		}

		static void Images()
		{
			IPdfGenerator generator = new PdfGenerator("Image tests", "Image tests", "Image tests", "Image tests");


			//System.IO.FileStream fs = new System.IO.FileStream(path: @"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\reparaturloesungen.jpg", mode: System.IO.FileMode.Open, access: System.IO.FileAccess.Read);

			//generator.Elements.Add(new PdfImageFromStream(fs));
			generator.Elements.Add(new PdfHyperlink("http://www.opten.ch", MigraDoc.DocumentObjectModel.HyperlinkType.Web));
			generator.Elements.Add(new PdfParagraph("Ich bin ein text"));
			//generator.Elements.Add(new PdfImageFromStream(fs));
			//generator.Elements.Add(new PdfImageFromStream(fs));
			generator.Elements.Add(new PdfParagraph("Ich bin ein text"));
			generator.Elements.Add(new PdfParagraph("Ich bin ein text"));
			generator.Elements.Add(new PdfImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\reparaturloesungen.jpg", 215, 232));

			generator.Elements.Add(new PdfPageBreak());
			generator.Elements.Add(new PdfImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\404.jpg", 1043, 792));
			generator.Elements.Add(new PdfImage(@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tests\Opten.Web.Infrastructure.Pdf.Test\Images\404.jpg", 1043, 792));

			generator.Elements.Add(new PdfList(new TextLine[] { new TextLine("Hi"), new TextLine("Hi2"), new TextLine("hi3") }, PdfListType.OrderedList));
			generator.Elements.Add(new PdfList(new TextLine[] { new TextLine("Hi") }, PdfListType.UnorderedList));
			generator.Elements.Add(new PdfList(new TextLine[] { new TextLine("http://www.opten.ch"), new TextLine("http://www.opten.ch"), new TextLine("http://www.opten.ch") }, PdfListType.OrderedList, MigraDoc.DocumentObjectModel.HyperlinkType.Web));

			generator.Elements.Add(new PdfParagraph("Ich bin ein text"));
			generator.Elements.Add(new PdfLineBreak());
			generator.Elements.Add(new PdfParagraph("Ich bin ein text"));
			generator.Elements.Add(new PdfParagraph(string.Empty));
			generator.Elements.Add(new PdfParagraph("Ich bin ein text"));
			generator.Elements.Add(new PdfParagraph("Ich bin ein text"));
			generator.Elements.Add(new PdfParagraph("Ich bin ein text"));

			generator.SaveOnDisk(@"C:\Users\cfrei\Desktop\" + Guid.NewGuid() + ".pdf");
		}

		static void PdfWithInvoice()
		{
			// Company address
			List<TextLine> companyAddress = new List<TextLine>();
			companyAddress.Add(new TextLine("Autoersatzteilhandel und Zubehör", true));
			companyAddress.Add(new TextLine("Gebr. Knechtli AG", true));
			companyAddress.Add(new TextLine("Weststrasse 5"));
			companyAddress.Add(new TextLine("CH-5426 Lengnau AG"));
			companyAddress.Add(new TextLine("Tel.: +41 56 268 77 77"));
			companyAddress.Add(new TextLine("Fax: +41 56 241 04 54"));
			companyAddress.Add(new TextLine("E-Mail: info@gebr-knechtli.ch"));

			// Billed to address
			List<TextLine> billedTo = new List<TextLine>();
			billedTo.Add(new TextLine("Calvin Frei"));
			billedTo.Add(new TextLine("Moosweg 8"));
			billedTo.Add(new TextLine("5628 Aristau"));

			PdfTable tableDetails = new PdfTable(
				style: TableStyle.NoSpacing,
				fitToDocument: false);

			tableDetails.THead.Add(new PdfTableHeaderCell(Alignment.Left));
			tableDetails.THead.Add(new PdfTableHeaderCell(Alignment.Left));

			tableDetails.TBody.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("Rechnungsdatum:"),
					new PdfTableCell(DateTime.Now.ToString("dd.MM.yyyy"))
				}
			});

			tableDetails.TBody.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("Fälligkeitsdatum:"),
					new PdfTableCell(DateTime.Now.AddDays(30).ToString("dd.MM.yyyy"))
				}
			});

			tableDetails.TBody.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("Bestellnummer:"),
					new PdfTableCell("235424234")
				}
			});

			tableDetails.TBody.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("Kundennummer:"),
					new PdfTableCell("34234")
				}
			});

			tableDetails.TBody.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("MwSt. Nr.:"),
					new PdfTableCell("23424515-123")
				}
			});

			tableDetails.TBody.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("Zahlungskonditionen:"),
					new PdfTableCell("innerhalb 30 Tage")
				}
			});

			tableDetails.TBody.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("Währung:"),
					new PdfTableCell("CHF")
				}
			});

			// Table
			PdfTable tableSummary = new PdfTable(
				style: TableStyle.StripedRows,
				fitToDocument: true);

			// THead
			tableSummary.THead.Add(new PdfTableHeaderCell("Artikel", true, Alignment.Left));
			tableSummary.THead.Add(new PdfTableHeaderCell("Artikelnr.", true, Alignment.Left));
			tableSummary.THead.Add(new PdfTableHeaderCell("Anzahl", true, Alignment.Center));
			tableSummary.THead.Add(new PdfTableHeaderCell("Preis", true, Alignment.Right));
			tableSummary.THead.Add(new PdfTableHeaderCell("Total", true, Alignment.Right));

			// TBody

			tableSummary.TBody.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCellWithSubtext("iPhone 3G", "16GB, Swisscom"),
					new PdfTableCell("GK-13445"),
					new PdfTableCell("1"),
					new PdfTableCell("CHF 1'000.00"),
					new PdfTableCell("CHF 1'000.00")
				}
			});

			tableSummary.TBody.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("iPhone 5S"),
					new PdfTableCell("GK-65653"),
					new PdfTableCell("1"),
					new PdfTableCell("CHF 1'000.00"),
					new PdfTableCell("CHF 1'000.00")
				}
			});

			tableSummary.TBody.Add(new PdfTableRow()); // Empty table row

			// TFoot
			tableSummary.TFoot.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("Zwischentotal", Alignment.Right, 4),
					new PdfTableCell("CHF 2'000.00")
				}
			});

			tableSummary.TFoot.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("Versandkosten", Alignment.Right, 4),
					new PdfTableCell("CHF 25.00")
				}
			});

			tableSummary.TFoot.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("Zahlungsaufwand", Alignment.Right, 4),
					new PdfTableCell("CHF 0.00")
				}
			});

			tableSummary.TFoot.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("MwSt. (8%)", Alignment.Right, 4),
					new PdfTableCell("CHF 80.00")
				}
			});

			tableSummary.TFoot.Add(new PdfTableRow
			{
				Cells =
				{
					new PdfTableCell("Total inkl. MwSt.", true, Alignment.Right, 4),
					new PdfTableCell("CHF 2'105.00", true)
				}
			});

			List<TextLine> details = new List<TextLine>();

			details.Add(new TextLine("Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."));

			IPdfTemplate template = new PdfTemplateInvoice(
				"12",
				"Rechnung Nr. 12",
				companyAddress,
				string.Empty,
				billedTo,
				tableDetails,
				tableSummary,
				details);

			template.Define(
				title: "Invoice Number 12",
				author: "Calvin Frei",
				subject: "Invoice Number 12",
				keywords: "Invoice",/*,
				@"D:\DEVELOPMENT\GIT\OPTEN Solutions\tools\Pdf\pdf_template.pdf"*/
				pdfStyling: new PdfStyling(
					fontName: "Arial",
					fontSize: 8));

			// Create PDF
			template.SaveOnDisk(@"C:\Users\cfrei\Desktop\" + template.FileName());
		}

		static void PdfWithTable()
		{
			// Table
			PdfTable table = new PdfTable(
				style: TableStyle.OnlyRowsBordered,
				fitToDocument: true);

			// THead
			table.THead.Add(new PdfTableHeaderCell("Nummer", Alignment.Left, 2.0));
			table.THead.Add(new PdfTableHeaderCell("Name", Alignment.Left, 4.0));
			table.THead.Add(new PdfTableHeaderCell("Position", Alignment.Left, 2.0));
			table.THead.Add(new PdfTableHeaderCell("GP", Alignment.Center, 2.0));
			table.THead.Add(new PdfTableHeaderCell("G", Alignment.Center, 2.0));
			table.THead.Add(new PdfTableHeaderCell("A", Alignment.Center, 2.0));
			table.THead.Add(new PdfTableHeaderCell("PTS", Alignment.Center, 2.0));
			table.THead.Add(new PdfTableHeaderCell("PIM", Alignment.Center, 2.0));

			// TBody
			PdfTableRow row = new PdfTableRow();

			row.Add(new PdfTableCell("1"));
			row.Add(new PdfTableCell("Aebischer David"));
			row.Add(new PdfTableCell("Goalkeeper"));
			row.Add(new PdfTableCell("65"));
			row.Add(new PdfTableCell("0"));
			row.Add(new PdfTableCell("0"));
			row.Add(new PdfTableCell("0"));
			row.Add(new PdfTableCell("14"));

			table.TBody.Add(row);

			row = new PdfTableRow();

			row.Add(new PdfTableCell("1"));
			row.Add(new PdfTableCell("Aeschlimann Jean-Jaques"));
			row.Add(new PdfTableCell("Forwarder"));
			row.Add(new PdfTableCell("120"));
			row.Add(new PdfTableCell("20"));
			row.Add(new PdfTableCell("15"));
			row.Add(new PdfTableCell("35"));
			row.Add(new PdfTableCell("24"));

			table.TBody.Add(row);

			IPdfTemplate template = new PdfTemplateWithHeaderAndHeading();

			template.Define(
				title: "U20 Junior National Team 2 2 2 2 2 2 2 2 2 2",
				author: "Doc Author",
				subject: "PLAYER STATISTICS - Season 2014 / 2015",
				keywords: "Doc Keywords",
				absolutePathToPdfTemplate: @"D:\DEVELOPMENT\GIT\OPTEN Solutions\tools\Pdf\pdf_template.pdf");

			template.Elements.Add(table);

			// Create
			template.SaveOnDisk(@"C:\Users\cfrei\Desktop\" + template.FileName());
		}

		static void PdfWithEmptyCells()
		{
			// Table
			PdfTable table = new PdfTable(
				style: TableStyle.OnlyRowsBordered,
				fitToDocument: true);

			table.THead.Add(new PdfTableHeaderCell("Thema / Sujet", Alignment.Left, 7));
			table.THead.Add(new PdfTableHeaderCell("Bemerkung / Remarques", Alignment.Left, 10));
			table.TBody.Add(new PdfTableRow
			{
				Cells = {
					new PdfTableCell("Parkplatz, Fahrzeug / Place de parking, véhicule"),
					new PdfTableCell(null)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = {
					new PdfTableCell("Garderobe bewacht / Vestiaire surveillé"),
					new PdfTableCell(null)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = {
					new PdfTableCell("Weg Fahrzeug – Garderobe /    chemin véhicule - vestiaire"),
					new PdfTableCell(null)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = {
					new PdfTableCell("Weg Garderobe - Eisfeld / chemin vestiaire - surface de glace"),
					new PdfTableCell(null)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = {
					new PdfTableCell("Zuschauer auf dem Eis / Spectacteurs sur la glace"),
					new PdfTableCell(null)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = {
					new PdfTableCell("Beleuchtung, Beschallung / Eclairage, Sonorisation"),
					new PdfTableCell(null)
				}
			});

			IPdfTemplate template = new PdfTemplateBasic();

			template.Define(
				title: "Empty Cells Table",
				author: "Doc Author",
				subject: "Empty Cells",
				keywords: "Doc Keywords");

			template.Elements.Add(table);

			// Create
			template.SaveOnDisk(@"C:\Users\cfrei\Desktop\" + template.FileName());
		}

		static void PdfLandscape()
		{
			IPdfTemplate template = new PdfTemplateBasic();

			PdfStyling styling = new PdfStyling(
				orientation: Orientation.Landscape,
				margin: new Unit[4] { Unit.FromCentimeter(2), Unit.FromCentimeter(2), Unit.FromCentimeter(2), Unit.FromCentimeter(2) },
				pageNumberAlignment: ParagraphAlignment.Center,
				pageNumberMarginTop: 20);

			template.Define("Heading tests", "Heading tests", "Heading tests", "Heading tests", null, styling);

			template.Elements.Add(new PdfHeading("HEADING", 1));

			template.SaveOnDisk(@"C:\Users\cfrei\Desktop\" + Guid.NewGuid() + ".pdf");
		}

		static void PdfWithEtiquette()
		{
			// Herma 4615 - 70x37
			// A4 = 21cm x 29.7cm

			IPdfTemplate template = new PdfTemplateEtiquettes(
				width: Unit.FromMillimeter(70),
				height: Unit.FromMillimeter(37),
				alignment: Alignment.Left,
				textLines: new TextLine[][] {
					new TextLine[] { new TextLine("Herr Calvin Frei", true), new TextLine("OPTEN AG", false), new TextLine("Herostrasse 9", false), new TextLine("8048 Altstetten", false) },
					new TextLine[] { new TextLine("Herr Severin d'Heureuse", true), new TextLine("OPTEN AG", false), new TextLine("Herostrasse 9", false), new TextLine("8048 Altstetten", false) },
					new TextLine[] { new TextLine("Frau Mary Waldvogel", true), new TextLine("OPTEN AG", false), new TextLine("Herostrasse 9", false), new TextLine("8048 Altstetten", false) },
					new TextLine[] { new TextLine("Zu Händen von Herrn Tobias Morf", true), new TextLine("OPTEN AG", false), new TextLine("Herostrasse 9", false), new TextLine("8048 Altstetten", false) }
				},
				margin: new Unit[4] { Unit.FromMillimeter(5), Unit.FromMillimeter(5), Unit.FromMillimeter(5), Unit.FromMillimeter(5) },
				borderColor: Colors.Black);

			template.Define("Etiketten Herma 4615", "Etiketten Herma 4615", "Etiketten Herma 4615", "Etiketten Herma 4615");

			template.SaveOnDisk(@"C:\Users\cfrei\Desktop\etiketten.pdf");
		}

		static void RobertSpleiss_AG()
		{
			IPdfTemplate template = new PdfTemplateBasic();

			template.Define("Robert Spleiss AG", "Robert Spleiss AG", "Firmen mit Geschenkliste", "firmen, geschenkliste, firmen geschenkliste",
				pdfStyling: new PdfStyling(
					fontName: "Arial",
					headerTitleFontSize: 10,
					headerTitleColor: Colors.Black,
					fontSize: 6,
					orientation: Orientation.Landscape,
					margin: new Unit[4] { Unit.FromCentimeter(3.5), Unit.FromCentimeter(2.5), Unit.FromCentimeter(2), Unit.FromCentimeter(2.5) }));

			template.Elements.Add(new PdfHeaderTitle("ROBERT SPLEISS AG", ParagraphAlignment.Left));
			template.Elements.Add(new PdfHeaderTitle("Firmen mit Geschenkliste: Sekretärin / 2013", ParagraphAlignment.Left, TextFormat.Bold));

			PdfTable table = new PdfTable(
				fitToDocument: true,
				style: TableStyle.Grid);

			table.THead.Add(new PdfTableHeaderCell("Name", true, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("Adresse 1", true, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("Adresse 2", true, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("PLZ", true, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("Ort", true, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("BRF", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("KRT", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("SK", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("R12", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("R6", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("W6", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("W3", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("SA", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("KS", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("KB", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("CH", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("BL", true, Alignment.Center));
			table.THead.Add(new PdfTableHeaderCell("SONSTIGES", true, Alignment.Left));

			table.TBody.Add(new PdfTableRow
			{
				Cells = {
					new PdfTableCell("AMAG Automobil + Moteren A"),
					new PdfTableCell("Liegenschaftsverwaltung"),
					new PdfTableCell("Utoquai 49"),
					new PdfTableCell("8008"),
					new PdfTableCell("Zürich"),
					new PdfTableCell(null),
					new PdfTableCell(null),
					new PdfTableCell("1"),
					new PdfTableCell(null),
					new PdfTableCell(null),
					new PdfTableCell(null),
					new PdfTableCell(null),
					new PdfTableCell(null),
					new PdfTableCell("1"),
					new PdfTableCell(null),
					new PdfTableCell(null),
					new PdfTableCell(null),
					new PdfTableCell("M")
				}
			});

			template.Elements.Add(table);

			template.SaveOnDisk(@"C:\Users\cfrei\Desktop\robert_spleiss_ag.pdf");
		}

		static void RobertSpleiss_AG_2()
		{
			IPdfTemplate template = new PdfTemplateBasic();

			template.Define("Robert Spleiss AG", "Robert Spleiss AG", "Firmen mit Geschenkliste", "firmen, geschenkliste, firmen geschenkliste",
				pdfStyling: new PdfStyling(
					fontName: "Arial",
					headerTitleFontSize: 10,
					headerTitleColor: Colors.Black,
					fontSize: 6,
					orientation: Orientation.Landscape,
					margin: new Unit[4] { Unit.FromCentimeter(3.5), Unit.FromCentimeter(1), Unit.FromCentimeter(2), Unit.FromCentimeter(1) }));

			template.Elements.Add(new PdfHeaderTitle("ROBERT SPLEISS AG", ParagraphAlignment.Left));
			template.Elements.Add(new PdfHeaderTitle("Firmen mit Geschenkliste: Sekretärin / 2013", ParagraphAlignment.Left, TextFormat.Bold));

			PdfTable table = new PdfTable(
				style: TableStyle.OnlyRowsBordered,
				fitToDocument: true);

			table.THead.Add(new PdfTableHeaderCell("O-Nr.", false, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("O-Eingang", false, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("Abt.", false, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("O-Eingabe", false, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("Obj-Art", false, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("Verantw.", false, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("Objektbezeichnung", false, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCellWithTextLines(new TextLine[] { new TextLine("Eing. Datum"), new TextLine("Eing. Summe") }, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("Baubeginn", false, Alignment.Left));
			table.THead.Add(new PdfTableHeaderCell("Bemerkung", false, 6));

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("Werubau AG, Matthias Petter", 6),
					new PdfTableCell("043 844 20 90", 4)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("Werubau AG, Matthias Petter", 6),
					new PdfTableCell("043 844 20 90", 4)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("Werubau AG, Matthias Petter", 6),
					new PdfTableCell("043 844 20 90", 4)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("Werubau AG, Matthias Petter", 6),
					new PdfTableCell("043 844 20 90", 4)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("Werubau AG, Matthias Petter", 6),
					new PdfTableCell("043 844 20 90", 4)
				}
			});
			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("Werubau AG, Matthias Petter", 6),
					new PdfTableCell("043 844 20 90", 4)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			table.TBody.Add(new PdfTableRow
			{
				Cells = new List<IPdfTableCell>
				{
					new PdfTableCell("O17450", true),
					new PdfTableCell("29.10.2015", true),
					new PdfTableCell("NB", true),
					new PdfTableCell("13.11.2015", true),
					new PdfTableCell("Pb", true),
					new PdfTableCell("MF", true),
					new PdfTableCell("Freudenbergstrasse 108, 8044 Zürich", true),
					new PdfTableCellWithTextLines(textLines: new TextLine[] { new TextLine("25.11.2015", true), new TextLine("1'338'600", true) }),
					new PdfTableCell("Ende Mai 2016"),
					new PdfTableCell(null)
				}
			});

			template.Elements.Add(table);

			var memory = template.SaveInMemory();

			template.SaveOnDisk(@"C:\Users\cfrei\Desktop\robert_spleiss_ag_2.pdf");
		}

		static void Rimuss_Recipe()
		{
			IPdfTemplate template = new PdfTemplateBasic();

			PdfStyling styling = new PdfStyling(
				fontName: "Times New Roman",
				fontSize: 9,
				fontColor: Color.FromCmyk(84, 73, 62, 92),
				paragraphSmallColor: Color.FromCmyk(21, 15, 16, 1),
				horizontalRuleColor: Color.FromCmyk(21, 15, 16, 1),
				orientation: Orientation.Landscape,
				margin: new Unit[4] { Unit.FromCentimeter(1.5), Unit.FromCentimeter(2), Unit.FromCentimeter(1.5), Unit.FromCentimeter(2) },
				showPageNumber: false);

			template.Define("Rimuss Rezepte", "Rimuss Rezepte", "Rimuss Rezepte", "Rimuss Rezepte", null, styling);

			template.Elements.Add(new Opten.Web.Infrastructure.Pdf.Test.Custom.Cover());
			template.Elements.Add(new Opten.Web.Infrastructure.Pdf.Test.Custom.TableOfContents());
			template.Elements.Add(new Opten.Web.Infrastructure.Pdf.Test.Custom.Recipe());
			template.Elements.Add(new PdfPageBreak());
			template.Elements.Add(new Opten.Web.Infrastructure.Pdf.Test.Custom.Recipe());

			template.SaveOnDisk(@"C:\Users\cfrei\Desktop\rimuss_recipe.pdf");
		}

	}
}
