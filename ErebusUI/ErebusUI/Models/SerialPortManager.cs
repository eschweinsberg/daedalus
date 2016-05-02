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
        public /*static*/ SerialPort serialPort { get; set; }
        private static bool _continue { get; set; }
        //private Thread readThread;
        public static string packet { get; set; }
        #endregion Properties

        public SerialPortManager(string portName)
        {
            if (portName == null)
            {
                portName = "COM1";
            }
            _continue = true;
            // Configure the Serial Port here.
            serialPort = new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = 57600;


            // Start a thread to listen to the serial port.
            //readThread = new Thread(Read);
            // Everything Below can be left to defaults in order to interface with Arduino. The Baud rate and port name are all that matter.
            //serialPort.Parity = null;         // I am not 100% sure this needs to be set.
            //serialPort.DataBits = 64;
            //serialPort.StopBits = 1;
            //serialPort.Handshake = null;
            // Set the read/write timeouts
            //serialPort.ReadTimeout = 500;
            //serialPort.WriteTimeout = 500;
        }

        public Response WriteToSerialPort(string package)
        {
            bool success = false;
            string responseMessage = "";
            if(serialPort.IsOpen){
                try
                {
                    serialPort.Write(package);
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.Write(String.Format("Error writing to Serial Port: {0}", ex.Message));
                    responseMessage = ex.Message;
                }
            }
            else
            {
                try
                {
                    serialPort.Open();
                    serialPort.Write(package);
                    serialPort.Close();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.Write(String.Format("Error writing to Serial Port: {0}", ex.Message));
                    responseMessage = ex.Message;
                }
            }
            Response resp = new Response(success, responseMessage);
            return resp;
        }

        //public void QuitReadingFromPort()
        //{
        //    //Rejoin the thread into main.
        //    readThread.Join();
        //}

        //private static void Read()
        //{
        //    while (_continue)
        //    {
        //        try
        //        {
        //            string message = serialPort.ReadLine();
        //            Console.WriteLine(message);
        //            lock (message)
        //            {
        //                packet = message;
        //            }
        //        }
        //        catch (TimeoutException) { }
        //    }
        //}
    }

    class Response
    {
        public bool success { get; set; }
        public string responseMessage { get; set; }
        public Response(bool success, string responseMessage)
        {
            this.success = success;
            this.responseMessage = responseMessage;
        }
    }
}
