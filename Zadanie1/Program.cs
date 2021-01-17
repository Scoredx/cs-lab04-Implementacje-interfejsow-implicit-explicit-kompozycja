using System;

namespace Zadanie1
{
    class Program
    {
        static void Main()
        {
            var xerox = new Copier();
            xerox.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);
            xerox.ScanAndPrint();
            Console.WriteLine();

            Console.WriteLine("PowerON Counter: " + xerox.Counter);
            Console.WriteLine("Printer Counter: " + xerox.PrintCounter);
            Console.WriteLine("Scanner Counter: " + xerox.ScanCounter);
        }
    }
}