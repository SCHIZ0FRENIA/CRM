using System.Windows;
using CRM_Build.MVVM.VM;

namespace CRM_Build.MVVM.V
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainVM();
            InitializeComponent();
        }
    }
}
