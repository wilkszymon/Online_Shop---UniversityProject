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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp4
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GridHome.Children.Add(new UserControlHome());
            Server.ConnectToServer();
        }

        private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            Server.Exit();
            Application.Current.Shutdown();
        }


       private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            UserControlLogin ucObj = new UserControlLogin();
            GridHome.Children.Add(ucObj);
            
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            UserControlRegister ucObj = new UserControlRegister();
            GridHome.Children.Add(ucObj);
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            UserControlHome ucObject = new UserControlHome();
            GridHome.Children.Add(ucObject);
        }

        private void ButtonSmartphone_Click(object sender, RoutedEventArgs e)
        {
            UserControlProductsCategory ucObject = new UserControlProductsCategory("Smartphone");
            GridHome.Children.Add(ucObject);
        }

        private void ButtonMonitor_Click(object sender, RoutedEventArgs e)
        {
            UserControlProductsCategory ucObject = new UserControlProductsCategory("Monitors");
            GridHome.Children.Add(ucObject);
        }

        private void ButtonTablet_Click(object sender, RoutedEventArgs e)
        {
            UserControlProductsCategory ucObject = new UserControlProductsCategory("Tablets");
            GridHome.Children.Add(ucObject);
        }

        private void ButtonAccount_Click(object sender, RoutedEventArgs e)
        {
            UserControlUserData ucObject = new UserControlUserData();
            GridHome.Children.Add(ucObject);
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            User.Logout();
        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            UserControlAddProduct ucObject = new UserControlAddProduct();
            GridHome.Children.Add(ucObject);
        }

        private void ButtonCart(object sender, RoutedEventArgs e)
        {
            UserControlCart ucObject = new UserControlCart();
            GridHome.Children.Add(ucObject);
        }

        private void ButtonOrders_Click(object sender, RoutedEventArgs e)
        {
            UserControlOrders ucObject = new UserControlOrders();
            GridHome.Children.Add(ucObject);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserControlSearchProduct ucObject = new UserControlSearchProduct();
            GridHome.Children.Add(ucObject);
        }

  
    }
}
