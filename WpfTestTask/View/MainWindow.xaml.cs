using System.Windows;
using WpfTestTask.ViewModel;

namespace WpfTestTask.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new UserInfoViewModel();
        }
    }
}