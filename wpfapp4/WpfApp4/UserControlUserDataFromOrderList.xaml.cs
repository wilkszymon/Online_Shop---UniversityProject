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
    /// Logika interakcji dla klasy UserControlUserDataFromOrderList.xaml
    /// </summary>
    public partial class UserControlUserDataFromOrderList : UserControl
    {
        public UserControlUserDataFromOrderList()
        {
            InitializeComponent();
        }

        public void SetCustomerData(int id)
        {

            Server.SendString("show_user " + id.ToString());
            string response = Server.ReceiveResponse();
            if (response == null)
            {
                Environment.Exit(0);
            }

            int end = 0;

            int Firstname_end = response.IndexOf(",", end);
            string FirstName_ = response.Substring(end, Firstname_end - end);

            int Surname_end = response.IndexOf(",", Firstname_end + 1);
            string Surname_ = response.Substring(Firstname_end + 1, Surname_end - Firstname_end - 1);

            int Username_end = response.IndexOf(",", Surname_end + 1);
            string Login_ = response.Substring(Surname_end + 1, Username_end - Surname_end - 1);

            int Phone_end = response.IndexOf(",", Username_end + 1);
            string Phone_ = response.Substring(Username_end + 1, Phone_end - Username_end - 1);

            int Email_end = response.IndexOf(",", Phone_end + 1);
            string Email_ = response.Substring(Phone_end + 1, Email_end - Phone_end - 1);

            int City_end = response.IndexOf(",", Email_end + 1);
            string City_ = response.Substring(Email_end + 1, City_end - Email_end - 1);

            int ZipCode_end = response.IndexOf(",", City_end + 1);
            string ZipCode_ = response.Substring(City_end + 1, ZipCode_end - City_end - 1);

            int Street_end = response.IndexOf(",", ZipCode_end + 1);
            string Street_ = response.Substring(ZipCode_end + 1, Street_end - ZipCode_end - 1);

            int HouseNumber_end = response.IndexOf(",", Street_end + 1);
            string HouseNumber_ = response.Substring(Street_end + 1, HouseNumber_end - Street_end - 1);

            string ApartmentNumber_ = response.Substring(HouseNumber_end + 1, response.Length - HouseNumber_end - 1);



            Name.Content = FirstName_;
            Surname.Content = Surname_;
            Username.Content = Login_;
            Email.Content = Email_;
            PhoneNumber.Content = Phone_;
            City.Content = City_;
            Street.Content = Street_;
            ZipCode.Content = ZipCode_;
            HouseNumber.Content = HouseNumber_;
            ApartmentNumber.Content = ApartmentNumber_;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

            UserControlOrders ucObj = new UserControlOrders();
            window.GridHome.Children.Add(ucObj);
        }
    }
}
