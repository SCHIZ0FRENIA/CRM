using System.Windows;
using CRM.MVVM.VM;

namespace CRM.MVVM.V
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainVM();
            InitializeComponent();
        }
    }
}