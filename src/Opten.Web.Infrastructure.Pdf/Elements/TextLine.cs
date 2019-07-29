namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// A Text line.
	/// </summary>
	public class TextLine
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="TextLine"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="isBold">if set to <c>true</c> [is bold].</param>
		public TextLine(
			string text,
			bool isBold = false)
		{
			this.Text = string.IsNullOrWhiteSpace(text) //TODO: Better way -> maybe NullCheckTrim()?
				? string.Empty
				: text.Trim();
			this.IsBold = isBold;
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is bold.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is bold; otherwise, <c>false</c>.
		/// </value>
		public bool IsBold { get; private set; }

	}
}