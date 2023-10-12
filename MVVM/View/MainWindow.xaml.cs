using EmptyBank.MVVM.Model;
using EmptyBank.MVVM.View;
using System.Windows;
using System.Windows.Input;

namespace EmptyBank
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ShutdownWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        void CloseWindow()
        {
            var thisWindow = Application.Current.Windows[0];
            if (thisWindow != null) thisWindow.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ServerModel serverModel = new ServerModel();
            if (Properties.Settings.Default.IsRemember)
            {
                serverModel.Id = Properties.Settings.Default.userID;
                serverModel.Server();
                var openBankWindow = new BankWindow();
                openBankWindow.Show();
                CloseWindow();
            }
        }
    }
}
