using System.Windows;
using System.Windows.Input;

namespace EmptyBank.MVVM.View
{
    public partial class BankWindow : Window
    {
        public BankWindow()
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
    }
}
