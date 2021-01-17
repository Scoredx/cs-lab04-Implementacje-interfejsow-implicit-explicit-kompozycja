using System;

namespace Zadanie3
{
    public interface IDevice
    {
        enum State { on, off };

        void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
        void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
        State GetState(); // zwraca aktualny stan urządzenia

        int Counter { get; }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                              // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public abstract class BaseDevice : IDevice
    {
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public virtual void PowerOff()
        {
            state = IDevice.State.off;
            Console.Write("... Device is off !");
        }

        public void PowerOn()
        {
            if (state != IDevice.State.on)
                Counter++;
            state = IDevice.State.on;
            Console.WriteLine("Device is on ...");
        }

        public int Counter { get; private set; } = 0;
    }

    public interface IPrinter : IDevice
    {
        int PrintCounter { get; set; }

        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        int ScanCounter { get; set; }
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG);
    }

    public interface IFax : IDevice
    {
        int ReceivedFaxCounter { get; set; }
        int SentFaxCounter { get; set; }
        void SendFax(in IDocument document, string reciver);
        void ReceiveFax(in IDocument document, string sender);
    }
    public class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get; set; } = 0;

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine($"{ DateTime.Now } Print: {document.GetFileName()}");
            }
        }
    }

    public class Scanner : BaseDevice, IScanner
    {
        public int ScanCounter { get; set; } = 0;

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            switch (formatType)
            {
                case IDocument.FormatType.TXT:
                    document = new TextDocument("TextScan" + ScanCounter.ToString() + ".txt");
                    break;
                case IDocument.FormatType.PDF:
                    document = new PDFDocument("PDFScan" + ScanCounter.ToString() + ".pdf");
                    break;
                case IDocument.FormatType.JPG:
                    document = new TextDocument("ImageScan" + ScanCounter.ToString() + ".jpg");
                    break;
                default:
                    throw new Exception();
            }

            if (state == IDevice.State.on)
            {
                ScanCounter++;
                Console.WriteLine($"{ DateTime.Now } Scan: { document.GetFileName() }");
            }
        }
    }
    public class Fax : BaseDevice, IFax
    {
        public int ReceivedFaxCounter { get; set; }
        public int SentFaxCounter { get; set; }

        public void ReceiveFax(in IDocument document, string sender)
        {
            if (state == IDevice.State.on)
            {
                ReceivedFaxCounter++;

                Console.WriteLine($"Received document: { document.GetFileName() } from: { sender }");
            }
        }

        public void SendFax(in IDocument document, string reciver)
        {
            if (state == IDevice.State.on)
            {
                SentFaxCounter++;

                Console.WriteLine($"Sending document: { document.GetFileName() } to: { reciver }");
            }
        }
    }
}