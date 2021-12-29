using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;

namespace Todos
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly IDialogService dialogService;
        private readonly RelayCommand addCommand;
        private readonly RelayCommand clearCompletedCommand;

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            addCommand = new RelayCommand(Add);
            clearCompletedCommand = new RelayCommand(ClearCompleted, CanClearCompleted);

            Todos.CollectionChanged += OnTodoCollectionChanged;
        }

        public ObservableCollection<TodoViewModel> Todos { get; } = new ObservableCollection<TodoViewModel>();

        public ICommand AddCommand => addCommand;

        public ICommand ClearCompletedCommand => clearCompletedCommand;

        private void Add()
        {
            var dialogViewModel = new AddTodoDialogViewModel();

            var success = dialogService.ShowDialog(this, dialogViewModel);
            if (success == true)
            {
                Todos.Add(new TodoViewModel(dialogViewModel.Name));
            }
        }

        private void ClearCompleted()
        {
            var result = dialogService.ShowMessageBox(this, "Are you sure?", "Clear Completed", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                foreach (var completed in Todos.Where(todo => todo.IsCompleted).ToArray())
                {
                    Todos.Remove(completed);
                }
            }
        }

        private bool CanClearCompleted()
        {
            return Todos.Any(todo => todo.IsCompleted);
        }

        private void OnTodoCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TodoViewModel todo in e.NewItems)
                    {
                        todo.PropertyChanged += OnTodoPropertyChanged;
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (TodoViewModel todo in e.OldItems)
                    {
                        todo.PropertyChanged -= OnTodoPropertyChanged;
                    }
                    break;
            }
        }

        private void OnTodoPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoViewModel.IsCompleted))
            {
                clearCompletedCommand.NotifyCanExecuteChanged();
            }
        }
    }
}
