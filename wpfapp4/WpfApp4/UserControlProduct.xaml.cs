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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Logika interakcji dla klasy UserControlProduct.xaml
    /// </summary>
    public partial class UserControlProduct : UserControl
    {
        private int Id;
        private int Quantity = 1;

        public UserControlProduct()
        {
            InitializeComponent();
        }

        public void createDescribe(int id, string name, string brandName, int price)
        {
            Id = id;
            ProductName.Text = brandName + " " + name;
            ProductPrice.Text = price + " zł";

            Server.SendString("show_product " + id);
            string response = Server.ReceiveResponse();

            int end = response.IndexOf(",", 0);
            ProductDescribe.Text = response.Substring(0, end);

            Quantity = int.Parse(response.Substring(end + 1, response.Length - end - 1));
        }

        public int GetId()
        {
            return Id;
        }

        private void ButtonY51_Click(object sender, RoutedEventArgs e)
        {
            Picture.ImageSource = Picture1.ImageSource;
        }

        private void ButtonY52_Click(object sender, RoutedEventArgs e)
        {
            Picture.ImageSource = Picture2.ImageSource;
        }

        private void ButtonY53_Click(object sender, RoutedEventArgs e)
        {
            Picture.ImageSource = Picture3.ImageSource;
        }
        private void ButtonY54_Click(object sender, RoutedEventArgs e)
        {
            Picture.ImageSource = Picture4.ImageSource;
        }
        private void ButtonY55_Click(object sender, RoutedEventArgs e)
        {
            Picture.ImageSource = Picture5.ImageSource;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (User.GetID() == -1)
            {
                GoToUserControlLogin();
            }
            else
            {
                Server.SendString("cart_add " + Id.ToString() + " " + User.GetID().ToString() + " 1");
                string response = Server.ReceiveResponse();
                if (response == null)
                {
                    Environment.Exit(0);
                }
                else if(response == "Correct")
                {
                    Label_AddToCard.Visibility = Visibility.Visible;
                    Label_AddToCard.Content = "Dodano do koszyka";
                }
            }
        }

        private void ButtonBuyNow_Click(object sender, RoutedEventArgs e)
        {
            if (User.GetID() == -1)
            {
                GoToUserControlLogin();
            }
            else
            {
                IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
                MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

                UserControlCart userControlLogin = new UserControlCart();
                window.GridHome.Children.Add(userControlLogin);
            }
        }

        private void GoToUserControlLogin()
        {
            IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

            UserControlLogin userControlLogin = new UserControlLogin();
            window.GridHome.Children.Add(userControlLogin);
        }

    }
}
