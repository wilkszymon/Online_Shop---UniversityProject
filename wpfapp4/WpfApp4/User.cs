using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
   
    public struct Adress
    {
        public string City;
        public string Street;
        public string ZipCode;
        public string HouseNumber;
        public string ApartmentNumber;
    }

    // Odpowiednik Użytkownik z DK
    class User
    {
        private static int Id = -1;
        private static string Name;
        private static string Password;
        private static string Surname;
        private static string Username;
        private static string Email;
        private static string PhoneNumber;
        private static string Permissions;
        private static Adress adress;

        public static void SetData(int id,string name,string surname,string password,string username,string email,string phonenumber,string permissions, Adress _adress)
        {
            Id = id;
            Name = name;
            Password = password;
            Surname = surname;
            Username = username;
            Email = email;
            PhoneNumber = phonenumber;
            Permissions = permissions;
            adress = _adress;
        }

        public static void SetAdress(string city,string street, string zipCode, string houseNumber, string apartmentNumber)
        {
            adress.City = city;
            adress.Street = street;
            adress.ZipCode = zipCode;
            adress.HouseNumber = houseNumber;
            adress.ApartmentNumber = apartmentNumber;
        }

        public static void SetPassword(string password)
        {
            Password = password;
        }

        public static void Logout()
        {
            Id = -1;
            Name = "";
            Password = "";
            Surname = "";
            Username = "";
            Email = "";
            PhoneNumber = "";
            Permissions = "";
            adress.ApartmentNumber = "";
            adress.City = "";
            adress.HouseNumber = "";
            adress.Street = "";
            adress.ZipCode = "";
        }

        public static int GetID()
        {
            return Id;
        }

        public static string GetName()
        {
            return Name;
        }

        public static string GetPassword()
        {
            return Password;
        }

        public static string GetSurname()
        {
            return Surname;
        }

        public static string GetUsername()
        {
            return Username;
        }

        public static string GetEmail()
        {
            return Email;
        }

        public static string GetPhoneNumber()
        {
            return PhoneNumber;
        }

        public static string GetPermissions()
        {
            return Permissions;
        }

        public static Adress GetAdress()
        {
            return adress;
        }

    }
}
