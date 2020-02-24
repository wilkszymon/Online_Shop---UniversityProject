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
    /// Logika interakcji dla klasy UserControlProductsCategory.xaml
    /// </summary>
    public partial class UserControlProductsCategory : UserControl
    {
        List<UserControlProductInCategory> items = new List<UserControlProductInCategory>();

        public UserControlProductsCategory(string category)
        {
            InitializeComponent();

            Server.SendString("show_category " + category);
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
                    AddProductToList(int.Parse(id), ProductName, BrandName, int.Parse(Price), category);
                    NumberOfProducts++;
                }
                catch (Exception)
                {

                }
            }
            switch(category)
            {
                case "Smartphone":
                    CategoryName.Text ="Smartfony(" + NumberOfProducts + ")";
                    break;
                case "Monitors":
                    CategoryName.Text = "Monitory(" + NumberOfProducts + ")";
                    break;
                case "Tablets":
                    CategoryName.Text = "Tablety(" + NumberOfProducts + ")";
                    break;
                default:
                    break;
            }

            if(NumberOfProducts == 0)
            {
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Brak produktów!";
            }
           

        }

        public void AddProductToList(int id,string productName,string brandName, int price, string category)
        {
            UserControlProductInCategory prod = new UserControlProductInCategory();
            prod.SetProductProperty(id, productName, brandName, price, category);
            items.Add(prod);
            ProductsList.ItemsSource = items;
        }

        private void ButtonCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
