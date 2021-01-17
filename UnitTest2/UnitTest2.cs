using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie2;

namespace UnitTests2
{
    [TestClass]
    public class UnitTest2
    {
        public class ConsoleRedirectionToStringWriter : IDisposable
        {
            private StringWriter stringWriter;
            private TextWriter originalOutput;

            public ConsoleRedirectionToStringWriter()
            {
                stringWriter = new StringWriter();
                originalOutput = Console.Out;
                Console.SetOut(stringWriter);
            }

            public string GetOutput()
            {
                return stringWriter.ToString();
            }

            public void Dispose()
            {
                Console.SetOut(originalOutput);
                stringWriter.Dispose();
            }
        }

        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceStateOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("test.pdf");
                string to = "reciver";
                multifunctionalDevice.SendFax(in doc1, to);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceStateOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("test.pdf");
                string to = "reciver";
                multifunctionalDevice.SendFax(in doc1, to);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanAndFax_DeviceStateOff()
        {
            var copier = new MultifunctionalDevice();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                string to = "1234";
                copier.ScanAndSendFax(to);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanAndFax_DeviceStateOn()
        {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                string to = "reciver";
                copier.ScanAndSendFax(to);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_FaxCounter()
        {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();
            string to = "reciver";
            IDocument doc1 = new PDFDocument("test.pdf");
            copier.SendFax(in doc1, to);
            IDocument doc2 = new TextDocument("test.txt");
            copier.SendFax(in doc2, to);
            IDocument doc3 = new ImageDocument("test.jpg");
            copier.SendFax(in doc3, to);

            copier.PowerOff();
            copier.SendFax(in doc3, to);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndSendFax(to);
            copier.ScanAndSendFax(to);

            // 5 wydruków, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(5, copier.FaxCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_FaxRecievedCounter()
        {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            string from = "sender";

            IDocument doc1 = new PDFDocument("test.pdf");
            IDocument doc2 = new TextDocument("test.txt");
            IDocument doc3 = new ImageDocument("test.jpg");
            IDocument doc4 = new ImageDocument("test.jpg");

            copier.ReceiveFax(in doc1, from);
            copier.ReceiveFax(in doc2, from);
            copier.ReceiveFax(in doc3, from);

            copier.PowerOff();
            copier.ReceiveFax(in doc4, from);
            copier.PowerOn();

            Assert.AreEqual(3, copier.FaxReceivedCounter);
        }
    }
}