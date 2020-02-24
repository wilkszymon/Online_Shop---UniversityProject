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
    /// Logika interakcji dla klasy UserControlCart.xaml
    /// </summary>
    public partial class UserControlCart : UserControl

    {
        List<UserControlProductInCart> items = new List<UserControlProductInCart>();
        private int NumberOfProducts = 0;
        private int BasketValue = 0;
        private string CartId = "";

        public UserControlCart()
        {
            InitializeComponent();

            

            Server.SendString("cart_show " + User.GetID());
            string response = Server.ReceiveResponse();

            string[] products = response.Split(';');

            foreach (string product in products)
            {
                try
                {
                    string[] param = product.Split(',');
                    string id = param[0];
                    string productId = param[1];
                    string productName = param[2];
                    string brandName = param[3];
                    string price = param[4];

                    AddProductToCart(int.Parse(id), int.Parse(productId), productName, brandName, int.Parse(price));

                    BasketValue = BasketValue + int.Parse(price);

                    if (NumberOfProducts == 0)
                    {
                        CartId = id;
                    }
                    else
                    {
                        CartId = CartId + "," + id;
                    }

                    NumberOfProducts++;

                }
                catch (Exception)
                {
                    
                }
            }

            CategoryName.Text = "Koszyk(liczba produktow " + NumberOfProducts + ") a wartość to " + BasketValue + "zł";

            if (NumberOfProducts == 0)
            { 
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Brak produktów!";
                ButtonOrder.Visibility = Visibility.Hidden;
            }
        }

        public void AddProductToCart(int id, int productId, string productName, string brandName, int price)
        {
            UserControlProductInCart prod = new UserControlProductInCart();
            prod.SetProductPropertyInCart(id, productId, productName, brandName, price);
            items.Add(prod);
            ProductsList.ItemsSource = items;
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

            UserControlOrder ucObj = new UserControlOrder();
            ucObj.SetData(BasketValue, CartId);
            window.GridHome.Children.Add(ucObj);
        }
    }
}
