using System;
using System.IO;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

public class PdfRectangleDrawer
{
    public byte[] DrawRectanglesOnEveryPage(string sourcePdfPath, string searchText)
    {
        using (PdfReader reader = new PdfReader(sourcePdfPath))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfStamper stamper = new PdfStamper(reader, ms))
                {
                    PdfTextSearch search = new PdfTextSearch();

                    var tasks = new Task[reader.NumberOfPages];

                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        int pageNumber = i; 
                        tasks[i - 1] = Task.Run(() =>
                        {
                            PdfContentByte canvas = stamper.GetOverContent(pageNumber);
                            float pageHeight = reader.GetPageSize(pageNumber).Height;
                            float pageWidth = reader.GetPageSize(pageNumber).Width;

                            float x = 0;
                            float y = 0; 
                            float height = 5;

                            while (y <= pageHeight) 
                            {
                                string textInRectangle = search.GetTextInRectangle(sourcePdfPath, (int)x, (int)(y), (int)pageWidth, (int)height, pageNumber);
                                Console.WriteLine(textInRectangle);
                                Console.WriteLine("______________________________________");
                                if (textInRectangle.Contains(searchText))
                                {
                                    Console.WriteLine("finded");
                                    iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(x, y + 10, x + pageWidth, y + height - 120);
                                    rect.BackgroundColor = BaseColor.WHITE;
                                    canvas.Rectangle(rect);
                                    canvas.SetColorFill(BaseColor.WHITE);
                                    canvas.Fill();
                                    return;
                                }
                                y += 10;
                            }
                        });
                    }

                    Task.WaitAny(tasks);
                }
                return ms.ToArray();
            }
        }
    }
}