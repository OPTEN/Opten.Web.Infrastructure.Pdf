using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using System;

namespace Opten.Web.Infrastructure.Pdf.Styles
{
	/// <summary>
	/// The PDF Styling.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfStyling" />
	public class PdfStyling : IPdfStyling
	{

		#region Page

		/// <summary>
		/// Gets the page format.
		/// </summary>
		/// <value>
		/// The page format.
		/// </value>
		public PageFormat PageFormat { get; protected set; }

		/// <summary>
		/// Gets the page orientation.
		/// </summary>
		/// <value>
		/// The orientation.
		/// </value>
		public Orientation Orientation { get; protected set; }

		/// <summary>
		/// Gets the page margin [top, right, bottom, left].
		/// </summary>
		/// <value>
		/// The margin.
		/// </value>
		public Unit[] Margin { get; protected set; }

		/// <summary>
		/// Gets a value indicating whether [show page number].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show page number]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowPageNumber { get; protected set; }

		/// <summary>
		/// Gets the page number alignment.
		/// </summary>
		/// <value>
		/// The page number alignment.
		/// </value>
		public ParagraphAlignment PageNumberAlignment { get; protected set; }

		/// <summary>
		/// Gets the page number margin top.
		/// </summary>
		/// <value>
		/// The page number margin top.
		/// </value>
		public int PageNumberMarginTop { get; protected set; }

		#endregion

		#region Units

		/// <summary>
		/// Gets the page width in centimeters.
		/// </summary>
		/// <value>
		/// The page width in centimeters.
		/// </value>
		/// <exception cref="System.NotImplementedException">The format:  + this.PageFormat +  is not implemented in Opten.Web.Infrastructure.Pdf.</exception>
		public double PageWidthInCentimeters
		{
			get
			{
				if (this.PageFormat == PageFormat.A4)
				{
					if (this.Orientation == Orientation.Portrait)
					{
						// Max width is 21cm
						return 21;
					}
					else
					{
						// Max width is 29,7cm
						return 29.7;
					}
				}

				throw new NotImplementedException("The format: " + this.PageFormat + " is not implemented in Opten.Web.Infrastructure.Pdf.");
			}
		}

		/// <summary>
		/// Gets the page height in centimeters.
		/// </summary>
		/// <value>
		/// The page height in centimeter.
		/// </value>
		/// <exception cref="System.NotImplementedException">The format:  + this.PageFormat +  is not implemented in Opten.Web.Infrastructure.Pdf.</exception>
		public double PageHeightInCentimeters
		{
			get
			{
				if (this.PageFormat == PageFormat.A4)
				{
					if (this.Orientation == Orientation.Portrait)
					{
						// Max height is 29,7cm
						return 29.7;
					}
					else
					{
						// Max height is 21cm
						return 21;
					}
				}

				throw new NotImplementedException("The format: " + this.PageFormat + " is not implemented in Opten.Web.Infrastructure.Pdf.");
			}
		}

		/// <summary>
		/// Gets the maximum width in points (page width - margin).
		/// </summary>
		/// <value>
		/// The maximum width in points.
		/// </value>
		public Unit MaxWidthInPoints
		{
			get
			{
				return Unit.FromCentimeter(this.MaxWidthInCentimeters);
			}
		}

		/// <summary>
		/// Gets the maximum height in points (page height - margin).
		/// </summary>
		/// <value>
		/// The maximum height in points.
		/// </value>
		public Unit MaxHeightInPoints
		{
			get
			{
				return Unit.FromCentimeter(this.MaxHeightInCentimeters);
			}
		}

		/// <summary>
		/// Gets the maximum width in centimeters (page width - margin).
		/// </summary>
		/// <value>
		/// The maximum width in centimeters.
		/// </value>
		public double MaxWidthInCentimeters
		{
			get
			{
				return this.PageWidthInCentimeters - Margin[1].Centimeter - Margin[3].Centimeter;
			}
		}

		/// <summary>
		/// Gets the maximum height in centimeters (page height - margin).
		/// </summary>
		/// <value>
		/// The maximum height in centimeters.
		/// </value>
		public double MaxHeightInCentimeters
		{
			get
			{
				return this.PageHeightInCentimeters - Margin[0].Centimeter - Margin[2].Centimeter;
			}
		}

		internal Unit _paragraphSpaceAfter = new Unit(6);

		#endregion

		#region Fonts, Colors, Tables, ...

		/// <summary>
		/// Gets the name of the font.
		/// </summary>
		/// <value>
		/// The name of the font.
		/// </value>
		public string FontName { get; protected set; }

		/// <summary>
		/// Gets the size of the font.
		/// </summary>
		/// <value>
		/// The size of the font.
		/// </value>
		public int FontSize { get; protected set; }

		/// <summary>
		/// Gets the font color.
		/// </summary>
		/// <value>
		/// The font color.
		/// </value>
		public Color FontColor { get; protected set; }

		/// <summary>
		/// Gets the size of the header title font.
		/// </summary>
		/// <value>
		/// The size of the header title font.
		/// </value>
		public int HeaderTitleFontSize { get; protected set; }

		/// <summary>
		/// Gets the color of the header title.
		/// </summary>
		/// <value>
		/// The color of the header title.
		/// </value>
		public Color HeaderTitleColor { get; protected set; }

		/// <summary>
		/// Gets the size of the paragraph small font.
		/// </summary>
		/// <value>
		/// The size of the paragraph small font.
		/// </value>
		public int ParagraphSmallFontSize { get; protected set; }

		/// <summary>
		/// Gets the color of the paragraph small.
		/// </summary>
		/// <value>
		/// The color of the paragraph small.
		/// </value>
		public Color ParagraphSmallColor { get; protected set; }

		/// <summary>
		/// Gets the color of the horizontal rule.
		/// </summary>
		/// <value>
		/// The color of the horizontal rule.
		/// </value>
		public Color HorizontalRuleColor { get; protected set; }

		/// <summary>
		/// Gets the table styling.
		/// </summary>
		/// <value>
		/// The table styling.
		/// </value>
		public IPdfTableStyling TableStyling { get; protected set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfStyling"/> class.
		/// </summary>
		/// <param name="fontName">Name of the font.</param>
		/// <param name="fontSize">Size of the font.</param>
		/// <param name="fontColor">Color of the font.</param>
		/// <param name="headerTitleFontSize">Size of the header title font.</param>
		/// <param name="headerTitleColor">Color of the header title.</param>
		/// <param name="paragraphSmallFontSize">Size of the paragraph small font.</param>
		/// <param name="paragraphSmallColor">Color of the paragraph small.</param>
		/// <param name="horizontalRuleColor">Color of the horizontal rule.</param>
		/// <param name="pageFormat">The page format.</param>
		/// <param name="orientation">The orientation.</param>
		/// <param name="margin">The margin.</param>
		/// <param name="showPageNumber">if set to <c>true</c> [show page number].</param>
		/// <param name="pageNumberAlignment">The page number alignment.</param>
		/// <param name="pageNumberMarginTop">The page number margin top.</param>
		/// <param name="tableStyling">The PDF table styling.</param>
		/// <exception cref="System.IndexOutOfRangeException">You have to define all 4 sites [top, right, bottom, left]</exception>
		public PdfStyling(
			string fontName = "Trebuchet MS",
			int fontSize = 10,
			Color? fontColor = null,
			int headerTitleFontSize = 18,
			Color? headerTitleColor = null,
			int paragraphSmallFontSize = 8,
			Color? paragraphSmallColor = null,
			Color? horizontalRuleColor = null,
			PageFormat pageFormat = PageFormat.A4,
			Orientation orientation = Orientation.Portrait,
			Unit[] margin = null,
			bool showPageNumber = true,
			ParagraphAlignment pageNumberAlignment = ParagraphAlignment.Right,
			int pageNumberMarginTop = 8,
			IPdfTableStyling tableStyling = null)
		{
			if (margin != null && margin.Length < 4)
				throw new IndexOutOfRangeException("You have to define all 4 sites [top, right, bottom, left]");

			this.FontName = fontName;
			this.FontSize = fontSize;
			this.FontColor = fontColor.HasValue ? fontColor.Value : Colors.Black;
			this.HeaderTitleFontSize = headerTitleFontSize;
			this.HeaderTitleColor = headerTitleColor.HasValue ? headerTitleColor.Value : new Color((byte)255, (byte)96, (byte)96, (byte)96);
			this.ParagraphSmallFontSize = paragraphSmallFontSize;
			this.ParagraphSmallColor = paragraphSmallColor.HasValue ? paragraphSmallColor.Value : new Color((byte)255, (byte)166, (byte)166, (byte)166);
			this.HorizontalRuleColor = horizontalRuleColor.HasValue ? horizontalRuleColor.Value : Colors.Black;

			this.PageFormat = pageFormat;
			this.Orientation = orientation;
			this.Margin = margin == null
				? new Unit[4] {
					Unit.FromCentimeter(2.5),
					Unit.FromCentimeter(2.5),
					Unit.FromCentimeter(2),
					Unit.FromCentimeter(2.5) } :
				margin;
			this.ShowPageNumber = showPageNumber;
			this.PageNumberAlignment = pageNumberAlignment;
			this.PageNumberMarginTop = pageNumberMarginTop;

			this.TableStyling = tableStyling == null ? new PdfTableStyling() : tableStyling;
		}

		#endregion

		/// <summary>
		/// Defines the document.
		/// </summary>
		/// <param name="document">The document.</param>
		public void Define(Document document)
		{
			Defaults(document);
			TableOfContents(document);
			Headings(document);
			Paragraphs(document);
			Lists(document);
			//Tables(document);
			Blocks(document);
		}

		#region Define document stylings

		private void Defaults(Document document)
		{
			// Get the predefined style Normal.
			Style style = document.Styles[PdfStyleNames.Normal];
			// Because all styles are derived from Normal, the next line changes the 
			// font of the whole document. Or, more exactly, it changes the font of
			// all styles and paragraphs that do not redefine the font.

			style.Font.Name = this.FontName;
			style.Font.Size = this.FontSize;
			style.Font.Color = this.FontColor;

			//style.ParagraphFormat.SpaceBefore = new Unit(12);
			style.ParagraphFormat.SpaceAfter = _paragraphSpaceAfter;

			style = document.Styles[PdfStyleNames.Header];
			style.ParagraphFormat.AddTabStop(this.MaxWidthInCentimeters + "cm", TabAlignment.Right);

			style = document.Styles[PdfStyleNames.Footer];
			style.ParagraphFormat.AddTabStop(this.MaxWidthInCentimeters / 2 + "cm", TabAlignment.Center);
		}

		private void TableOfContents(Document document)
		{
			Style style;
			for (int i = 1; i < 7; i++)
			{
				style = document.Styles.AddStyle(PdfStyleNames.Misc.TableOfContents + i, PdfStyleNames.Normal);
				style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
				style.ParagraphFormat.AddTabStop(this.MaxWidthInCentimeters + "cm", TabAlignment.Right, TabLeader.Dots);

				style.Font.Bold = i == 1;
			}

			/*style = document.Styles.AddStyle("TOCHidden", StyleNames.Normal);
			style.ParagraphFormat.Font.Size = 0.0001f;
			style.ParagraphFormat.SpaceBefore = 0;
			style.ParagraphFormat.Font.Color = Colors.White;*/
		}

		private void Headings(Document document)
		{
			Style style;
			for (int i = 1; i < 7; i++)
			{
				style = document.Styles[PdfStyleNames.Title.Heading(i)];

				style.ParagraphFormat.SpaceBefore = new Unit(_paragraphSpaceAfter.Point * 2);

				style.Font.Name = this.FontName;
				style.Font.Size = this.FontSize + (6 - i);
				style.Font.Color = this.FontColor;

				style.Font.Bold = true;
				/*if (i == 2) style.Font.Underline = Underline.Single;
				else style.Font.Underline = Underline.None;*/
			}

			style = document.Styles.AddStyle(PdfStyleNames.Title.Header, PdfStyleNames.Normal);
			style.ParagraphFormat.AddTabStop("1.5cm", TabAlignment.Left);
			style.Font.Size = this.HeaderTitleFontSize;
			style.Font.Color = this.HeaderTitleColor;
			style.Font.Bold = false;
		}

		private void Paragraphs(Document document)
		{
			Style style = document.Styles.AddStyle(PdfStyleNames.Paragraph.Default, PdfStyleNames.Normal);
			style.ParagraphFormat.Font.Name = this.FontName; //TODO: Why do we have to set it here again?

			style = document.Styles.AddStyle(PdfStyleNames.Paragraph.Small, PdfStyleNames.Paragraph.Default);
			style.ParagraphFormat.Font.Name = this.FontName; //TODO: Why do we have to set it here again?
			style.ParagraphFormat.Font.Size = this.ParagraphSmallFontSize;
			style.ParagraphFormat.Font.Color = this.ParagraphSmallColor;
			//style.ParagraphFormat.Alignment = ParagraphAlignment.Right;
		}

		private void Lists(Document document)
		{
			Style style = document.Styles.AddStyle(PdfStyleNames.List.UnorderedList(1), PdfStyleNames.Normal);
			style.ParagraphFormat.LeftIndent = Unit.FromCentimeter(0.5);
			style.ParagraphFormat.FirstLineIndent = Unit.FromCentimeter(-0.5);
			style.ParagraphFormat.ListInfo.ListType = ListType.BulletList1;

			style = document.Styles.AddStyle(PdfStyleNames.List.OrderedList(1), PdfStyleNames.List.UnorderedList(1));
			style.ParagraphFormat.ListInfo.ListType = ListType.NumberList1;

			// Indents
			int maxOfIndents = 6;
			for (int i = 2; i < maxOfIndents; i++)
			{
				style = document.Styles.AddStyle(PdfStyleNames.List.UnorderedList(i), PdfStyleNames.List.UnorderedList(1));
				style.ParagraphFormat.LeftIndent = Unit.FromCentimeter(0.5 + (0.5 * (i - 1)));

				style = document.Styles.AddStyle(PdfStyleNames.List.OrderedList(i), PdfStyleNames.List.OrderedList(1));
				style.ParagraphFormat.LeftIndent = Unit.FromCentimeter(0.5 + (0.5 * (i - 1)));
			}

			// Start and endings
			//style = document.Styles.AddStyle("ListStartSub", StyleNames.Normal);
			//style.ParagraphFormat.SpaceBefore = new Unit(0);
			//style.ParagraphFormat.SpaceAfter = new Unit(-20);
		}

		/*private void Tables(Document document)
		{
			Style style = document.Styles.AddStyle("HeaderRow", StyleNames.Normal);
			style.ParagraphFormat.SpaceBefore = new Unit(0);
			style.ParagraphFormat.SpaceAfter = new Unit(0);
			style.ParagraphFormat.Shading.Color = Colors.Black;
			style.ParagraphFormat.Font.Color = Colors.White;

			style = document.Styles.AddStyle("ContentRowOdd", StyleNames.Normal);
			style.ParagraphFormat.SpaceBefore = new Unit(0);
			style.ParagraphFormat.SpaceAfter = new Unit(0);
			style.ParagraphFormat.Borders.Bottom.Color = Colors.Gray;
			style.ParagraphFormat.Borders.Bottom.Width = 1;

			style = document.Styles.AddStyle("ContentRowEven", "ContentRowOdd");
			style.ParagraphFormat.Shading.Color = Colors.LightGray;
		}*/

		private void Blocks(Document document)
		{
			Style style = document.Styles.AddStyle(PdfStyleNames.Misc.HorizontalRule, PdfStyleNames.Normal);
			style.ParagraphFormat.Borders.Top.Color = this.HorizontalRuleColor;
			style.ParagraphFormat.Borders.Top.Width = 1; //TODO: Custom width?
		}

		#endregion

	}
}