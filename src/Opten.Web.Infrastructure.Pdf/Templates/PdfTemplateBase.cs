using Opten.Common.Helpers;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using Opten.Web.Infrastructure.Pdf.Styles;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Opten.Web.Infrastructure.Pdf.Templates
{
	/// <summary>
	/// The base PDF Template.
	/// </summary>
	public abstract class PdfTemplateBase : IPdfTemplate, IPdfGenerator
	{

		#region Public properties

		/// <summary>
		/// Gets the elements.
		/// </summary>
		/// <value>
		/// The elements.
		/// </value>
		public List<IPdfElement> Elements { get; set; }

		/// <summary>
		/// Gets the pages.
		/// </summary>
		/// <value>
		/// The pages.
		/// </value>
		public List<IPdfPage> Pages { get; set; }

		#endregion

		#region Protected/Private fields

		//TODO: Update names to uppercase

#pragma warning disable 1591

		protected string _title;
		protected string _author;
		protected string _subject;
		protected string _keywords;
		protected string _absolutePathToPdfTemplate;

		protected IPdfStyling _pdfStyling;

#pragma warning restore 1591

		private bool _isDefined = false;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTemplateBase" /> class.
		/// </summary>
		public PdfTemplateBase()
		{
			Elements = new List<IPdfElement>();
			Pages = new List<IPdfPage>();
		}

		/// <summary>
		/// Defines the PDF.
		/// </summary>
		public virtual void Define(
			string title,
			string author,
			string subject,
			string keywords,
			string absolutePathToPdfTemplate = null,
			IPdfStyling pdfStyling = null)
		{
			_title = title;
			_author = author;
			_subject = subject;
			_keywords = keywords;
			_absolutePathToPdfTemplate = absolutePathToPdfTemplate;
			_pdfStyling = pdfStyling == null ? new PdfStyling() : pdfStyling;

			_isDefined = true;
		}

		/// <summary>
		/// The file name.
		/// </summary>
		/// <returns></returns>
		public virtual string FileName()
		{
			//TODO: Should we still have this method? Is this really needed?
			string fileName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", this._title, this._subject);
			return FileHelper.GetFileNameWithDate(fileName, "pdf", DateTime.Now, "yyyyMMdd");
		}

		/// <summary>
		/// The PDF Generator.
		/// </summary>
		/// <returns></returns>
		private IPdfGenerator Generator()
		{
			if (_isDefined == false)
			{
				throw new InvalidOperationException("The document is not defined yet. Please use method Define() before you try to save it!");
			}

			IPdfGenerator generator = new PdfGenerator(
				title: this._title,
				author: this._author,
				subject: this._subject,
				keywords: this._keywords,
				absolutePathToPdfTemplate: this._absolutePathToPdfTemplate,
				pdfStyling: this._pdfStyling);

			generator.Elements.AddRange(this.Elements);
			generator.Pages.AddRange(this.Pages);

			return generator;
		}

		/// <summary>
		/// Saves the PDF on disk.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public void SaveOnDisk(string filePath)
		{
			IPdfGenerator generator = this.Generator();

			generator.SaveOnDisk(filePath: filePath);
		}

		/// <summary>
		/// Saves the PDF in memory.
		/// </summary>
		/// <returns></returns>
		public byte[] SaveInMemory()
		{
			IPdfGenerator generator = this.Generator();

			return generator.SaveInMemory();
		}
	}
}