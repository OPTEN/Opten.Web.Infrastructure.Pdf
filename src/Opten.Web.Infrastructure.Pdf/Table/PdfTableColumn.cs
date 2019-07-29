namespace Opten.Web.Infrastructure.Pdf.Table
{
	internal class PdfTableColumn
	{
		public PdfTableColumn()
		{
			Fit = true;
		}

		internal double Width { get; set; }

		internal bool Fit { get; set; }
	}
}