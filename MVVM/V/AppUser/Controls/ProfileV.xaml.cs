using System.Windows.Controls;
using CRM.MVVM.VM.AppUser.Controls;

namespace CRM.MVVM.V.AppUser.Controls
{
    /// <summary>
    /// Interaction logic for ProfileV.xaml
    /// </summary>
    public partial class ProfileV : UserControl
    {
        public ProfileV()
        {
            InitializeComponent();
            DataContext = new ProfileVM();
        }
    }
}
