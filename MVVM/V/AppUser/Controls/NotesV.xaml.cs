using System.Windows.Controls;
using CRM.MVVM.VM.AppUser.Controls;

namespace CRM.MVVM.V.AppUser.Controls
{
    /// <summary>
    /// Interaction logic for NotesV.xaml
    /// </summary>
    public partial class NotesV : UserControl
    {
        public NotesV()
        {
            InitializeComponent();
            DataContext = new NotesVM();
        }
    }
}
