using System.Windows;
using CRM.MVVM.M;
using CRM.MVVM.VM.AppUser.Controls.Windows;

namespace CRM.MVVM.V.AppUser.Controls.Windows
{
    /// <summary>
    /// Interaction logic for AddNote.xaml
    /// </summary>
    public partial class AddNote : Window
    {
        public AddNote(Note note, int id)
        {
            InitializeComponent();
            DataContext = new AddNoteVM(note, this, id);
        }
    }
}
