using System;

namespace Zadanie3
{
    class Program
    {
        static void Main(string[] args)
        {           
            var MultidimensionalDevice = new MultidimensionalDevice();

            MultidimensionalDevice.PowerOn(); Console.WriteLine(" Base");
            MultidimensionalDevice.TurnPrinterOn(); Console.WriteLine(" Printer");
            IDocument doc1 = new PDFDocument("aaa.pdf");
            MultidimensionalDevice.Print(in doc1);
            Console.WriteLine();

            MultidimensionalDevice.TurnScannerOn(); Console.WriteLine(" Scanner");
            IDocument doc2;
            MultidimensionalDevice.Scan(out doc2);
            MultidimensionalDevice.ScanAndPrint();
            Console.WriteLine();

            Console.WriteLine("PowerON Counter: " + MultidimensionalDevice.Counter);
            Console.WriteLine("Printer Counter: " + MultidimensionalDevice.PrintCounter);
            Console.WriteLine("Scanner Counter: " + MultidimensionalDevice.ScanCounter);
            Console.WriteLine();

            MultidimensionalDevice.PowerOn();
            Console.WriteLine(" Base");
            MultidimensionalDevice.TurnFaxOn();
            Console.WriteLine(" Fax");
            MultidimensionalDevice.TurnPrinterOn();
            Console.WriteLine(" Printer");
            MultidimensionalDevice.TurnScannerOn();
            Console.WriteLine(" Scanner");

            Console.WriteLine();
            IDocument doc3;
            MultidimensionalDevice.SendFax(out doc3, "test");
            IDocument doc4 = new ImageDocument("ImgDoc.jpg");

            MultidimensionalDevice.ReceiveFax(in doc4, "test");
            Console.WriteLine();

            Console.WriteLine("Fax Sent Counter: " + MultidimensionalDevice.SentFaxCounter);
            Console.WriteLine("Fax Recived Counter: " + MultidimensionalDevice.ReceivedFaxCounter);
            Console.WriteLine();

            MultidimensionalDevice.TurnFaxOff();
            Console.WriteLine(" Fax");
            MultidimensionalDevice.TurnPrinterOff();
            Console.WriteLine(" Printer");
            MultidimensionalDevice.TurnScannerOff();
            Console.WriteLine(" Scanner");
        }
    }
}
