using MigraDoc.DocumentObjectModel;

namespace Opten.Web.Infrastructure.Pdf.Interfaces
{
	/// <summary>
	/// The PDF Styling.
	/// </summary>
	public interface IPdfStyling
	{

		/// <summary>
		/// Gets the page format.
		/// </summary>
		/// <value>
		/// The page format.
		/// </value>
		PageFormat PageFormat { get; }

		/// <summary>
		/// Gets the page orientation.
		/// </summary>
		/// <value>
		/// The orientation.
		/// </value>
		Orientation Orientation { get; }

		/// <summary>
		/// Gets the page margin [top, right, bottom, left].
		/// </summary>
		/// <value>
		/// The margin.
		/// </value>
		Unit[] Margin { get; }

		/// <summary>
		/// Gets a value indicating whether [show page number].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show page number]; otherwise, <c>false</c>.
		/// </value>
		bool ShowPageNumber { get; }

		/// <summary>
		/// Gets the page number alignment.
		/// </summary>
		/// <value>
		/// The page number alignment.
		/// </value>
		ParagraphAlignment PageNumberAlignment { get; }

		/// <summary>
		/// Gets the page number margin top.
		/// </summary>
		/// <value>
		/// The page number margin top.
		/// </value>
		int PageNumberMarginTop { get; }

		/// <summary>
		/// Gets the page width in centimeters.
		/// </summary>
		/// <value>
		/// The page width in centimeter.
		/// </value>
		double PageWidthInCentimeters { get; }

		/// <summary>
		/// Gets the page height in centimeters.
		/// </summary>
		/// <value>
		/// The page height in centimeter.
		/// </value>
		double PageHeightInCentimeters { get; }
		
		/// <summary>
		/// Gets the maximum width in points (page width - margin).
		/// </summary>
		/// <value>
		/// The maximum width in points.
		/// </value>
		Unit MaxWidthInPoints { get; }

		/// <summary>
		/// Gets the maximum height in points (page height - margin).
		/// </summary>
		/// <value>
		/// The maximum height in points.
		/// </value>
		Unit MaxHeightInPoints { get; }

		/// <summary>
		/// Gets the maximum width in centimeters (page width - margin).
		/// </summary>
		/// <value>
		/// The maximum width in centimeters.
		/// </value>
		double MaxWidthInCentimeters { get; }

		/// <summary>
		/// Gets the maximum height in centimeters (page height - margin).
		/// </summary>
		/// <value>
		/// The maximum height in centimeters.
		/// </value>
		double MaxHeightInCentimeters { get; }

		/// <summary>
		/// Gets the name of the font.
		/// </summary>
		/// <value>
		/// The name of the font.
		/// </value>
		string FontName { get; }

		/// <summary>
		/// Gets the size of the font.
		/// </summary>
		/// <value>
		/// The size of the font.
		/// </value>
		int FontSize { get; }

		/// <summary>
		/// Gets the font color.
		/// </summary>
		/// <value>
		/// The font color.
		/// </value>
		Color FontColor { get; }
		
		/// <summary>
		/// Gets the size of the header title font.
		/// </summary>
		/// <value>
		/// The size of the header title font.
		/// </value>
		int HeaderTitleFontSize { get; }

		/// <summary>
		/// Gets the color of the header title.
		/// </summary>
		/// <value>
		/// The color of the header title.
		/// </value>
		Color HeaderTitleColor { get; }
		
		/// <summary>
		/// Gets the size of the paragraph small font.
		/// </summary>
		/// <value>
		/// The size of the paragraph small font.
		/// </value>
		int ParagraphSmallFontSize { get; }

		/// <summary>
		/// Gets the color of the paragraph small.
		/// </summary>
		/// <value>
		/// The color of the paragraph small.
		/// </value>
		Color ParagraphSmallColor { get; }

		/// <summary>
		/// Gets the color of the horizontal rule.
		/// </summary>
		/// <value>
		/// The color of the horizontal rule.
		/// </value>
		Color HorizontalRuleColor { get; }

		/// <summary>
		/// Gets the table styling.
		/// </summary>
		/// <value>
		/// The table styling.
		/// </value>
		IPdfTableStyling TableStyling { get; }

		/// <summary>
		/// Defines the document.
		/// </summary>
		/// <param name="document">The document.</param>
		void Define(Document document);

	}
}
