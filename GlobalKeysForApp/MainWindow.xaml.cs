using GlobalKeysForApp.ViewModel;
using System.Windows;

namespace GlobalKeysForApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
            viewModel.GetFocusedEvent += ViewModel_GetFocusedEvent;
        }

        private void ViewModel_GetFocusedEvent()
        {
            this.WindowState = WindowState.Minimized;
            Show();
            this.WindowState = WindowState.Normal;
        }
    }
}
