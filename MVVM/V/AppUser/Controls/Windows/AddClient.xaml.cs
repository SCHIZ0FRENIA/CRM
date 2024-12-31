using System.Windows;
using CRM.MVVM.M;
using CRM.MVVM.VM.AppUser.Controls.Windows;

namespace CRM.MVVM.V.AppUser.Controls.Windows
{
    /// <summary>
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public AddClient(Client client)
        {
            InitializeComponent();
            DataContext = new AddClientVM(client, this);
        }
    }
}
