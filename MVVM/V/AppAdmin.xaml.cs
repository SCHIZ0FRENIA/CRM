using System.Windows.Controls;
using CRM.MVVM.VM;

namespace CRM.MVVM.V
{
    /// <summary>
    /// Interaction logic for AppAdmin.xaml
    /// </summary>
    public partial class AppAdmin : UserControl
    {
        public AppAdmin()
        {
            InitializeComponent();
            DataContext = new AppAdminVM();
        }
    }
}
