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
using System.Text.RegularExpressions;

namespace WpfApp4
{
    /// <summary>
    /// Logika interakcji dla klasy UserControlAddProduct.xaml
    /// </summary>
    public partial class UserControlAddProduct : UserControl
    {
        public UserControlAddProduct()
        {
            InitializeComponent();
        }

        private void IloscValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);

        }

        private void CenaValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);

        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            AddProductToDatabase();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddProductToDatabase();
            }
        }

        private void AddProductToDatabase()
        {
            if (ProductName.Text == "" || ProductBrandName.Text == "" || ProductPrice.Text == "" || ProductQuantity.Text == "" || ProductDescribe.Text == "")
            {
                LabelInfo.Visibility = Visibility.Visible;
                LabelInfo.Content = "Uzupełnij wszystkie pola!";
                LabelInfo.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                Server.SendString("product_add " + ProductName.Text + " " + ListBoxProductCategory.Text + " " +
                ProductBrandName.Text + " " + ProductDescribe.Text + " " + ProductPrice.Text + " " + ProductQuantity.Text);
                string response = Server.ReceiveResponse();

                if (response == null)
                {
                    Environment.Exit(0);
                }


                if (response == "Correct")
                {
                    LabelInfo.Visibility = Visibility.Visible;
                    LabelInfo.Content = "Dodano produkt!";
                    LabelInfo.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    LabelInfo.Visibility = Visibility.Visible;
                    LabelInfo.Content = "Błąd!";
                    LabelInfo.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
        }

    }
}
