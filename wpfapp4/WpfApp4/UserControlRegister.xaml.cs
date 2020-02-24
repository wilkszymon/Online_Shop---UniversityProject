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
    /// Logika interakcji dla klasy UserControlRegister.xaml
    /// </summary>
    public partial class UserControlRegister : UserControl
    {
        public UserControlRegister()
        {
            InitializeComponent();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            Register();
        }


        private void clickEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Register();
            }
        }

        private void UsernamePasswordValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);

        }

        private void NameValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-ZąćęłńóśżźŁŻ]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-ZąćęłńóśżźŁŚĆŻŹ]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void ZipCodeValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9-]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void HouseAndApartmentNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void PhoneNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9+]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void EmailValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9a-zA-Z@.]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void Register()
        {
            if (Name.Text == "" || Surname.Text == "" || Username.Text == "" || Password.Password == "" || City.Text == "" || Street.Text == "" || PhoneNumber.Text == "" || ZipCode.Text == "" || Email.Text == "" || HouseNumber.Text == "" || ApartmentNumber.Text == "")
            {
                LabelRequired.Content = "Proszę wypełnić pola oznaczone *";
                if (Username.Text.Length < 5 || Password.Password.Length < 5)
                {
                    lblCheckUsernamePassword.Content = "Nazwa użytkownika i hasło musi zawierać co najmniej 5 znaków.";
                }
                return;
            }

            Server.SendString("register " + Username.Text + " " + Password.Password + " " + Email.Text + " " + Name.Text + " " + Surname.Text + " " + Street.Text + " " + ZipCode.Text + " " + City.Text + " " + "Test" + " " + "1" + " " + PhoneNumber.Text);
            LabelRequired.Content = Server.ReceiveResponse();
        }

    }
}
