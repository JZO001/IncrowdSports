using System.Windows.Input;

namespace Jzo.IncrowdSports.Shared.Codes
{

    /// <summary>A generic (sync) command implementation</summary>
    public class GenericCommand : ICommand
    {

        private readonly Action<object> mAction;

        private readonly Func<object, bool> mCanExecute;

        public event EventHandler? CanExecuteChanged;

        public GenericCommand(Action<object> action, Func<object, bool> canExecute)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (canExecute == null) throw new ArgumentNullException("canExecute");

            mAction = action;
            mCanExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return mCanExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            mAction(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }

}
