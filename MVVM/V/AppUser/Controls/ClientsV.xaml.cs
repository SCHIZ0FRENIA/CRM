using System.Windows.Controls;
using CRM.MVVM.VM.AppUser.Controls;

namespace CRM.MVVM.V.AppUser.Controls
{
    /// <summary>
    /// Interaction logic for ClientsV.xaml
    /// </summary>
    public partial class ClientsV : UserControl
    {
        public ClientsV()
        {
            InitializeComponent();
            DataContext = new ClientsVM();
        }
    }
}
