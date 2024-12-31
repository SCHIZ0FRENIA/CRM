using System.Windows.Controls;
using CRM.MVVM.VM.AppUser.Controls;

namespace CRM.MVVM.V.AppUser.Controls
{
    /// <summary>
    /// Interaction logic for CalendarV.xaml
    /// </summary>
    public partial class CalendarV : UserControl
    {
        public CalendarV()
        {
            InitializeComponent();
            DataContext = new CalendarVM();
        }
    }
}
