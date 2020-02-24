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
    /// Logika interakcji dla klasy UserControlProductInCategory.xaml
    /// </summary>
    public partial class UserControlProductInCategory : UserControl
    {
        

        private int Id;
        private string Name;
        private string BrandName;
        private int Price;
        private string Category;

        public UserControlProductInCategory()
        {
            InitializeComponent();

            if(int.Parse(User.GetPermissions()) == 1)
            {
                BtnDeleteProduct.Visibility = Visibility.Visible;
                BtnSetPromotion.Visibility = Visibility.Visible;
            }

        }

        private void PrecentValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);

        }

        public void SetProductProperty(int id, string name, string brandName, int price,string category)
        {
            Id = id;
            Name = name;
            Price = price;
            BrandName = brandName;
            Category = category;
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

        private void BtnCheckDetails_Click(object sender, RoutedEventArgs e)
        {
            UserControlProduct ucObj = new UserControlProduct();

            IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

            window.GridHome.Children.Add(ucObj);
            ucObj.createDescribe(Id,Name,BrandName,Price);
        }

        public void DeleteProduct()
        {
        
            Server.SendString("product_delete " + Id.ToString());
            string response = Server.ReceiveResponse();

            if(response == "Correct")
            {
                UserControlProductsCategory ucObj = new UserControlProductsCategory(Category);

                IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
                MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

                window.GridHome.Children.Add(ucObj);
            }
            else
            {
                ErrorDeleteProduct.Content = "Błąd";
            }
        }
        
        public void SetPromotion()
        {
            Server.SendString("promotion " + Id.ToString() + " " + SetPercent.Text);
            string response = Server.ReceiveResponse();

            if(response == "Correct")
            {
                UserControlProductsCategory ucObj = new UserControlProductsCategory("Smartphone");

                IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
                MainWindow window = (MainWindow)HwndSource.FromHwnd(windowHandle).RootVisual;

                window.GridHome.Children.Add(ucObj);
            }
            else
            {
                ErrorDeleteProduct.Content = "Błąd";
            }
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            DeleteProduct();
        }

        private void BtnSetPromotion_Click(object sender, RoutedEventArgs e)
        {
            SetPercent.Visibility = Visibility.Visible;
            BtnAddPromotion.Visibility = Visibility.Visible;
        }

        private void BtnAddPromotion_Click(object sender, RoutedEventArgs e)
        {
            SetPromotion();
            
           
        }
    }
}
