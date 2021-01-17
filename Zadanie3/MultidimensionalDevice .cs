using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    public class MultidimensionalDevice : BaseDevice
    {
        private IFax Fax { get; set; }
        private IPrinter Printer { get; set; }
        private IScanner Scanner { get; set; }

        public int PrintCounter => Printer.PrintCounter;
        public int ScanCounter => Scanner.ScanCounter;
        public int SentFaxCounter => Fax.SentFaxCounter;
        public int ReceivedFaxCounter => Fax.ReceivedFaxCounter;


        public MultidimensionalDevice()
        {
            Fax = new Fax();
            Printer = new Printer();
            Scanner = new Scanner();
        }

        public override void PowerOff()
        {
            Printer.PowerOff();
            Scanner.PowerOff();
            Fax.PowerOff();
            base.PowerOff();
        }

        public void TurnPrinterOn()
        {
            if (state == IDevice.State.on)
                Printer.PowerOn();
        }

        public void TurnPrinterOff()
        {
            Printer.PowerOff();
        }

        public void TurnScannerOn()
        {
            if (state == IDevice.State.on)
                Scanner.PowerOn();
        }

        public void TurnScannerOff()
        {
            Scanner.PowerOff();
        }

        public void TurnFaxOn()
        {
            if (state == IDevice.State.on)
                Fax.PowerOn();
        }

        public void TurnFaxOff()
        {
            Fax.PowerOff();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            Scanner.Scan(out document, formatType);
        }
        public void Print(in IDocument document)
        {
            Printer.Print(document);
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                IDocument document;
                Scan(out document, IDocument.FormatType.JPG);

                if (Scanner.GetState() == IDevice.State.on)
                    Print(document);
            }
        }
        public void SendFax(out IDocument document, string reciver)
        {
            Scan(out document);

            if (Scanner.GetState() == IDevice.State.on)
                Fax.SendFax(document, reciver);
            else
                Console.WriteLine("Scanner is turned off");
        }

        public void ReceiveFax(in IDocument document, string sender)
        {
            if (Printer.GetState() == IDevice.State.on)
            {
                Fax.ReceiveFax(document, sender);
                Print(document);
            }
            else
                Console.WriteLine("Printer is turned off");
        }
    }
}
