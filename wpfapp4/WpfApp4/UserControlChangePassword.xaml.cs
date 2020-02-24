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
    /// Logika interakcji dla klasy UserControlChangePassword.xaml
    /// </summary>
    public partial class UserControlChangePassword : UserControl
    {
        public UserControlChangePassword()
        {
            InitializeComponent();
        }

        private void PolishSignValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[ąćęłńóśżź]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ButtonChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if(Password.Password != User.GetPassword())
            {
                LabelRequired.Content = "Błędne stare hasło!";
            }
            else
            {
                ChangePassword();   
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ChangePassword();
            }
        }

        private void ChangePassword()
        {
            //alter_user
            Server.SendString("alter_user szymon2112g nowe email imie nazwisko ulica kod miejscowosc nrdom nrlok tel");/*+ User.GetUsername() + " " + NewPassword.Password + " " + User.GetEmail() +
                User.GetName() + " " + User.GetSurname() + " " + User.GetAdress().Street + " " + User.GetAdress().ZipCode +
                " " + User.GetAdress().City + " " + User.GetAdress().HouseNumber + " " + User.GetAdress().ApartmentNumber +
                " " + User.GetPhoneNumber());*/

            string response = Server.ReceiveResponse();

            if (response == null)
            {
                Environment.Exit(0);
            }
            else if (response == "Correct")
            {
                LabelRequired.Content = "Zmieniono hasło!";
                LabelRequired.Foreground = new SolidColorBrush(Colors.Green);
                User.SetPassword(NewPassword.Password);
            }
            else
            {
                LabelRequired.Content = response;// "Błąd połączenia!";
            }
        }
    }
}
