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
using System.Text.RegularExpressions;

namespace WpfApp4
{
    /// <summary>
    /// Logika interakcji dla klasy UserControlOrderList.xaml
    /// </summary>
    public partial class UserControlOrderList : UserControl
    {
        public UserControlOrderList()
        {
            InitializeComponent();
        }

        private void StatusValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-1]*$");
            e.Handled = !regex.IsMatch(e.Text);

        }

        private string CustomerId;
        private string Id;
        private string Status_;
        private string TrackingNumber_;
        public void SetDataOrder(string id,  string customerId, string dateOfOrder, string status, string wasPaid, string value,
            string paymentMethod, string trackingNumber, string dateOfImplementation)
        {
            Id = id;
            CustomerId = customerId;
            DateOfOrder.Content = dateOfOrder;
            Status.Text = status;
            Status_ = status;
            WasPaid.Content = wasPaid;
            Value.Content = value + "zł";
            PaymentMethod.Content = paymentMethod;
            TrackingNumber.Text = trackingNumber;
            TrackingNumber_ = trackingNumber;
            DateOfImplementation.Content = dateOfImplementation;
        }

        private void ButtonCustomer_Click(object sender, RoutedEventArgs e)
        {
            IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

            UserControlUserDataFromOrderList ucObj = new UserControlUserDataFromOrderList();
            ucObj.SetCustomerData(int.Parse(CustomerId));
            window.GridHome.Children.Add(ucObj);
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            Server.SendString("alter_order " + Id + " " + Status.Text + " " + TrackingNumber.Text);
            string response = Server.ReceiveResponse();

            if(response == "Correct")
            {
                if(Status_ != Status.Text)
                {
                    Status.BorderBrush = new SolidColorBrush(Colors.Green);
                }

                if (TrackingNumber_ != TrackingNumber.Text)
                {
                    TrackingNumber.BorderBrush = new SolidColorBrush(Colors.Green);
                }
                ButtonChange.Content = "Zmieniono";
            } 
        }
    }
}
