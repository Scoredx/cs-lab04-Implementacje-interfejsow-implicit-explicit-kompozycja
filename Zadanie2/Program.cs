using System;

namespace Zadanie2
{
    class Program
    {
        static void Main()
        {
            var xerox = new MultifunctionalDevice();
            xerox.PowerOn();

            IDocument doc1 = new PDFDocument("test.pdf");
            xerox.Print(in doc1);
            IDocument doc2;
            xerox.Scan(out doc2);
            xerox.ScanAndPrint();
            Console.WriteLine();
            xerox.SendFax(doc1, "test");
            xerox.ScanAndSendFax("test");

            Console.WriteLine();
            Console.WriteLine("PowerON Counter: " + xerox.Counter);
            Console.WriteLine("Print Counter: " + xerox.PrintCounter);
            Console.WriteLine("Scan Counter: " + xerox.ScanCounter);
        }
    }
}
