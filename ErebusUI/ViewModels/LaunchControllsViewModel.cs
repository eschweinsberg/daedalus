using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace ErebusUI.ViewModels
{
    public enum LaunchControlls
    {
        IgniteOne = 1,
        IgniteTwo = 2,
        DeployParachuteOne = 3,
        DeployParachuteTwo = 4,
        StopOneIgnition = 5,
        StopTwoIgnition = 6
    }

    class LaunchControllsViewModel : ViewModelBase
    {
        public int oneAlt { get; set; }
        public int twoAlt { get; set; }
        public int onespeed { get; set; }
        public int twospeed { get; set; }
        public double oneLat { get; set; }
        public double oneLon { get; set; }
        public double twoLat { get; set; }
        public double twoLon { get; set; }

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
            var something = 0;
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

        }

        public bool CanExecuteDisableIgnitionStage1Command()
        {
            return true;
        }

        private ICommand _XTRACommand;
        public ICommand XTRACommand
        {
            get
            {
                if (_XTRACommand == null)
                {
                    _XTRACommand = new RelayCommand(XTRAExecute, CanExecuteXTRACommand);
                }

                return _XTRACommand;
            }
        }

        public void XTRAExecute()
        {

        }

        public bool CanExecuteXTRACommand()
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

        }

        public bool CanExecuteDisableIgnition2Command()
        {
            return true;
        }


        //Constructor
        public LaunchControllsViewModel()
        {

        }


        //Methods
        public void BeginGraphing()
        {

        }

    }
}
