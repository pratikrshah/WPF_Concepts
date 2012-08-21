using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace BuiltIn_Custom_Commands_Eg
{
    class Example_ViewModel : ViewModelBase
    {
        #region Properties
        private string _txtblck_text;
        private Brush _txtblck_color;
        private bool _isOkToExecute;
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string txtblck_text
        {
            get { return _txtblck_text; }
            set 
            { 
                _txtblck_text = value;
                RaisePropertyChanged("txtblck_text");
            }
        }

        public Brush txtblck_color
        {
            get { return _txtblck_color; }
            set
            {
                _txtblck_color = value;
                RaisePropertyChanged("txtblck_color");
            }
        }

        private bool IsOkToExecute
        {
            get { return _isOkToExecute; }
            set
            {
                _isOkToExecute = value;
                RaisePropertyChanged("IsOkToExecute");
            }
        }

        #endregion

        #region Constructor
        public Example_ViewModel()
        {
            txtblck_color = Brushes.Yellow;
            txtblck_text = "You havent clicked me!!!";
            OkCommand = new myCommand(myOkExecute, myCanOkExecute);
            CancelCommand = new myCommand(myCancelExecute, myCanCancelExecute);
        }
        #endregion

        private void myOkExecute(object parameter)
        {
            #region Issue I faced here
            /* Read this link to get more idea - http://stackoverflow.com/questions/8382612/how-do-i-change-the-background-color-of-a-textblock-in-mvvm-wpf */
            #endregion

            txtblck_color = Brushes.OrangeRed;
            //RaisePropertyChanged("txtblck_color");

            txtblck_text = "You Clicked me!!!";
            //RaisePropertyChanged("txtblck_text");

            #region Another issue I faced here
            /*How to toggle the button enable/disable state - http://stackoverflow.com/questions/10198171/enable-submit-cancel-button-through-wpf-command-pattern*/
            #endregion

            IsOkToExecute = true;
        }

        private bool myCanOkExecute(object parameter)
        {
            #region What issue I faced here
            /*Best Practice - you shouldnt set it here - http://stackoverflow.com/questions/12012167/changing-the-background-color-of-a-textblock-in-mvvm-wpf-and-retaining-it
            The problem is that we shouldn't do any setting of your properties in your myCanOkExecute...because it is that that is being called and changing your properties back to the yellow, etc.
            The CanExecute methods of Commands could be called multiple times and sometimes when you don't expect ...e.g. when the focus changes to a different control, when certain controls are being edited/sent keypress, after a Command has been executed, when someone calls CommandManager.InvalidateRequerySuggested, etc.*/
            #endregion

            //txtblck_color = Brushes.Yellow;
            //txtblck_text = "You havent clicked me!!!";
            return !IsOkToExecute;
        }

        private void myCancelExecute(object parameter)
        {
            txtblck_color = Brushes.Yellow;
            txtblck_text = "You Clicked me back to default settings!!!";

            IsOkToExecute = false;
        }

        private bool myCanCancelExecute(object parameter)
        {
            return IsOkToExecute;
        }
    }
}
