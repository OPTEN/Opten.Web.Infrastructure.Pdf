using Opten.Web.Infrastructure.Pdf.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Web.Infrastructure.Pdf.Table
{
	/// <summary>
	/// A PDF Table row.
	/// </summary>
	public class PdfTableRow
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableRow"/> class.
		/// </summary>
		public PdfTableRow()
		{
			Cells = new List<IPdfTableCell>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableRow"/> class.
		/// </summary>
		/// <param name="cells">The cells.</param>
		/// <exception cref="System.ArgumentNullException">cells;You have to define some cells!</exception>
		public PdfTableRow(IEnumerable<IPdfTableCell> cells)
		{
			if (cells == null)
				throw new ArgumentNullException("cells", "You have to define some cells!");

			Cells = cells.ToList();
		}

		/// <summary>
		/// Gets or sets the cells.
		/// </summary>
		/// <value>
		/// The cells.
		/// </value>
		public List<IPdfTableCell> Cells { get; set; }

		/// <summary>
		/// Adds the specified cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		public void Add(IPdfTableCell cell)
		{
			this.Cells.Add(cell);
		}
	}
}