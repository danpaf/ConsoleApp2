using System.Diagnostics.CodeAnalysis;

class Program
{
    //мейн метод
    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: iTextSharp.text.pdf.PdfString; size: 142MB")]
    static void Main(string[] args)
    {
        PdfRectangleDrawer drawer = new PdfRectangleDrawer();
        byte[] modifiedPdf = drawer.DrawRectanglesOnEveryPage("Receipt-9620981.pdf", "Payment receipt");
        File.WriteAllBytes("ModifiedReceipt.pdf", modifiedPdf);
        Console.WriteLine("PDF processing completed.");
    }
}


