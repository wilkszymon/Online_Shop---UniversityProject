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
    /// Logika interakcji dla klasy UserControlAddressCorres.xaml
    /// </summary>
    public partial class UserControlAddressCorres : UserControl
    {
        public UserControlAddressCorres()
        {
            InitializeComponent();
            CityCoress.Text = User.GetAdress().City;
            StreetCoress.Text = User.GetAdress().Street;
            ApartmentNumberCoress.Text = User.GetAdress().ApartmentNumber;
            HouseNumberCoress.Text = User.GetAdress().HouseNumber;
            ZIPCodeCoress.Text = User.GetAdress().ZipCode;
        }

        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-ZąćęłńóśżźŁŚĆŻŹ]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void HouseAndApartmentNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void ZipCodeValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9-]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        
        private void ChangeAddressCorres_Click(object sender, RoutedEventArgs e)
        {
            ChangeAdress();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ChangeAdress();
            }
        }

        private void ChangeAdress()
        {
            if (CityCoress.Text == "" || StreetCoress.Text == "" || HouseNumberCoress.Text == "" || ApartmentNumberCoress.Text == "" || ZIPCodeCoress.Text == "")
            {
                LabelRequired.Content = "Proszę wypełnić pola oznaczone *";
                return;
            }
            else
            {
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
                    User.SetAdress(CityCoress.Text, StreetCoress.Text, ZIPCodeCoress.Text, HouseNumberCoress.Text, ApartmentNumberCoress.Text);
                }
                else
                {
                    LabelRequired.Content = response;// "Błąd połączenia!";
                }
            }
        }


    }
}
