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
    /// Logika interakcji dla klasy UserControlUserData.xaml
    /// </summary>
    public partial class UserControlUserData : UserControl
    {
        public UserControlUserData()
        {
            InitializeComponent();

            Name.Content = User.GetName();
            Surname.Content = User.GetSurname();
            Username.Content = User.GetUsername();
            Email.Content = User.GetEmail();
            PhoneNumber.Content = User.GetPhoneNumber();
            City.Content = User.GetAdress().City;
            Street.Content = User.GetAdress().Street;
            ZipCode.Content = User.GetAdress().ZipCode;
            HouseNumber.Content = User.GetAdress().HouseNumber;
            ApartmentNumber.Content = User.GetAdress().ApartmentNumber;
        }

        private void Click_ButtonChangeData(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonChangePassword_Click(object sender, RoutedEventArgs e)
        {
            UserControlChangePassword ucObj = new UserControlChangePassword();

            GridLogin.Children.Add(ucObj);


        }

        private void ButtonAddressCorres_Click(object sender, RoutedEventArgs e)
        {
            UserControlAddressCorres ucObj = new UserControlAddressCorres();
            GridLogin.Children.Add(ucObj);
        }
    }
}
