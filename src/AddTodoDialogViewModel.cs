using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;

namespace Todos
{
    public class AddTodoDialogViewModel : ObservableObject, IModalDialogViewModel
    {
        private readonly RelayCommand okCommand;

        private string name;
        private bool? dialogResult;

        public AddTodoDialogViewModel()
        {
            okCommand = new RelayCommand(Ok, CanOk);
        }

        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);

                okCommand.NotifyCanExecuteChanged();
            }
        }

        public ICommand OkCommand => okCommand;

        public bool? DialogResult
        {
            get => dialogResult;
            private set => SetProperty(ref dialogResult, value);
        }

        private void Ok()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                DialogResult = true;
            }
        }

        private bool CanOk()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }
}
