using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    public class Copier : BaseDevice
    {
        private IPrinter Printer { get; }
        private IScanner Scanner { get; }
        public int PrintCounter => Printer.PrintCounter;
        public int ScanCounter => Scanner.ScanCounter;

        public Copier()
        {
            Printer = new Printer();
            Scanner = new Scanner();
        }

        public override void PowerOff()
        {
            Printer.PowerOff();
            Scanner.PowerOff();
            base.PowerOff();
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

        public void TurnPrinterOn()
        {
            if (state == IDevice.State.on)
                Printer.PowerOn();
        }

        public void TurnPrinterOff()
        {
            Printer.PowerOff();
        }
        public void Print(in IDocument document)
        {
            Printer.Print(document);
        }
        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            Scanner.Scan(out document, formatType);
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
    }
}