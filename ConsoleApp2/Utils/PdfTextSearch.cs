using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

public class PdfTextSearch
{
    //find text
    public string GetTextInRectangle(string filePath, int x, int y, int width, int height, int pageNumber)
    {
        using (PdfReader reader = new PdfReader(filePath))
        {
            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(x, y, 800,  height);
            RenderFilter[] filters = new RenderFilter[1];
            filters[0] = new RegionTextRenderFilter(rect);
            ITextExtractionStrategy strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filters);
            string text = PdfTextExtractor.GetTextFromPage(reader, pageNumber, strategy);
            return text;
        }
    }
}