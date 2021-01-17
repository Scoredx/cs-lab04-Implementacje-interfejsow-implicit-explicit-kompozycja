using System;

namespace Zadanie2
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {

        public int PrintCounter { get; set; } = 0;

        public int ScanCounter { get; set; } = 0;


        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.on)
            {
                Console.WriteLine($"{ DateTime.Now } Print: {document.GetFileName()}");
                PrintCounter++;
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (GetState() == IDevice.State.on)
            {
                switch (formatType)
                {
                    case IDocument.FormatType.JPG:
                        document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                        break;
                    case IDocument.FormatType.TXT:
                        document = new TextDocument($"TextScan{ScanCounter}.txt");
                        break;
                    case IDocument.FormatType.PDF:
                    default:
                        document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                        break;
                }
                Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Scan: {document.GetFileName()}");
                ScanCounter++;
            }
            else
            {
                document = null;
            }
        }

        public void Scan(out IDocument document)
        {
            Scan(out document, IDocument.FormatType.JPG);
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                IDocument document;
                Scan(out document, IDocument.FormatType.JPG);
                Print(document);

            }
        }
    }
}

