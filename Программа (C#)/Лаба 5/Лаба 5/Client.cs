using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Лаба_5
{
    class Client
    {
        // конструктор поумолчанию - создает список клиентов
        public Client()
        {
            all_Client = new ArrayList();
        }
        // конструктор с параметрами - клиент с логином и паролем
        public Client(string login, int password, int start_summ)
        {
            this.login = login;
            this.password = password;
            sav_acc = new Savings_Account(start_summ);
            tim_acc = new Timed_Maturity_Account(start_summ);
            chec_acc = new Checking_Account(start_summ);
            over_acc = new Overdraft_Account(start_summ);
        }

        // 4-е вида счета
        public Savings_Account sav_acc;
        public Timed_Maturity_Account tim_acc;
        public Checking_Account chec_acc;
        public Overdraft_Account over_acc;

        // список всех клиентов
        private ArrayList all_Client;
        // добавление клиента
        public void add_Client(Client clien)
        {
            all_Client.Add(clien);
        }
        // выбор одного клиента
        public Client get_Client_From_List(string login_of_client, int password_of_client)
        {
            Client returnd_value = new Client("", 0, 0);
            foreach (Client cl in get_All_CLient())
            {
                if ((cl.get_Login() == login_of_client) && (cl.get_Password() == password_of_client))
                {
                    returnd_value = cl;
                }
            }
            return returnd_value;
        }
        // список всех клиентов
        public ArrayList get_All_CLient()
        {
            return all_Client;
        }
        // логин
        private string login; 
        public void set_Login(string login)
        {
            this.login = login;
        }
        public string get_Login()
        {
            return login;
        }
        // пароль
        private int password; 
        public void set_Password(int password)
        {
            this.password = password;
        }
        public int get_Password()
        {
            return password;
        }
        // помощь для ввода пароля
        private string helpForPassword; 
        public void set_HelpForPassword(string helpForPassword)
        {
            this.helpForPassword = helpForPassword;
        }
        public string get_HelpForPassword()
        {
            return helpForPassword;
        }
        public bool Login_password_check(string login, int password)
        {
            bool use_login = false;
            bool passowrd_accepted = false;
            foreach (Client client in all_Client)
            {
                if (client.get_Login() == login)
                {
                    use_login = true;
                    if (client.get_Password() == password)
                    {
                        Console.Clear();
                        Console.Write("Пароль принят.\n Можете приступить к работе со счетами.");
                        Console.ReadKey();
                        passowrd_accepted = true;
                    }
                    else
                    {
                        Console.Write("Не верный пароль. \nПовторите.");
                    }
                }
            }
            if (!use_login)
            {
                Console.Write("Такого логина не найдено в базе. \nПовторите.");
            }
            return passowrd_accepted;
        }
    }
}
