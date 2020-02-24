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
    /// Logika interakcji dla klasy UserControlProductInCart.xaml
    /// </summary>
    public partial class UserControlProductInCart : UserControl
    {
        private int Id;
        private int ProductId;
        private string Name;
        private string BrandName;
        private int Price;

        public UserControlProductInCart()
        {
            InitializeComponent();
        }

        public void SetProductPropertyInCart(int id, int productId, string name, string brandName, int price)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Price = price;
            BrandName = brandName;
            ProductName.Text = Name;
            ProductBrand.Text = BrandName;
            ProductPrice.Text = Price.ToString() + "zł";
        }

        public int getId()
        {
            return Id;
        }

        public string getName()
        {
            return Name;
        }

        public int getPrice()
        {
            return Price;
        }

        public void DeleteFromCart()
        {
            Server.SendString("cart_delete " + Id.ToString());
            string response = Server.ReceiveResponse();

            if (response == "Correct")
            {
                IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
                MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

                UserControlCart ucObj = new UserControlCart();
                window.GridHome.Children.Add(ucObj);
            }
            else
            {
                ErrorDelete.Content = "Błąd " + response + " " + Id.ToString();
            }
        }

        private void BtnDeleteFromCart_Click(object sender, RoutedEventArgs e)
        {
            DeleteFromCart();
        }
    }
}
