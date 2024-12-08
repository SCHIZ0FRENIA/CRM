using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CRM_Build.MVVM.VM
{
    internal class MainVM : INotifyPropertyChanged
    {
        private string _welcomeMessage = "Welcome to CRM Dashboard";

        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set { _welcomeMessage = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
