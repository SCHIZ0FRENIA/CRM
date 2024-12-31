using System.Windows.Controls;
using CRM.Core;
using CRM.MVVM.VM.Authorization;

namespace CRM.MVVM.V.Authorization
{
    /// <summary>
    /// Interaction logic for RegistrationV.xaml
    /// </summary>
    public partial class RegistrationV : UserControl
    {
        public RegistrationV(RelayCommand change, Action StartUserCommand)
        {
            DataContext = new RegistrationVM(change, StartUserCommand);
            InitializeComponent();
        }
    }
}
