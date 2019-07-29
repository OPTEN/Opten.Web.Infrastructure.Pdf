namespace Opten.Web.Infrastructure.Pdf.Interfaces
{
	/// <summary>
	/// A PDF table header cell.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfTableCell" />
	public interface IPdfTableHeaderCell : IPdfTableCell
	{

		/// <summary>
		/// Gets the width in centimeter.
		/// </summary>
		/// <value>
		/// The width in centimeter.
		/// </value>
		double WidthInCentimeter { get; }

	}
}