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
    /// Logika interakcji dla klasy UserControlSearchProduct.xaml
    /// </summary>
    public partial class UserControlSearchProduct : UserControl
    {
        public string search;

        List<UserControlProductInCategory> items = new List<UserControlProductInCategory>();
        public UserControlSearchProduct()
        {
            InitializeComponent();

            IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;
            window.toSearch.Text = search;

            Server.SendString("product_search " + search);
            string response = Server.ReceiveResponse();

            int NumberOfProducts = 0;

            string[] products = response.Split(';');

            foreach (string product in products)
            {
                try
                {
                    string[] param = product.Split(',');
                    string id = param[0];
                    string ProductName = param[1];
                    string BrandName = param[2];
                    string Price = param[3];

                    AddProductToList(int.Parse(id), ProductName, BrandName, int.Parse(Price), search);
                    NumberOfProducts++;
                }
                catch (Exception)
                {
                }
            }
            if (NumberOfProducts == 0)
            {
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Brak produktów!";
            }
        }
        public void AddProductToList(int id, string productName, string brandName, int price, string search)
        {
            UserControlProductInCategory prod = new UserControlProductInCategory();
            prod.SetProductProperty(id, productName, brandName, price, search);
            items.Add(prod);
            ProductsList.ItemsSource = items;
        }
    }
}