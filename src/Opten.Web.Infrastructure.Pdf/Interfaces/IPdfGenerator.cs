using System.Collections.Generic;

namespace Opten.Web.Infrastructure.Pdf.Interfaces
{
	/// <summary>
	/// The PDF Generator.
	/// </summary>
	public interface IPdfGenerator
	{

		/// <summary>
		/// Gets the elements.
		/// </summary>
		/// <value>
		/// The elements.
		/// </value>
		List<IPdfElement> Elements { get; }

		/// <summary>
		/// Saves the PDF on disk.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		void SaveOnDisk(string filePath);

		/// <summary>
		/// Saves the PDF in memory.
		/// </summary>
		/// <returns></returns>
		byte[] SaveInMemory();

	}
}