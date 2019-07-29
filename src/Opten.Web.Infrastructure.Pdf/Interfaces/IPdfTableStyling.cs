using MigraDoc.DocumentObjectModel;

namespace Opten.Web.Infrastructure.Pdf.Interfaces
{
	/// <summary>
	/// The PDF Table Styling.
	/// </summary>
	public interface IPdfTableStyling
	{

		/// <summary>
		/// Gets the fore color of the header.
		/// </summary>
		/// <value>
		/// The color of the header fore.
		/// </value>
		Color HeaderForeColor { get; }

		/// <summary>
		/// Gets the back color of the header.
		/// </summary>
		/// <value>
		/// The color of the header back.
		/// </value>
		Color HeaderBackColor { get; }

		/// <summary>
		/// Gets the fore color of the row odd.
		/// </summary>
		/// <value>
		/// The color of the row odd fore.
		/// </value>
		Color? RowOddForeColor { get; }

		/// <summary>
		/// Gets the back color of the row odd.
		/// </summary>
		/// <value>
		/// The color of the row odd back.
		/// </value>
		Color RowOddBackColor { get; }

		/// <summary>
		/// Gets the color of the border.
		/// </summary>
		/// <value>
		/// The color of the border.
		/// </value>
		Color BorderColor { get; }

		/// <summary>
		/// Gets the width of the header border.
		/// </summary>
		/// <value>
		/// The width of the border.
		/// </value>
		double HeaderBorderWidth { get; }

		/// <summary>
		/// Gets the width of the row border.
		/// </summary>
		/// <value>
		/// The width of the border.
		/// </value>
		double RowBorderWidth { get; }

		/// <summary>
		/// Bordered style.
		/// </summary>
		/// <param name="table">The table.</param>
		void BorderedStyle(MigraDoc.DocumentObjectModel.Tables.Table table);

		/// <summary>
		/// Grid style.
		/// </summary>
		/// <param name="table">The table.</param>
		void GridStyle(MigraDoc.DocumentObjectModel.Tables.Table table);

		/// <summary>
		/// Only rows bordered style.
		/// </summary>
		/// <param name="table">The table.</param>
		void OnlyRowsBorderedStyle(MigraDoc.DocumentObjectModel.Tables.Table table);

		/// <summary>
		/// No borders style.
		/// </summary>
		/// <param name="table">The table.</param>
		void NoBordersStyle(MigraDoc.DocumentObjectModel.Tables.Table table);

		/// <summary>
		/// Striped rows style.
		/// </summary>
		/// <param name="table">The table.</param>
		void StripedRowsStyle(MigraDoc.DocumentObjectModel.Tables.Table table);

		/// <summary>
		/// Zebra style.
		/// </summary>
		/// <param name="table">The table.</param>
		void ZebraStyle(MigraDoc.DocumentObjectModel.Tables.Table table);

		/// <summary>
		/// No spacing style.
		/// </summary>
		/// <param name="table">The table.</param>
		void NoSpacingStyle(MigraDoc.DocumentObjectModel.Tables.Table table);

	}
}