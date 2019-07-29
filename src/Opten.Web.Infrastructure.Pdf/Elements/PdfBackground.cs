using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Background element.
	/// </summary>
	public class PdfBackground
	{

		private readonly string _url;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfBackground"/> class.
		/// </summary>
		/// <param name="absoluteUrl">The absolute URL.</param>
		public PdfBackground(string absoluteUrl)
		{
			_url = absoluteUrl;
		}

		/// <summary>
		/// Renders the specified headers footers.
		/// </summary>
		/// <param name="headersFooters">The headers footers.</param>
		public void Render(HeadersFooters headersFooters)
		{
			TextFrame frame = headersFooters.Primary.AddTextFrame();
			frame.RelativeHorizontal = RelativeHorizontal.Page;
			frame.RelativeVertical = RelativeVertical.Page;
			frame.Left = ShapePosition.Left;
			frame.Width = Unit.FromCentimeter(21);
			frame.Height = Unit.FromCentimeter(29.8);
			frame.AddImage(_url);
		}

	}
}