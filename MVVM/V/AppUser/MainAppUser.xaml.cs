using System.Windows.Controls;
using CRM.Core;
using CRM.MVVM.VM.AppUser;

namespace CRM.MVVM.V.AppUser
{
    /// <summary>
    /// Interaction logic for MainAppUser.xaml
    /// </summary>
    public partial class MainAppUser : UserControl
    {
        public MainAppUser()
        {
            DataContext = new AppUserVM();
            InitializeComponent();
        }
    }
}
