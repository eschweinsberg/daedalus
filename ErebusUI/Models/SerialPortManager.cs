using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ErebusUI.Models
{
    class SerialPortManager     //This is the class to control the USB Serial ports communications.
    {
        #region Properties
        private static SerialPort serialPort { get; set; }
        private static bool _continue { get; set; }
        private Thread readThread;
        #endregion Properties

        public SerialPortManager() {
            _continue = true;
            readThread = new Thread(Read);
            serialPort = new SerialPort();
            //Configure the Serial Port here.
            // Go over all of this make sure nothing is being missed. JOE.
            serialPort.PortName = "";
            serialPort.BaudRate = 9600;
            //serialPort.Parity = null;         // I am not 100% sure this needs to be set.
            serialPort.DataBits = 64;
            //serialPort.StopBits = 1;
            //serialPort.Handshake = null;

            // Set the read/write timeouts
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;
        }

        public bool WriteToSerialPort(string package) {
            // Make this return success or failure.
            // Make sure to open the port and do all config neccessary before writing. 
            bool success = false;
            try
            {
                serialPort.Write(package);
                success = true;
            }
            catch (Exception ex)
            {
                Console.Write(String.Format("Error writing to Serial Port: {0}", ex.Message));
            }
            return success;
        }

        public void QuitReadingFromPort()
        {
            //Rejoin the thread into main.
            readThread.Join();
        }

        private static void Read()
        {
            while (_continue)
            {
                try
                {
                    string message = serialPort.ReadLine();
                    Console.WriteLine(message);
                }
                catch (TimeoutException) { }
            }
        }

        //Useless Code. Sample stuff is better.
        //public string ReadFromSerialPort()
        //{
        //    string result = "";
        //    try
        //    {   // Read until /n character in the serial port. This should be every line that is sent back from the device.
        //        result = serialPort.ReadLine();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Something went wrong reading from the serial Port. 
        //        Console.Write(String.Format("Error reading from Serial Port: {0}", ex.Message));
        //    }
        //    return result;
        //}
    }
}
