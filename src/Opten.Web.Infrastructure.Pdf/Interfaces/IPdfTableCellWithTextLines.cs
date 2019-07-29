using Opten.Web.Infrastructure.Pdf.Elements;
using System.Collections.Generic;

namespace Opten.Web.Infrastructure.Pdf.Interfaces
{
	/// <summary>
	/// A PDF Table cell with text lines.
	/// </summary>
	public interface IPdfTableCellWithTextLines
	{

		/// <summary>
		/// Gets the text lines.
		/// </summary>
		/// <value>
		/// The text lines.
		/// </value>
		IEnumerable<TextLine> TextLines { get; }

	}
}