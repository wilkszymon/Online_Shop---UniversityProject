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
    /// Logika interakcji dla klasy UserControlOrders.xaml
    /// </summary>
    public partial class UserControlOrders : UserControl
    {
        List<UserControlOrderList> items = new List<UserControlOrderList>();


        public UserControlOrders()
        {
            InitializeComponent();
                       
            Server.SendString("order_show");
            string response = Server.ReceiveResponse();
            int NumberOfOrders = 0;

            string[] orders = response.Split(';');

            foreach (string product in orders)
            {
                try
                {
                    string[] param = product.Split(',');
                    string id = param[0];
                    string idCustomer = param[1];
                    string DateOfOrder = param[2];
                    string Status = param[3];
                    string WasPaid = param[4];
                    string Value = param[5];
                    string PaymentMethod = param[6];
                    string TrackingNumber = param[7];
                    string DateOfImplementation = param[8];

                    AddOrderToList(id, idCustomer, DateOfOrder, Status, WasPaid, Value, PaymentMethod, TrackingNumber, DateOfImplementation);
                    NumberOfOrders++;
                }
                catch (Exception)
                {

                }
            }

            OrdersText.Content = "Zamówienia(" + NumberOfOrders + ")";

            if(NumberOfOrders == 0)
            {
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Text = "Brak zamowień!" + response;
            }
        }

        public void AddOrderToList(string id, string customerId, string dateOfOrder, string status, string wasPaid, string value,
            string paymentMethod, string trackingNumber, string dateOfImplementation)
        {
            UserControlOrderList prod = new UserControlOrderList();
            prod.SetDataOrder(id, customerId, dateOfOrder, status, wasPaid, value, paymentMethod, trackingNumber, dateOfImplementation); ;
            items.Add(prod);
            OrdersList.ItemsSource = items;
        }

    }
}
