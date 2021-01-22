using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.IO;

namespace WatermarkService
{
    /// <summary>
    /// Watermark stamp services built on iText 7 for .NET core 2.2
    /// </summary>
    public class PdfWatermarkStampService : IWaterStampService
    {
        public PdfWatermarkStampService()
        {

        }

        /// <summary>
        /// Adds a watermark phrase to each page of the pdf, centered on the bottom.
        /// </summary>
        /// <param name="source">input file name</param>
        /// <param name="dest">output file name</param>
        /// <param name="phrase"></param>
        public void AddWatermarkEveryPage(string source, string dest, string phrase)
        {
            using (PdfReader reader = new PdfReader(source))
            {
                using (var writer = new PdfWriter(dest))
                {
                    AddWatermarkEveryPage(reader, writer, phrase);
                }
                reader.Close();
            }
        }

        /// <summary>
        /// Adds a watermark phrase to each page of the pdf, centered on the bottom.
        /// </summary>
        /// <param name="source">input stream source</param>
        /// <param name="dest">output stream source</param>
        /// <param name="phrase"></param>
        public void AddWatermarkEveryPage(Stream source, Stream dest, string phrase)
        {
            using (PdfReader reader = new PdfReader(source))
            {
                using (var writer = new PdfWriter(dest))
                {
                    AddWatermarkEveryPage(reader, writer, phrase);
                }
                reader.Close();
            }
        }

        /// <summary>
        /// Adds a watermark phrase to each page of the pdf, centered on the bottom.
        /// </summary>
        /// <param name="reader">input pdf reader</param>
        /// <param name="writer">output pdf reader</param>
        /// <param name="phrase"></param>
        public void AddWatermarkEveryPage(PdfReader reader, PdfWriter writer, string phrase)
        {
            using (PdfDocument pdfDoc = new PdfDocument(reader, writer))
            {
                using (Document doc = new Document(pdfDoc))
                {
                    Rectangle pageSize;
                    PdfCanvas pdfCanvas;

                    int nPages = pdfDoc.GetNumberOfPages();
                    for (int ipage = 1; ipage <= nPages; ipage++)
                    {
                        PdfPage page = pdfDoc.GetPage(ipage);
                        pageSize = page.GetPageSize();

                        pdfCanvas = new PdfCanvas(page);
                        Canvas canvas = new Canvas(pdfCanvas, pageSize);

                        PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                        Text mark = new Text(phrase).SetFont(font).SetFontSize(10);
                        Paragraph p = new Paragraph().Add(mark);

                        p.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        float bottom = pageSize.GetBottom() + 55;
                        p.SetFixedPosition(pageSize.GetLeft(), bottom, pageSize.GetWidth());
                        canvas.Add(p);
                        canvas.Close();
                    }
                }
                pdfDoc.Close();
            }
        }

        /// <summary>
        /// Adds a watermark phrase to the last page of the pdf, centered and rotated on the right margin.
        /// </summary>
        /// <param name="source">input file name</param>
        /// <param name="dest">output file name</param>
        /// <param name="phrase"></param>
        public void AddWatermarkLastPage(string source, string dest, string phrase)
        {
            using (PdfReader reader = new PdfReader(source))
            {
                using (var writer = new PdfWriter(dest))
                {
                    AddWatermarkLastPage(reader, writer, phrase);
                }
                reader.Close();
            }
        }

        /// <summary>
        /// Adds a watermark phrase to the last page of the pdf, centered and rotated on the right margin.
        /// </summary>
        /// <param name="source">input stream</param>
        /// <param name="dest">output stream</param>
        /// <param name="phrase"></param>
        public void AddWatermarkLastPage(Stream source, Stream dest, string phrase)
        {
            using (PdfReader reader = new PdfReader(source))
            {
                using (var writer = new PdfWriter(dest))
                {
                    AddWatermarkLastPage(reader, writer, phrase);
                }
                reader.Close();
            }
        }

        /// <summary>
        /// Adds a watermark phrase to the last page of the pdf, centered and rotated on the right margin.
        /// </summary>
        /// <param name="reader">input pdf reader</param>
        /// <param name="writer">output pdf writer</param>
        /// <param name="phrase"></param>
        public void AddWatermarkLastPage(PdfReader reader, PdfWriter writer, string phrase)
        {
            using (PdfDocument pdfDoc = new PdfDocument(reader, writer))
            {
                using (Document doc = new Document(pdfDoc))
                {
                    int lastPage = pdfDoc.GetNumberOfPages();
                    PdfPage page = pdfDoc.GetPage(lastPage);
                    Rectangle pageSize = page.GetPageSize();
                    PdfCanvas pdfCanvas = new PdfCanvas(page);
                    Canvas canvas = new Canvas(pdfCanvas, pageSize);

                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    Text mark = new Text(phrase).SetFont(font).SetFontSize(5);
                    Paragraph p = new Paragraph().Add(mark);

                    p.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    p.SetRotationAngle(Math.PI / 2.0);
                    p.SetFixedPosition(pageSize.GetRight() - 10, pageSize.GetBottom(), pageSize.GetHeight());
                    canvas.Add(p);
                    canvas.Close();
                }
                pdfDoc.Close();
            }
        }
    }
}
