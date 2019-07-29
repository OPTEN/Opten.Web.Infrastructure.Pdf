using MigraDoc.DocumentObjectModel;
using Opten.Web.Infrastructure.Pdf.Extensions;
using Opten.Web.Infrastructure.Pdf.Interfaces;

namespace Opten.Web.Infrastructure.Pdf.Styles
{

	/// <summary>
	/// The Table Styles.
	/// </summary>
	public enum TableStyle
	{
		/// <summary>
		/// Bordered style
		/// </summary>
		Bordered,
		/// <summary>
		/// Grid style
		/// </summary>
		Grid,
		/// <summary>
		/// Only rows bordered style
		/// </summary>
		OnlyRowsBordered,
		/// <summary>
		/// No borders style
		/// </summary>
		NoBorders,
		/// <summary>
		/// Striped rows style
		/// </summary>
		StripedRows,
		/// <summary>
		/// Zebra style
		/// </summary>
		Zebra,
		/// <summary>
		/// No spacing style
		/// </summary>
		NoSpacing
	}

	/// <summary>
	/// The PDF Table Styling.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfTableStyling" />
	public class PdfTableStyling : IPdfTableStyling
	{

		#region Properties

		//TODO: Control table font-size

		/// <summary>
		/// Gets the fore color of the header.
		/// </summary>
		/// <value>
		/// The color of the header fore.
		/// </value>
		public Color HeaderForeColor { get; protected set; }

		/// <summary>
		/// Gets the back color of the header.
		/// </summary>
		/// <value>
		/// The color of the header back.
		/// </value>
		public Color HeaderBackColor { get; protected set; }

		/// <summary>
		/// Gets the fore color of the row odd.
		/// </summary>
		/// <value>
		/// The color of the row odd fore.
		/// </value>
		public Color? RowOddForeColor { get; protected set; }

		/// <summary>
		/// Gets the back color of the row odd.
		/// </summary>
		/// <value>
		/// The color of the row odd back.
		/// </value>
		public Color RowOddBackColor { get; protected set; }

		/// <summary>
		/// Gets the color of the border.
		/// </summary>
		/// <value>
		/// The color of the border.
		/// </value>
		public Color BorderColor { get; protected set; }

		/// <summary>
		/// Gets the width of the header border.
		/// </summary>
		/// <value>
		/// The width of the border.
		/// </value>
		public double HeaderBorderWidth { get; protected set; }

		/// <summary>
		/// Gets the width of the row border.
		/// </summary>
		/// <value>
		/// The width of the border.
		/// </value>
		public double RowBorderWidth { get; protected set; }

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTableStyling" /> class.
		/// </summary>
		/// <param name="headerForeColor">Color of the header fore.</param>
		/// <param name="headerBackColor">Color of the header back.</param>
		/// <param name="rowOddForeColor">Color of the row odd fore.</param>
		/// <param name="rowOddBackColor">Color of the row odd back.</param>
		/// <param name="borderColor">Color of the border.</param>
		/// <param name="headerBorderWidth">Width of the header border.</param>
		/// <param name="rowBorderWidth">Width of the border.</param>
		public PdfTableStyling(
			Color? headerForeColor = null,
			Color? headerBackColor = null,
			Color? rowOddForeColor = null,
			Color? rowOddBackColor = null,
			Color? borderColor = null,
			double headerBorderWidth = 1.0,
			double rowBorderWidth = 0.5)
		{
			this.HeaderForeColor = headerForeColor.HasValue ? headerForeColor.Value : Colors.White;
			this.HeaderBackColor = headerBackColor.HasValue ? headerBackColor.Value : Colors.Black;
			this.RowOddForeColor = rowOddForeColor;
			this.RowOddBackColor = rowOddBackColor.HasValue ? rowOddBackColor.Value : new Color((byte)255, (byte)248, (byte)248, (byte)248);
			this.BorderColor = borderColor.HasValue ? borderColor.Value : Colors.Black;
			this.HeaderBorderWidth = headerBorderWidth;
			this.RowBorderWidth = rowBorderWidth;
		}

		/// <summary>
		/// Bordered style.
		/// </summary>
		/// <param name="table">The table.</param>
		public virtual void BorderedStyle(MigraDoc.DocumentObjectModel.Tables.Table table)
		{
			// THead
			for (int r = 0; r < table.Rows[0].Cells.Count; r++)
			{
				table.Rows[0].Cells[r].Borders.Color = this.BorderColor;
				table.Rows[0].Cells[r].Borders.Top.Width = this.RowBorderWidth;
				table.Rows[0].Cells[r].Borders.Bottom.Width = this.HeaderBorderWidth;
				table.Rows[0].Cells[r].Borders.Left.Width = this.RowBorderWidth;
				table.Rows[0].Cells[r].Borders.Right.Width = this.RowBorderWidth;
			}

			// TBody, TFoot
			for (int r = 1; r < table.Rows.Count; r++)
			{
				for (int c = 0; c < table.Rows[r].Cells.Count; c++)
				{
					table.Rows[r].Cells[c].Borders.Color = this.BorderColor;
					table.Rows[r].Cells[c].Borders.Top.Width = this.RowBorderWidth;
					table.Rows[r].Cells[c].Borders.Bottom.Width = this.RowBorderWidth;
					table.Rows[r].Cells[c].Borders.Left.Width = this.RowBorderWidth;
					table.Rows[r].Cells[c].Borders.Right.Width = this.RowBorderWidth;
				}
			}
		}

		/// <summary>
		/// Grid style.
		/// </summary>
		/// <param name="table">The table.</param>
		public virtual void GridStyle(MigraDoc.DocumentObjectModel.Tables.Table table)
		{
			// THead
			for (int r = 0; r < table.Rows[0].Cells.Count; r++)
			{
				table.Rows[0].Cells[r].Borders.Color = this.BorderColor;
				table.Rows[0].Cells[r].Borders.Top.Width = 0D;
				table.Rows[0].Cells[r].Borders.Bottom.Width = this.HeaderBorderWidth;
				table.Rows[0].Cells[r].Borders.Left.Width = (r == 0) ? 0D : this.RowBorderWidth;
				table.Rows[0].Cells[r].Borders.Right.Width = ((r + 1) == table.Rows[0].Cells.Count) ? 0D : this.RowBorderWidth;
			}

			// TBody, TFoot
			for (int r = 1; r < table.Rows.Count; r++)
			{
				for (int c = 0; c < table.Rows[r].Cells.Count; c++)
				{
					table.Rows[r].Cells[c].Borders.Color = this.BorderColor;
					table.Rows[r].Cells[c].Borders.Top.Width = this.RowBorderWidth;
					table.Rows[r].Cells[c].Borders.Bottom.Width = ((r + 1) == table.Rows.Count) ? 0D : this.RowBorderWidth;
					table.Rows[r].Cells[c].Borders.Left.Width = (c == 0) ? 0D : this.RowBorderWidth;
					table.Rows[r].Cells[c].Borders.Right.Width = ((c + 1) == table.Rows[r].Cells.Count) ? 0D : this.RowBorderWidth;
				}
			}
		}

		/// <summary>
		/// Only rows bordered style.
		/// </summary>
		/// <param name="table">The table.</param>
		public virtual void OnlyRowsBorderedStyle(MigraDoc.DocumentObjectModel.Tables.Table table)
		{
			// THead
			for (int r = 0; r < table.Rows[0].Cells.Count; r++)
			{
				table.Rows[0].Cells[r].Borders.Color = this.BorderColor;
				table.Rows[0].Cells[r].Borders.Top.Width = 0;
				table.Rows[0].Cells[r].Borders.Bottom.Width = this.HeaderBorderWidth;
				table.Rows[0].Cells[r].Borders.Left.Width = 0;
				table.Rows[0].Cells[r].Borders.Right.Width = 0;
			}

			// TBody, TFoot
			MigraDoc.DocumentObjectModel.Tables.Cell cell;
			for (int r = 1; r < table.Rows.Count; r++)
			{
				for (int c = 0; c < table.Rows[r].Cells.Count; c++)
				{
					cell = table.Rows[r].Cells[c];

					cell.Borders.Color = this.BorderColor;
					cell.Borders.Top.Width = 0;
					cell.Borders.Bottom.Width = this.RowBorderWidth;
					cell.Borders.Left.Width = 0;
					cell.Borders.Right.Width = 0;

					// Respect colspan for last row otherwise there will be an | -> if only new PdfTableCell("...", 6);
					if (c + 1 == table.Rows[r].Cells.Count && cell.MergeRight > 0)
					{
						table.Rows[r].Cells[c + cell.MergeRight].Borders.Left.Width = 0;
						table.Rows[r].Cells[c + cell.MergeRight].Borders.Right.Width = 0;
					}
				}
			}
		}

		/// <summary>
		/// No borders style.
		/// </summary>
		/// <param name="table">The table.</param>
		public virtual void NoBordersStyle(MigraDoc.DocumentObjectModel.Tables.Table table)
		{
			// THead, TBody, TFoot
			for (int r = 0; r < table.Rows.Count; r++)
			{
				table.Rows[r].Borders.Visible = false;
			}
		}

		/// <summary>
		/// Striped rows style.
		/// </summary>
		/// <param name="table">The table.</param>
		public virtual void StripedRowsStyle(MigraDoc.DocumentObjectModel.Tables.Table table)
		{
			OnlyRowsBorderedStyle(table);

			// TBody, TFoot
			for (int r = 1; r < table.Rows.Count; r++)
			{
				for (int c = 0; c < table.Rows[r].Cells.Count; c++)
				{
					if ((r + 1) % 2 == 0)
					{
						if (table.Rows[r].Cells[c].IsTBody())
						{
							if (this.RowOddForeColor.HasValue)
							{
								table.Rows[r].Cells[c].Format.Font.Color = this.RowOddForeColor.Value;
							}
							table.Rows[r].Cells[c].Shading.Color = this.RowOddBackColor;
						}
					}
				}
			}
		}

		/// <summary>
		/// Zebra style.
		/// </summary>
		/// <param name="table">The table.</param>
		public virtual void ZebraStyle(MigraDoc.DocumentObjectModel.Tables.Table table)
		{
			// THead
			for (int r = 0; r < table.Rows[0].Cells.Count; r++)
			{
				table.Rows[0].Cells[r].Format.Font.Color = this.HeaderForeColor;
				table.Rows[0].Cells[r].Shading.Color = this.HeaderBackColor;
			}

			// TBody, TFoot
			for (int r = 1; r < table.Rows.Count; r++)
			{
				for (int c = 0; c < table.Rows[r].Cells.Count; c++)
				{
					if (table.Rows[r].Cells[c].IsTBody())
					{
						table.Rows[r].Cells[c].Borders.Bottom.Color = this.RowOddBackColor;
						table.Rows[r].Cells[c].Borders.Bottom.Width = this.RowBorderWidth;

						if ((r + 1) % 2 == 0)
						{
							if (this.RowOddForeColor.HasValue)
							{
								table.Rows[r].Cells[c].Format.Font.Color = this.RowOddForeColor.Value;
							}
							table.Rows[r].Cells[c].Shading.Color = this.RowOddBackColor;
						}
					}
				}
			}
		}

		/// <summary>
		/// No spacing style.
		/// </summary>
		/// <param name="table">The table.</param>
		public virtual void NoSpacingStyle(MigraDoc.DocumentObjectModel.Tables.Table table)
		{
			table.TopPadding = 0;
			table.BottomPadding = 0;

			// THead, TBody, TFoot
			for (int r = 0; r < table.Rows.Count; r++)
			{
				for (int c = 0; c < table.Rows[r].Cells.Count; c++)
				{
					table.Rows[r].Cells[c].Format.SpaceBefore = 0;
					table.Rows[r].Cells[c].Format.SpaceAfter = -2;
				}
			}
		}
	}
}