using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace login_page_example_cs;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void open_login(object sender, RoutedEventArgs e)
    {
        /**
         * Change page.
         * Set the login: Hidden.
         */

        login_page.Visibility = Visibility.Hidden;

        /**
         * Set the chat page on Visible.
         */

        chats_page.Visibility = Visibility.Visible;
    }

    private void back(object sender, RoutedEventArgs e)
    {
        

        login_page.Visibility = Visibility.Hidden;

        // Set the chat page on Visible.
        chats_page.Visibility = Visibility.Visible;
    }
}