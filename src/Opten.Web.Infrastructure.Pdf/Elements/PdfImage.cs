using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using Opten.Web.Infrastructure.Pdf.Interfaces;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Image element.
	/// </summary>
	/// <seealso cref="Opten.Web.Infrastructure.Pdf.Interfaces.IPdfElement" />
	public class PdfImage : IPdfElement
	{

		private readonly string _fileName;
		private readonly int _width;
		private readonly int _height;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfImage"/> class.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		public PdfImage(string fileName, int width, int height)
		{
			_fileName = fileName;
			_width = width;
			_height = height;
		}

		/// <summary>
		/// Renders the element.
		/// </summary>
		/// <param name="pdfStyling">The PDF styling.</param>
		/// <param name="section">The section.</param>
		public void Render(IPdfStyling pdfStyling, Section section)
		{
			Image image = new Image(this._fileName); // We recommend to scale down image with ImageProcessor or ImageResizer!
			//double width = this._width > pdfStyling.MaxWidthInPoints ? pdfStyling.MaxWidthInPoints : this._width;
			image.Width = this._width;
			//image.Height = Unit.FromCentimeter(heightInCentimeter);
			image.LockAspectRatio = true;
			
			TextFrame frame = section.AddTextFrame();
			frame.Add(image);
			frame.Height = this._height;
			frame.Width = this._width;
		}

	}
}