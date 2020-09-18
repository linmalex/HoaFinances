using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text;

namespace HoaTreasurerFunctions
{
    public class PdfTool
    {
        public string ExtractPdfText(FileInfo file, bool writeToTextFile = false)
        {
            StringBuilder text = new StringBuilder();
            using (PdfReader pdfReader = new PdfReader(file))
            {
                using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
                {
                    var pageCount = pdfDoc.GetNumberOfPages();

                    for (int page = 1; page <= pageCount; page++)
                    {
                        PdfPage pdfPage = pdfDoc.GetPage(page);
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                        string currentText = PdfTextExtractor.GetTextFromPage(pdfPage);

                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                        text.Append(currentText);
                    }
                }
            }

            return text.ToString();
        }
    }
}
