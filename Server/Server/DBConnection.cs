using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class DBConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string port;
        private string CharSet;
        public DBConnection()
        {
            Initialize();
        }
        private void Initialize()
        {
            server = "188.137.79.230";
            database = "projekt";
            uid = "root";
            password = "root";
            port = "60001";
            CharSet = "utf8";
            string connectionString;
            connectionString = String.Format("server={0};port={1};uid={2}; password={3}; database={4}; charset={5}", server, port, uid, password, database, CharSet);

            connection = new MySqlConnection(connectionString);

        }
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Select statement
        public string Register(string login, string password, string email, string name, string surname, string street, string zip_code, string city, string home_number, string flat_number, string phone)
        {
            if (this.OpenConnection() == true)
            {
                string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                query = "SELECT login FROM klient WHERE login=@login;";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@login", login);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                int temp = 0;

                while (dataReader.Read())
                {
                    temp++;
                }
                if (temp > 0)
                {
                    this.connection.Close();
                    return "Użytkownik już istnieje.";
                }
                else
                {
                    try
                    {
                        dataReader.Dispose();

                        query = "SELECT id FROM adres WHERE ulica=@street and miejscowosc =@city and kod_pocztowy=@zip_code and numer_domu = @home_number and numer_lokalu=@flat_number;";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@street", street);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@zip_code", zip_code);
                        cmd.Parameters.AddWithValue("@home_number", home_number);
                        cmd.Parameters.AddWithValue("@flat_number", flat_number);
                        dataReader = cmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                            temp = dataReader.GetInt32("id");
                        }

                        if (temp == 0)
                        {
                            dataReader.Dispose();

                            query = "INSERT into adres (miejscowosc, kod_pocztowy, ulica, numer_domu, numer_lokalu) values (@city, @zip_code,@street,@home_number,@flat_number);";
                            cmd = new MySqlCommand(query, connection);
                            cmd.Parameters.AddWithValue("@street", street);
                            cmd.Parameters.AddWithValue("@city", city);
                            cmd.Parameters.AddWithValue("@zip_code", zip_code);
                            cmd.Parameters.AddWithValue("@home_number", home_number);
                            cmd.Parameters.AddWithValue("@flat_number", flat_number);
                            cmd.ExecuteNonQuery();
                            query = "SELECT id FROM adres WHERE ulica=@street and miejscowosc =@city and kod_pocztowy=@zip_code and numer_domu = @home_number and numer_lokalu=@flat_number;";
                            cmd = new MySqlCommand(query, connection);
                            cmd.Parameters.AddWithValue("@street", street);
                            cmd.Parameters.AddWithValue("@city", city);
                            cmd.Parameters.AddWithValue("@zip_code", zip_code);
                            cmd.Parameters.AddWithValue("@home_number", home_number);
                            cmd.Parameters.AddWithValue("@flat_number", flat_number);
                            dataReader = cmd.ExecuteReader();

                            while (dataReader.Read())
                            {
                                temp = dataReader.GetInt32("id");
                            }

                            dataReader.Dispose();
                            query = "INSERT into klient (login,haslo,imie,nazwisko,id_adres,email,telefon) values (@login,@password,@name,@surname,@temp,@email,@phone);";
                            cmd = new MySqlCommand(query, connection);
                            cmd.Parameters.AddWithValue("@login", login);
                            cmd.Parameters.AddWithValue("@password", password);
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@surname", surname);
                            cmd.Parameters.AddWithValue("@temp", temp);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@phone", phone);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            dataReader.Dispose();
                            query = "INSERT into klient (login,haslo,imie,nazwisko,id_adres,email,telefon) values (@login,@password,@name,@surname,@temp,@email,@phone);";
                            cmd = new MySqlCommand(query, connection);
                            cmd.Parameters.AddWithValue("@login", login);
                            cmd.Parameters.AddWithValue("@password", password);
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@surname", surname);
                            cmd.Parameters.AddWithValue("@temp", temp);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@phone", phone);
                            cmd.ExecuteNonQuery();
                        }
                        this.connection.Close();
                        return "Rejestracja zakończona pomyślnie.";
                    }
                    catch (Exception)
                    {
                        this.connection.Close();
                        return "Błąd zapytania.";
                    }
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
        public string Login(string login, string password)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    query = "SELECT id, uprawnienia FROM klient WHERE login=@login and haslo=@password;";

                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    int temp = 0;
                    string uprawnienia = "";
                    string id = "";
                    string response;

                    while (dataReader.Read())
                    {
                        id = dataReader.GetInt32("id").ToString();
                        uprawnienia = dataReader.GetInt32("uprawnienia").ToString();
                        temp++;
                    }
                    if (temp == 0)
                    {
                        this.connection.Close();
                        return "Niepoprawne dane logowania.";
                    }
                    else
                    {
                        response = "Correct," + id + "," + uprawnienia;
                        this.connection.Close();
                        return response;
                    }
                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
        public string Show_category(string category)
        {
            if (this.OpenConnection() == true)
            {
                try
                {

                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    query = "SELECT id, nazwa, producent, (cena - (cena * promocja / 100)) as cena FROM produkt WHERE kategoria=@category;";


                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@category", category);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    int temp = 0;
                    string nazwa, producent, id, cena, response;
                    response = "";

                    while (dataReader.Read())
                    {
                        id = dataReader.GetString("id");
                        nazwa = dataReader.GetString("nazwa");
                        producent = dataReader.GetString("producent");
                        cena = dataReader.GetFloat("cena").ToString();
                        response = response + id + "," + nazwa + "," + producent + "," + cena + ";";

                        temp++;
                    }
                    if (temp == 0)
                    {
                        this.connection.Close();
                        return "Brak";
                    }
                    else
                    {
                        this.connection.Close();
                        return response;
                    }
                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
        public string Show_product(string id)
        {
            if (this.OpenConnection() == true)
            {
                try
                {

                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    query = "SELECT opis, stan FROM produkt WHERE id=@id;";


                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    string opis, stan, response;
                    response = "";

                    while (dataReader.Read())
                    {
                        opis = dataReader.GetString("opis");
                        stan = dataReader.GetInt32("stan").ToString();
                        response = response + opis + "," + stan;

                    }
                    this.connection.Close();
                    return response;

                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
        public string Show_user(string id)
        {
            if (this.OpenConnection() == true)
            {
                try
                {

                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    query = "SELECT k.imie, k.nazwisko, k.login, k.telefon, k.email, k.id_adres, a.miejscowosc, a.kod_pocztowy, a.ulica, a.numer_domu, a.numer_lokalu  FROM klient k inner join adres a on k.id_adres =a.id WHERE k.id=@id;";


                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    string imie, nazwisko, login, telefon, email, response, miejscowosc, kod_pocztowy, numer_domu, numer_lokalu, ulica;
                    response = "";

                    while (dataReader.Read())
                    {
                        imie = dataReader.GetString("imie");
                        nazwisko = dataReader.GetString("nazwisko");
                        login = dataReader.GetString("login");
                        telefon = dataReader.GetString("telefon");
                        email = dataReader.GetString("email");
                        miejscowosc = dataReader.GetString("miejscowosc");
                        kod_pocztowy = dataReader.GetString("kod_pocztowy");
                        ulica = dataReader.GetString("ulica");
                        numer_domu = dataReader.GetString("numer_domu");
                        numer_lokalu = dataReader.GetString("numer_lokalu");
                        response = response + imie + "," + nazwisko + "," + login + "," + telefon + "," + email + "," + miejscowosc + "," + kod_pocztowy + "," + ulica + "," + numer_domu + "," + numer_lokalu;

                    }
                    this.connection.Close();
                    return response;

                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
        public string Cart_add(string id_produkt, string id_uzytkownika, string ilosc)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    int a, b = 0, tmp = 0, temp = 0;
                    a = Convert.ToInt32(ilosc);
                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = "SELECT id FROM koszyk where id_klient=@id_uzytkownika and id_produkt=@id_produkt;";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id_produkt", id_produkt);
                    cmd.Parameters.AddWithValue("@id_uzytkownika", id_uzytkownika);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        temp++;
                    }
                    dataReader.Dispose();
                    for (int i = 0; i < a; i++)
                    {
                        query = "INSERT into koszyk (id_produkt, id_klient) values (@id_produkt,@id_uzytkownika);";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@id_produkt", id_produkt);
                        cmd.Parameters.AddWithValue("@id_uzytkownika", id_uzytkownika);
                        cmd.ExecuteNonQuery();
                        b++;
                    }
                    query = "SELECT id FROM koszyk where id_klient=@id_uzytkownika and id_produkt=@id_produkt;";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id_produkt", id_produkt);
                    cmd.Parameters.AddWithValue("@id_uzytkownika", id_uzytkownika);
                    dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        tmp++;
                    }
                    if (temp + b == tmp)
                    {
                        int stan = 0;
                        dataReader.Dispose();
                        query = "SELECT stan FROM produkt where id=@id_produkt;";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@id_produkt", id_produkt);
                        dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            stan = dataReader.GetInt32("stan");
                        }
                        dataReader.Dispose();
                        stan -= b;
                        query = "UPDATE produkt SET stan=@stan where id=@id_produkt;";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@id_produkt", id_produkt);
                        cmd.Parameters.AddWithValue("@stan", stan);
                        cmd.ExecuteNonQuery();
                        this.connection.Close();
                        return "Correct";
                    }
                    else
                    {
                        this.connection.Close();
                        return "Error";
                    }
                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }




        }
        public string Cart_show(string id_uzytkownika)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    int temp = 0;
                    string id, id_produkt, nazwa, producent, cena, response = "";
                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = "SELECT k.id, k.id_produkt, p.nazwa, p.kategoria, p.producent, (p.cena - (p.cena * p.promocja / 100)) as cena FROM koszyk k inner join produkt p ON p.id=k.id_produkt where k.id_klient=@id_uzytkownika and k.id_zamowienie=0;";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id_uzytkownika", id_uzytkownika);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        id = dataReader.GetInt32("id").ToString();
                        id_produkt = dataReader.GetInt32("id_produkt").ToString();
                        nazwa = dataReader.GetString("nazwa");
                        producent = dataReader.GetString("producent");
                        cena = dataReader.GetFloat("cena").ToString();
                        response = response + id + "," + id_produkt + "," + nazwa + "," + producent + "," + cena + ";";

                        temp++;
                    }
                    if (temp == 0)
                    {
                        this.connection.Close();
                        return "Brak";
                    }
                    else
                    {
                        this.connection.Close();
                        return response;
                    }
                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
        public string Cart_delete(string id_koszyk)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    string id="";
                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = "SELECT p.id FROM koszyk k inner join produkt p on p.id = k.id_produkt where k.id IN (@id_koszyk);";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id_koszyk", id_koszyk);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        id = id + "," + dataReader.GetInt32("id").ToString();
                    }
                    dataReader.Dispose();

                    query = "UPDATE projekt.produkt p SET p.stan=p.stan+1 WHERE p.id IN (@id);";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    query = "DELETE FROM koszyk where id IN (@id_koszyk);";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id_koszyk", id_koszyk);
                    cmd.ExecuteNonQuery();
                    this.connection.Close();
                    return "Correct";

                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }

            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }



        }
        public string Alter_user(string login, string password, string email, string name, string surname, string street, string zip_code, string city, string home_number, string flat_number, string phone)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = "SELECT id FROM adres WHERE ulica=@street and miejscowosc =@city and kod_pocztowy=@zip_code and numer_domu = @home_number and numer_lokalu=@flat_number;";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@street", street);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@zip_code", zip_code);
                    cmd.Parameters.AddWithValue("@home_number", home_number);
                    cmd.Parameters.AddWithValue("@flat_number", flat_number);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    int temp = 0;

                    while (dataReader.Read())
                    {
                        temp = dataReader.GetInt32("id");
                    }
                    dataReader.Dispose();
                    if (temp == 0)
                    {

                        query = "INSERT into adres (miejscowosc, kod_pocztowy, ulica, numer_domu, numer_lokalu) values (@city, @zip_code,@street,@home_number,@flat_number);";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@street", street);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@zip_code", zip_code);
                        cmd.Parameters.AddWithValue("@home_number", home_number);
                        cmd.Parameters.AddWithValue("@flat_number", flat_number);
                        cmd.ExecuteNonQuery();
                        query = "SELECT id FROM adres WHERE ulica=@street and miejscowosc =@city and kod_pocztowy=@zip_code and numer_domu = @home_number and numer_lokalu=@flat_number;";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@street", street);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@zip_code", zip_code);
                        cmd.Parameters.AddWithValue("@home_number", home_number);
                        cmd.Parameters.AddWithValue("@flat_number", flat_number);
                        dataReader = cmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                            temp = dataReader.GetInt32("id");
                        }
                        dataReader.Dispose();
                        query = "UPDATE klient set haslo=@password, imie=@name, nazwisko=@surname, email=@email, telefon=@phone, id_adres=@temp WHERE login=@login;";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@surname", surname);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@temp", temp);
                        cmd.ExecuteNonQuery();
                        this.connection.Close();
                        return "Correct";

                    }
                    else
                    {
                        query = "UPDATE klient set haslo=@password, imie=@name, nazwisko=@surname, email=@email, telefon=@phone, id_adres=@temp WHERE login=@login;";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@surname", surname);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@temp", temp);
                        cmd.ExecuteNonQuery();
                        this.connection.Close();
                        return "Correct";
                    }
                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
        public string Product_add(string nazwa, string kategoria, string producent, string opis, string cena, string stan)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    double a;
                    int b;
                    a = Convert.ToDouble(cena);
                    b = Convert.ToInt32(stan);
                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = "INSERT into produkt (nazwa, kategoria, producent, opis, cena, stan) values (@nazwa, @kategoria,@producent,@opis,@cena,@stan);";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@nazwa", nazwa);
                    cmd.Parameters.AddWithValue("@kategoria", kategoria);
                    cmd.Parameters.AddWithValue("@producent", producent);
                    cmd.Parameters.AddWithValue("@opis", opis);
                    cmd.Parameters.AddWithValue("@cena", cena);
                    cmd.Parameters.AddWithValue("@stan", stan);
                    cmd.ExecuteNonQuery();
                    this.connection.Close();
                    return "Correct";
                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }

        }
        public string Order_add(string id_klient, string id_koszyk, string forma_platnosci, string zaplacone, string wartosc)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    int a, b, z;
                    b = 0;
                    double c = 0;
                    a = Convert.ToInt32(id_klient);
                    z = Convert.ToInt32(zaplacone);
                    c = Convert.ToDouble(wartosc);
                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = "INSERT INTO zamowienie (id_klient, zaplacono, wartosc_netto, forma_platnosci) VALUES (@a, @z,@c, @forma_platnosci);";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@a", a);
                    cmd.Parameters.AddWithValue("@z", z);
                    cmd.Parameters.AddWithValue("@c", c);
                    cmd.Parameters.AddWithValue("@forma_platnosci", forma_platnosci);
                    cmd.ExecuteNonQuery();
                    query = "SELECT LAST_INSERT_ID() AS id;";
                    cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        b = dataReader.GetInt32("id");
                    }
                    dataReader.Dispose();
                    string[] tmp = id_koszyk.Split(',');
                    foreach (string id in tmp)
                    {
                        query = "UPDATE koszyk k SET k.id_zamowienie = @b WHERE k.id=@id_koszyk;";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@b", b);
                        cmd.Parameters.AddWithValue("@id_koszyk", id);
                        cmd.ExecuteNonQuery();
                    }
                    this.connection.Close();
                    return "Correct";
                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
        public string Order_show()
        {
            if (this.OpenConnection() == true)
            {
                try
                {

                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    query = "SELECT id, id_klient, data_zlozenia, status, zaplacono, wartosc_netto, forma_platnosci, numer_przesylki, data_zrealizowania from zamowienie;";


                    cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    string id, id_klient, data_zlozenia, status, zaplacono, wartosc_netto, numer_przesylki, forma_platnosci, data_zrealizowania, response;

                    response = "";

                    while (dataReader.Read())
                    {
                        id = dataReader.GetInt32("id").ToString();
                        id_klient = dataReader.GetInt32("id_klient").ToString();
                        data_zlozenia = dataReader.GetDateTime("data_zlozenia").ToString();
                        status = dataReader.GetInt32("status").ToString();
                        zaplacono = dataReader.GetInt32("zaplacono").ToString();
                        wartosc_netto = dataReader.GetDouble("wartosc_netto").ToString();
                        forma_platnosci = (dataReader.IsDBNull(6) ? "null" : dataReader.GetString("forma_platnosci"));
                        numer_przesylki = (dataReader.IsDBNull(7) ? "null" : dataReader.GetString("numer_przesylki"));
                        data_zrealizowania = (dataReader.IsDBNull(8) ? "null" : dataReader.GetDateTime("data_zrealizowania").ToString());
                        response = response + id + "," + id_klient + "," + data_zlozenia + "," + status + "," + zaplacono + "," + wartosc_netto + "," + forma_platnosci + "," + numer_przesylki + "," + data_zrealizowania + ";";

                    }
                    this.connection.Close();
                    return response;

                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
        public string Alter_order(string id, string status, string numer_przesylki)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = "UPDATE zamowienie SET status=@status, numer_przesylki=@numer_przesylki, data_zrealizowania=current_timestamp where id=@id;";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@numer_przesylki", numer_przesylki);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    this.connection.Close();
                    return "Correct";

                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
        public string Product_delete(string id)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = "DELETE FROM produkt where id=@id;";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    this.connection.Close();
                    return "Correct";

                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }

            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }

        }
        public string Product_search(string tmp)
        {
            if (this.OpenConnection() == true)
            {
                try
                {

                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    query = "SELECT  id, nazwa, producent, cena FROM produkt WHERE nazwa='%@nazwa%';";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@nazwa", tmp);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    string id, nazwa, producent, cena, response;
                    response = "";

                    while (dataReader.Read())
                    {
                        id = dataReader.GetString("id");
                        nazwa = dataReader.GetString("nazwa");
                        producent = dataReader.GetString("producent");
                        cena = dataReader.GetFloat("cena").ToString();
                        response = response + id+","+ nazwa + "," + producent + "," + cena + ";";


                    }
                    this.connection.Close();
                    return response;

                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        
    }
        public string Promotion(string id, string procent)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    string query = "SET NAMES 'utf8' COLLATE 'utf8_unicode_ci'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = "UPDATE produkt SET promocja=@procent where id=@id;";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@procent", procent);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    this.connection.Close();
                    return "Correct";

                }
                catch (Exception)
                {
                    this.connection.Close();
                    return "Błąd zapytania.";
                }
            }
            else
            {
                return "Błąd połączenia po stronie bazy danych.";
            }
        }
    }
}

