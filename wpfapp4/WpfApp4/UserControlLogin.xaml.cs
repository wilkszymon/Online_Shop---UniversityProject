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
    /// Logika interakcji dla klasy UserControlLogin.xaml
    /// </summary>
    public partial class UserControlLogin : UserControl
    {
        public UserControlLogin()
        {
            InitializeComponent();
        }


        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();            
        }

        private void clickEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void UsernamePasswordValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);

        }

        private void Login()
        {
            if (Username.Text == "" || Password.Password == "")
            {
                LabelLogin.Content = "Proszę uzupełnić wszystkie pola";
            }
            else
            {
                Server.SendString("login " + Username.Text + " " + Password.Password);
                string response = Server.ReceiveResponse();

                int end = 0;
                string Correct = "";
                int Id = -1, Role = -1;

                try
                {
                    int correct_end = response.IndexOf(",", end);
                    Correct = response.Substring(end, correct_end - end);
                    int id_end = response.IndexOf(",", correct_end + 1);
                    string IdString = response.Substring(correct_end + 1, id_end - correct_end - 1);
                    string StringRole = response.Substring(id_end + 1, response.Length - id_end - 1);

                    Id = int.Parse(IdString);
                    Role = int.Parse(StringRole);
                }
                catch(Exception ex)
                {
                    LabelLogin.Content = "Błędny login lub hasło";
                }


                if (Correct == "Correct")
                {
                    //Zalogowano poprawnie
                    IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
                    MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

                    window.ButtonAccount.Visibility = Visibility.Visible;
                    window.GridShoppingCart.Visibility = Visibility.Visible;
                    window.UserNameTextBox.Text = Username.Text;
                    window.UserNameTextBox.Visibility = Visibility.Visible;
                    window.ButtonLogout.Visibility = Visibility.Visible;
                    window.ButtonLogin.Visibility = Visibility.Hidden;
                    window.ButtonRegister.Visibility = Visibility.Hidden;
                    LabelLogin.Content = "Zalogowano jako " + Username.Text;

                    window.ButtonLogout.Click += ButtonLogout_Click;

                    if(Role == 1)
                    {
                        window.ButtonAddProduct.Visibility = Visibility.Visible;
                        window.ButtonOrders.Visibility = Visibility.Visible;
                    }

                    SetUserData(Id, Role);
                }
                else
                {
                    LabelLogin.Content = "Błędny login lub hasło";
                }

            }
        }
        private void SetUserData(int id,int role)
        {
            Server.SendString("show_user " + id.ToString());
            string response = Server.ReceiveResponse();
            if (response == null)
            {
                Environment.Exit(0);
            }

            int end = 0;

            int Firstname_end = response.IndexOf(",", end);
            string FirstName = response.Substring(end, Firstname_end - end);

            int Surname_end = response.IndexOf(",", Firstname_end + 1);
            string Surname = response.Substring(Firstname_end + 1, Surname_end - Firstname_end - 1);

            int Username_end = response.IndexOf(",", Surname_end + 1);
            string Login = response.Substring(Surname_end + 1, Username_end - Surname_end - 1);

            int Phone_end = response.IndexOf(",", Username_end + 1);
            string Phone = response.Substring(Username_end + 1, Phone_end - Username_end - 1);

            int Email_end = response.IndexOf(",", Phone_end + 1);
            string Email = response.Substring(Phone_end + 1, Email_end - Phone_end - 1);

            int City_end = response.IndexOf(",", Email_end + 1);
            string City = response.Substring(Email_end + 1, City_end - Email_end - 1);

            int ZipCode_end = response.IndexOf(",", City_end + 1);
            string ZipCode = response.Substring(City_end + 1, ZipCode_end - City_end - 1);

            int Street_end = response.IndexOf(",", ZipCode_end + 1);
            string Street = response.Substring(ZipCode_end + 1, Street_end - ZipCode_end - 1);

            int HouseNumber_end = response.IndexOf(",", Street_end + 1);
            string HouseNumber = response.Substring(Street_end + 1, HouseNumber_end - Street_end - 1);

            string ApartmentNumber = response.Substring(HouseNumber_end + 1, response.Length - HouseNumber_end - 1);



            Adress adress = new Adress();
            adress.ZipCode = ZipCode;
            adress.Street = Street;
            adress.HouseNumber = HouseNumber;
            adress.City = City;
            adress.ApartmentNumber = ApartmentNumber;

            User.SetData(id, FirstName, Surname, Password.Password, Login, Email, Phone, role.ToString(), adress);
        }


        private void LogOut()
        {
            UserControlHome ucObj = new UserControlHome();

            IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

            window.GridHome.Children.Add(ucObj);

            window.ButtonAccount.Visibility = Visibility.Hidden;
            window.GridShoppingCart.Visibility = Visibility.Hidden;
            window.UserNameTextBox.Visibility = Visibility.Hidden;
            window.ButtonLogout.Visibility = Visibility.Hidden;
            window.ButtonLogin.Visibility = Visibility.Visible;
            window.ButtonRegister.Visibility = Visibility.Visible;

        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            LogOut();
        }

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
