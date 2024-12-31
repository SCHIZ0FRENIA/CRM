using System.Windows.Controls;
using CRM.Core;
using CRM.MVVM.VM.Authorization;

namespace CRM.MVVM.V.Authorization
{
    /// <summary>
    /// Interaction logic for MainAuthorizationV.xaml
    /// </summary>
    public partial class MainAuthorizationV : UserControl
    {
        public MainAuthorizationV(Action StartUserCommand, Action StartAdminCommand)
        {
            DataContext = new MainAuthorizationVM(StartUserCommand, StartAdminCommand);
            InitializeComponent();
        }
    }
}
