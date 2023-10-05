using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmptyBank.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для BankWindow.xaml
    /// </summary>
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
