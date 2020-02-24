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
    /// <summary>
    /// Logika interakcji dla klasy UserControlOrder.xaml
    /// </summary>
    public partial class UserControlOrder : UserControl
    {
        private int Price;
        private string CartId;
        public UserControlOrder()
        {
            InitializeComponent();

            Name.Content = User.GetName();
            Surname.Content = User.GetSurname();
            PhoneNumber.Content = User.GetPhoneNumber();
            City.Content = User.GetAdress().City;
            Street.Content = User.GetAdress().Street;
            ZipCode.Content = User.GetAdress().ZipCode;
            HouseNumber.Content = User.GetAdress().HouseNumber;
            ApartmentNumber.Content = User.GetAdress().ApartmentNumber;
        }

        public void SetData(int price,string cartId)
        {
            Price = price;
            OrderPrice.Content = Price.ToString() + "zł";
            CartId = cartId;
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            int IsPay = 0;

            if(ListBoxMethodOfPayment.Content.ToString() == "Karta")
            {
                IsPay = 1;
            }

            Server.SendString("order_add " + User.GetID().ToString() + " " + CartId + " " +
                ListBoxMethodOfPayment.Content.ToString() + " " + IsPay.ToString() + " " + Price);

            string response = Server.ReceiveResponse();

            if(response == "Correct")
            {
                MessageResponse.Visibility = Visibility.Visible;
                MessageResponse.Content = "Zamówiono pomyślnie!";
                MessageResponse.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                MessageResponse.Visibility = Visibility.Visible;
                MessageResponse.Content = "Błąd = " + response;
                MessageResponse.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
