using Opten.Web.Infrastructure.Pdf.Styles;

namespace Opten.Web.Infrastructure.Pdf.Interfaces
{
	/// <summary>
	/// A PDF Table cell.
	/// </summary>
	public interface IPdfTableCell
	{

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		string Text { get; }

		/// <summary>
		/// Gets the alignment.
		/// </summary>
		/// <value>
		/// The alignment.
		/// </value>
		Alignment Alignment { get; }

		/// <summary>
		/// Gets the colspan.
		/// </summary>
		/// <value>
		/// The colspan.
		/// </value>
		int Colspan { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is bold.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is bold; otherwise, <c>false</c>.
		/// </value>
		bool IsBold { get; }

	}
}