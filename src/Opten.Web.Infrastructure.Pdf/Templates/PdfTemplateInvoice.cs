using MigraDoc.DocumentObjectModel.Shapes;
using Opten.Common.Helpers;
using Opten.Web.Infrastructure.Pdf.Elements;
using Opten.Web.Infrastructure.Pdf.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Opten.Web.Infrastructure.Pdf.Templates
{
	/// <summary>
	/// The PDF Template for Invoices.
	/// </summary>
	public class PdfTemplateInvoice : PdfTemplateBase, IPdfTemplate, IPdfGenerator
	{

		#region Private fields

		private readonly string _invoiceNumber;
		private readonly string _invoiceTitle;
		private readonly IEnumerable<TextLine> _companyAddress;
		private readonly string _billingAddressTitle;
		private readonly IEnumerable<TextLine> _billingAddress;
		private readonly PdfTable _invoiceDetails;
		private readonly PdfTable _invoiceSummary;
		private readonly IEnumerable<TextLine> _details;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfTemplateInvoice" /> class.
		/// </summary>
		/// <param name="invoiceNumber">The invoice number.</param>
		/// <param name="invoiceTitle">The invoice title.</param>
		/// <param name="companyAddress">The company address.</param>
		/// <param name="billingAddressTitle">The billing address title.</param>
		/// <param name="billingAddress">The billing address.</param>
		/// <param name="invoiceDetails">The invoice details.</param>
		/// <param name="invoiceSummary">The invoice summary.</param>
		/// <param name="details">The details (paragraphs after table).</param>
		/// <param name="absolutePathToPdfTemplate">The absolute template path.</param>
		public PdfTemplateInvoice(
			string invoiceNumber,
			string invoiceTitle,
			IEnumerable<TextLine> companyAddress,
			string billingAddressTitle,
			IEnumerable<TextLine> billingAddress,
			PdfTable invoiceDetails,
			PdfTable invoiceSummary,
			IEnumerable<TextLine> details = null,
			string absolutePathToPdfTemplate = null)
			: base()
		{
			_invoiceNumber = invoiceNumber;
			_invoiceTitle = invoiceTitle;
			_companyAddress = companyAddress;
			_billingAddressTitle = billingAddressTitle;
			_billingAddress = billingAddress;
			_invoiceDetails = invoiceDetails;
			_invoiceSummary = invoiceSummary;
			_details = details;
		}

		/// <summary>
		/// Defines the PDF.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="author"></param>
		/// <param name="subject"></param>
		/// <param name="keywords"></param>
		/// <param name="absolutePathToPdfTemplate"></param>
		/// <param name="pdfStyling"></param>
		public override void Define(
			string title,
			string author,
			string subject,
			string keywords,
			string absolutePathToPdfTemplate = null,
			IPdfStyling pdfStyling = null)
		{
			base.Define(title, author, subject, keywords, absolutePathToPdfTemplate, pdfStyling);

			base.Elements.Add(new PdfAddress(string.Empty, _companyAddress, 20, 0, 120, RelativeVertical.Page));

			base.Elements.Add(new PdfAddress(_billingAddressTitle, _billingAddress, 0, 300, 80));

			base.Elements.Add(new PdfHeading(_invoiceTitle, 1));

			if (_invoiceDetails != null)
			{
				base.Elements.Add(_invoiceDetails);

				base.Elements.Add(new PdfSpace(10, 10));
			}

			base.Elements.Add(_invoiceSummary);

			if (_details != null)
			{
				base.Elements.Add(new PdfSpace(10, 10));

				base.Elements.Add(new PdfParagraph(paragraphs: _details));
			}
		}

		/// <summary>
		/// Gets the file name of the pdf.
		/// </summary>
		/// <returns></returns>
		public override string FileName()
		{
			string fileName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", "Invoice", this._invoiceNumber);
			return FileHelper.GetFileNameWithDate(fileName, "pdf", DateTime.Now, "yyyyMMdd");
		}

	}
}