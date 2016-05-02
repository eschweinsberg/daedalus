using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ErebusUI.Models;
using System.IO.Ports;
using System.ComponentModel;
using System.Windows.Data;
using System.Timers;

namespace ErebusUI.ViewModels
{
    class LaunchControllsViewModel : ViewModelBase
    {
        #region properties
        private string _altitudeStageOne;
        public string altitudeStageOne
        {
            get
            {
                return _altitudeStageOne;
            }
            set
            {
                _altitudeStageOne = value;
                OnPropertyChanged("altitudeStageOne");
            }
        }
        private string _altitudeStageTwo;
        public string altitudeStageTwo
        {
            get
            {
                return _altitudeStageTwo;
            }
            set
            {
                _altitudeStageTwo = value;
                OnPropertyChanged("altitudeStageTwo");
            }
        }
        private string _velocityStageOne;
        public string velocityStageOne
        {
            get
            {
                return _velocityStageOne;
            }
            set
            {
                _velocityStageOne = value;
                OnPropertyChanged("velocityStageOne");
            }
        }
        private string _velocityStageTwo;
        public string velocityStageTwo
        {
            get
            {
                return _velocityStageTwo;
            }
            set
            {
                _velocityStageTwo = value;
                OnPropertyChanged("velocityStageTwo");
            }
        }
        private string _latitudeStageOne;
        public string latitudeStageOne
        {
            get
            {
                return _latitudeStageOne;
            }
            set
            {
                _latitudeStageOne = value;
                OnPropertyChanged("latitudeStageOne");
            }
        }
        private string _longitudeStageOne;
        public string longitudeStageOne
        {
            get
            {
                return _longitudeStageOne;
            }
            set
            {
                _longitudeStageOne = value;
                OnPropertyChanged("longitudeStageOne");
            }
        }
        private string _latitudeStageTwo;
        public string latitudeStageTwo
        {
            get
            {
                return _latitudeStageTwo;
            }
            set
            {
                _latitudeStageTwo = value;
                OnPropertyChanged("latitudeStageTwo");
            }
        }
        private string _longitudeStageTwo;
        public string longitudeStageTwo
        {
            get
            {
                return _longitudeStageTwo;
            }
            set
            {
                _longitudeStageTwo = value;
                OnPropertyChanged("longitudeStageTwo");
            }
        }
        private ICollectionView _availablePorts;
        public ICollectionView availablePorts
        {
            get
            {
                return _availablePorts;
            }
            set
            {
                _availablePorts = value;
                OnPropertyChanged("availablePorts");
            }
        }
        private string _chosenSerialPortName;
        public string chosenSerialPortName
        {
            get
            {
                if(_chosenSerialPortName == null){
                    return "COM1";
                }
                return _chosenSerialPortName;
            }
            set
            {
                _chosenSerialPortName = value;
                OnPropertyChanged("chosenSerialPortName");
            }
        }

        private bool _isSerialStarted;
        public bool isSerialStarted
        {
            get
            {
                return _isSerialStarted;
            }
            set
            {
                _isSerialStarted = value;
                OnPropertyChanged("isSerialStarted");
            }
        }

        private string _parsingMessages;
        public string parsingMessages
        {
            get
            {
                return _parsingMessages;
            }
            set
            {
                _parsingMessages = value;
                OnPropertyChanged("parsingMessages");
            }
        }

        private Response _recentResponse;
        public Response recentResponse
        {
            get
            {
                if (_recentResponse == null)
                {
                    _recentResponse = new Response(true, "");
                }
                return _recentResponse;
            }
            set
            {
                _recentResponse = value;
                if (_recentResponse.success == false)
                {
                    parsingMessages += DateTime.Now.ToShortTimeString() + ": " + _recentResponse.responseMessage + "\n";
                }
                OnPropertyChanged("recentResponse");
            }
        }

        public SerialPortManager portManager { get; set; }
        //public Timer messageParsingTimer;
        public Timer okTimer;
        #endregion properties

        public LaunchControllsViewModel()   // Constructor
        {
            //portManager = new SerialPortManager();
            isSerialStarted = false;
            availablePorts = CollectionViewSource.GetDefaultView(SerialPort.GetPortNames().ToList());
            //messageParsingTimer = new Timer(100);
            //messageParsingTimer.Start();
            //messageParsingTimer.Elapsed += new ElapsedEventHandler(ParsePacket);
            okTimer = new Timer(1000);
            okTimer.Start();
            okTimer.Elapsed += new ElapsedEventHandler(SendOK);
            parsingMessages = "";
        }

        public void SendOK(Object sender, EventArgs e)
        {
            if(isSerialStarted){
                okTimer.Enabled = false;
                portManager.WriteToSerialPort("CMD_OKOK_ONE");
                portManager.WriteToSerialPort("CMD_OKOK_TWO");
                okTimer.Enabled = true;
            }
        }

        public void ParsePacket(Object sender, EventArgs e)
        {
            //messageParsingTimer.Enabled = false;
            // Actual code.
            SerialPort port = (SerialPort)sender;
            string packet = port.ReadLine();        // Consider changing this to a read line. Looking for \n is important.
            List<string> packets = new List<string>();
            packets.Add(packet);
            string filelocation = @"C:\sample.txt";
            System.IO.File.WriteAllLines(filelocation, packets);  // Change this.
            //string packet = portManager.serialPort.ReadLine();
            // Fors testing purposes.
            //string packet = "2010006 343 322.123543-180.123456";

            // Do packet parsing here.
            //char [] _packet = packet.ToCharArray();
            // packet contents: in total length of characters.
            // stage(1), altitude(6), velocity(4), longitude(11), latitude(11)
            if (packet != null)
            {
                //  Rewrite this based on the X,Altitude,Lat,Long setup.

                try
                {
                    string[] parts = packet.Split(',');
                    if(parts[0] == "1")
                    {
                        // Launch Vehicle
                    }
                    else if(parts[0] == "2"){
                        // Payload
                    }
                    else{
                        // This shouldn't happen. 
                        throw new Exception("Packet Error, '" + packet + "'");
                    }
                }
                catch(Exception ex)
                {
                    parsingMessages += DateTime.Now.ToShortTimeString() + ": " + ex.Message + "\n";
                }
                //char stage = packet[0];
                //try
                //{
                //    if (stage == '1')
                //    {
                //        //Set stage 1 values.
                //        //altitudeStageOne = Convert.ToInt32(packet.Substring(1, 7));
                //        //velocityStageOne = Convert.ToInt32(packet.Substring(8, 11));
                //        //longitudeStageOne = Convert.ToDouble(packet.Substring(12, 22));
                //        //latitudeStageOne = Convert.ToDouble(packet.Substring(23,33));

                //        altitudeStageOne = packet.Substring(1, 7);
                //        velocityStageOne = packet.Substring(8, 4);
                //        longitudeStageOne = packet.Substring(11, 11);
                //        latitudeStageOne = packet.Substring(22, 11);
                //    }
                //    else if (stage == '2')
                //    {
                //        //Set stage 2 values.
                //        //altitudeStageTwo = Convert.ToInt32(packet.Substring(1, 7));
                //        //velocityStageTwo = Convert.ToInt32(packet.Substring(8, 11));
                //        //longitudeStageTwo = Convert.ToDouble(packet.Substring(12, 22));
                //        //latitudeStageTwo = Convert.ToDouble(packet.Substring(23, 33));

                //        altitudeStageTwo = packet.Substring(1, 7);
                //        velocityStageTwo = packet.Substring(8, 4);
                //        longitudeStageTwo = packet.Substring(11, 11);
                //        latitudeStageTwo = packet.Substring(22, 11);
                //    }
                //    else
                //    {
                //        // Somethins is wrong.....
                //        parsingMessages += DateTime.Now.ToShortTimeString() + ": " + "Could not identify a stage for incoming data.\n";
                //    }
                //}
                //catch (Exception ex)
                //{
                //    parsingMessages += DateTime.Now.ToShortTimeString() + ": " + ex.Message + "\n";
                //}
            }

            //messageParsingTimer.Enabled = true;
        }

        #region Commands
        private ICommand _IgniteStage1Command;
        public ICommand IgniteStage1Command
        {
            get
            {
                if (_IgniteStage1Command == null)
                {
                    _IgniteStage1Command = new RelayCommand(IgniteStage1Execute, CanExecuteIgniteStage1Command);
                }
                return _IgniteStage1Command;
            }
        }

        private bool CanExecuteIgniteStage1Command()
        {
            //Conditions on if this command can be executed. (No, if the disable ignition has been triggered.)
            return true;
        }

        public void IgniteStage1Execute()
        {
            // Send the command to ignite stage 1 here.
            // All Commands in the format "CMD_****_Stage_***"
            recentResponse = portManager.WriteToSerialPort("CMD_IGNT_ONE");
        }

        private ICommand _DeployStage1Command;
        public ICommand DeployStage1Command
        {
            get
            {
                if (_DeployStage1Command == null)
                {
                    _DeployStage1Command = new RelayCommand(DeployStage1Execute, CanExecuteDeployStage1Command);
                }

                return _DeployStage1Command;
            }
        }

        public void DeployStage1Execute()
        {
            recentResponse = portManager.WriteToSerialPort("CMD_DPLY_ONE");
        }

        public bool CanExecuteDeployStage1Command()
        {
            return true;
        }

        private ICommand _DisableIgnition1Command;
        public ICommand DisableIgnition1Command
        {
            get
            {
                if (_DisableIgnition1Command == null)
                {
                    _DisableIgnition1Command = new RelayCommand(DisableStage1Execute, CanExecuteDisableIgnitionStage1Command);
                }

                return _DisableIgnition1Command;
            }
        }

        public void DisableStage1Execute()
        {
            recentResponse = portManager.WriteToSerialPort("CMD_DISI_ONE");
        }

        public bool CanExecuteDisableIgnitionStage1Command()
        {
            return true;
        }

        private ICommand _DisableParachute1Command;
        public ICommand DisableParachute1Command
        {
            get
            {
                if (_DisableParachute1Command == null)
                {
                    _DisableParachute1Command = new RelayCommand(DisableParachute1Execute, CanExecuteDisableParachute1Command);
                }

                return _DisableParachute1Command;
            }
        }

        public void DisableParachute1Execute()
        {
            recentResponse = portManager.WriteToSerialPort("CMD_DISP_ONE");
        }

        public bool CanExecuteDisableParachute1Command()
        {
            return true;
        }

        private ICommand _IgniteStage2Command;
        public ICommand IgniteStage2Command
        {
            get
            {
                if (_IgniteStage2Command == null)
                {
                    _IgniteStage2Command = new RelayCommand(IgniteStage2Execute, CanExecuteIgniteStage2Command);
                }

                return _IgniteStage2Command;
            }
        }

        public void IgniteStage2Execute()
        {
            recentResponse = portManager.WriteToSerialPort("CMD_IGNT_TWO");
        }

        public bool CanExecuteIgniteStage2Command()
        {
            return true;
        }

        private ICommand _DeployStage2Command;
        public ICommand DeployStage2Command
        {
            get
            {
                if (_DeployStage2Command == null)
                {
                    _DeployStage2Command = new RelayCommand(DeployStage2Execute, CanExecuteDeployStage2Command);
                }

                return _DeployStage2Command;
            }
        }

        public void DeployStage2Execute()
        {
            recentResponse = portManager.WriteToSerialPort("CMD_DPLY_TWO");
        }

        public bool CanExecuteDeployStage2Command()
        {
            return true;
        }

        private ICommand _DisableIgnition2Command;
        public ICommand DisableIgnition2Command
        {
            get
            {
                if (_DisableIgnition2Command == null)
                {
                    _DisableIgnition2Command = new RelayCommand(DisableIgnition2Execute, CanExecuteDisableIgnition2Command);
                }

                return _DisableIgnition2Command;
            }
        }

        public void DisableIgnition2Execute()
        {
            recentResponse = portManager.WriteToSerialPort("CMD_DISI_TWO");
        }

        public bool CanExecuteDisableIgnition2Command()
        {
            return true;
        }

        private ICommand _DisableParachute2Command;
        public ICommand DisableParachute2Command
        {
            get
            {
                if (_DisableParachute2Command == null)
                {
                    _DisableParachute2Command = new RelayCommand(DisableParachute2Execute, CanExecuteDisableParachute2Command);
                }

                return _DisableParachute2Command;
            }
        }

        public void DisableParachute2Execute()
        {
            recentResponse = portManager.WriteToSerialPort("CMD_DISP_TWO");
        }

        public bool CanExecuteDisableParachute2Command()
        {
            return true;
        }

        private ICommand _BeginSerialPort;
        public ICommand BeginSerialPort
        {
            get
            {
                if (_BeginSerialPort == null)
                {
                    _BeginSerialPort = new RelayCommand(BeginSerialPortExecute, CanExecuteBeginSerialPort);
                }
                return _BeginSerialPort;
            }
        }

        public void BeginSerialPortExecute()
        {
            isSerialStarted = true;
            portManager = new SerialPortManager(chosenSerialPortName);
            portManager.serialPort.DataReceived += new SerialDataReceivedEventHandler(ParsePacket);
        }

        public bool CanExecuteBeginSerialPort()
        {
            return true;
        }
        #endregion Commands
    }
}
