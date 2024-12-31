using System.Windows;
using CRM.MVVM.M;
using CRM.MVVM.VM.AppUser.Controls.Windows;

namespace CRM.MVVM.V.AppUser.Controls.Windows
{
    /// <summary>
    /// Interaction logic for AddAppointment.xaml
    /// </summary>
    public partial class AddAppointment : Window
    {
        public AddAppointment(Appointment appointment, int id)
        {
            InitializeComponent();
            DataContext = new AddAppointmentVM(appointment, this, id);
        }
    }
}
