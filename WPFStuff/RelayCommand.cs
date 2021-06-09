using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFStuff
{
    public class ExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; init; }
        public ExceptionEventArgs(Exception ex) { this.Exception = ex; }
    }

    public delegate void ExceptionEventHandler(object sender, ExceptionEventArgs args);


    public class RelayCommand : ICommand
    {

        private Action<object> execute = null;
        private Predicate<object> canExecute = null;

        public event ExceptionEventHandler ExceptionInExecute;

        public string Name { get; init; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return canExecute(parameter);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            try
            {
                execute(parameter);
            }
            catch (Exception ex)
            {
                if (ExceptionInExecute != null)
                    ExceptionInExecute(this, new(ex));
                else
                    MessageBox.Show(
                        ex.ToString(),
                        $"An error occurred:{ex.Message}",
                        MessageBoxButton.OK
                        );
            }

        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute, string name, ExceptionEventHandler onEx = null)
        {
            (this.execute, this.canExecute, this.Name) = (execute, canExecute, name);

            if (onEx != null)
                this.ExceptionInExecute += onEx;
        }
    }

}
