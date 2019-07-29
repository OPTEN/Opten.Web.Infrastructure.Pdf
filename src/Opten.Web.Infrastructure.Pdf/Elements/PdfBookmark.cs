using System.Collections.Generic;

namespace Opten.Web.Infrastructure.Pdf.Elements
{
	/// <summary>
	/// The Bookmark element.
	/// </summary>
	public class PdfBookmark
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PdfBookmark"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="level">The level.</param>
		public PdfBookmark(string name, int level)
			: this(name, name, level) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfBookmark"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="refName">Name of the reference.</param>
		/// <param name="level">The level.</param>
		public PdfBookmark(string name, string refName, int level)
		{
			this.Name = name;
			this.RefName = refName;
			this.Level = level;
			this.Children = new List<PdfBookmark>();
		}

		/// <summary>
		/// Gets or sets the children.
		/// </summary>
		/// <value>
		/// The children.
		/// </value>
		public List<PdfBookmark> Children { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the name of the reference.
		/// </summary>
		/// <value>
		/// The name of the reference.
		/// </value>
		public string RefName { get; set; }

		/// <summary>
		/// Gets or sets the level.
		/// </summary>
		/// <value>
		/// The level.
		/// </value>
		public int Level { get; set; }

	}
}