using System.Windows;
using CRM.MVVM.M;
using CRM.MVVM.VM;

namespace CRM.MVVM.V
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser(User user)
        {
            InitializeComponent();
            DataContext = new AddUserVM(user, this);
        }
    }
}
