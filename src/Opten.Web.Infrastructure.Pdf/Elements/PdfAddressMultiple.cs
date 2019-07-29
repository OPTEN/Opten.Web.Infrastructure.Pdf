using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using Opten.Web.Infrastructure.Pdf.Table;
using System.Collections.Generic;
using System.Linq;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// Renders multiple addresses.
	/// </summary>
	public class PdfAddressMultiple : IPdfElement
	{

		private readonly IEnumerable<string> _titles;
		private readonly IEnumerable<IEnumerable<TextLine>> _addresses;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfAddressMultiple"/> class.
		/// </summary>
		/// <param name="titles">The titles.</param>
		/// <param name="addresses">The addresses.</param>
		public PdfAddressMultiple(
			IEnumerable<string> titles,
			IEnumerable<IEnumerable<TextLine>> addresses)
		{
			_titles = titles;
			_addresses = addresses;
		}

		/// <summary>
		/// Renders the specified PDF styling.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			PdfTable table = new PdfTable(
				style: TableStyle.NoSpacing,
				fitToDocument: true);

			// Title rows
			foreach (string title in _titles)
			{
				table.THead.Add(new PdfTableHeaderCell(title.Trim(), true, Alignment.Left));
			}

			// Get the max count

			int maxCount = _addresses.Max(o => o.Count());

			PdfTableRow row;

			string line;
			bool isBold;
			for (int i = 0; i < maxCount; i++)
			{
				row = new PdfTableRow();
				foreach (IEnumerable<TextLine> addressLine in _addresses)
				{
					line = i >= addressLine.Count() ? string.Empty : addressLine.ElementAt(i).Text;
					isBold = i >= addressLine.Count() ? false : addressLine.ElementAt(i).IsBold;

					row.Add(new PdfTableCell(text: line, isBold: isBold));
				}

				table.TBody.Add(row);
			}

			table.Render(pdfStyling: pdfStyling, section: section);
		}
	}
}
