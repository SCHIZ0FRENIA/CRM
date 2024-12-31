using System.Windows.Controls;
using CRM.Core;
using CRM.MVVM.VM.Authorization;

namespace CRM.MVVM.V.Authorization
{
    /// <summary>
    /// Interaction logic for AuthorizationV.xaml
    /// </summary>
    public partial class AuthorizationV : UserControl
    {
        public AuthorizationV(RelayCommand change, Action StartUserCommand, Action StartAdminCommand)
        {
            InitializeComponent();
            DataContext = new AuthorizationVM(change, StartUserCommand, StartAdminCommand);
        }
    }
}
