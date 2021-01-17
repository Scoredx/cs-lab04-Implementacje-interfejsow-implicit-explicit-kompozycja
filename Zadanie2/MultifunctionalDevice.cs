using System;


namespace Zadanie2
{
    public class MultifunctionalDevice : Copier, IFax
    {
        public int FaxCounter { get; private set; } = 0;
        public int FaxReceivedCounter { get; set; } = 0;


        public void SendFax(in IDocument document, String reciver)
        {
            if (GetState() == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Fax: {document.GetFileName()} to: {reciver}");
                FaxCounter++;
            }
        }

        public void ReceiveFax(in IDocument document, string sender)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine("\nYou have new fax!");
                Console.WriteLine($"Received document: { document.GetFileName() } from: { sender }");
                FaxReceivedCounter++;
            }
            Print(document);
        }
        public void ScanAndSendFax(String reciver)
        {
            IDocument scannedDocument;
            Scan(out scannedDocument);
            SendFax(scannedDocument, reciver);
        }
    }
}   