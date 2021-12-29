using CommunityToolkit.Mvvm.ComponentModel;

namespace Todos
{
    public class TodoViewModel : ObservableObject
    {
        private string name;
        private bool isCompleted;

        public TodoViewModel(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set => SetProperty(ref isCompleted, value);
        }
    }
}
