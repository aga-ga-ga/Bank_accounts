using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба_5
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Client list_of_client = new Client();
            // клиент Alexzander с паролем 1234 иммеот на балансе 1 милион
            list_of_client.add_Client(new Client("Alexzander",1234,1000000));
            bool finish = false;
            while (finish != true)
            {
                Console.Clear();
                Console.Write("Введите логин: ");
                String client_login = Console.ReadLine();
                Console.Write("Введите пароль: ");
                int client_password;
                try
                {
                    client_password = Convert.ToInt32(Console.ReadLine());
                    if (list_of_client.Login_password_check(client_login, client_password))
                    {
                        while(finish != true)
                        {
                            int my_chose0 = -1, my_chose1 = -1, my_chose2 = -1, summ = 0;
                            Client chouse_client = list_of_client.get_Client_From_List(client_login, client_password);
                            Console.WriteLine(" Выберите счет для работы: \n1)Savings Account \n2)Timed Maturity Account \n3)Checking Account \n4)Overdraft_Account \n 0)Выход из системы");
                            my_chose1 = Convert.ToInt32(Console.ReadLine());
                            switch (my_chose1)
                            {
                                case 0:
                                    finish = true;
                                    break;
                                case 1:
                                    Console.Clear();
                                    Console.WriteLine("Работа Производится со счетом Savings Account");
                                    Console.WriteLine(" Выберите операцию: \n1)Вложить фонд \n2)Снять фонд \n3)Проверить баланс \n 0)Выход из системы");
                                    my_chose2 = Convert.ToInt32(Console.ReadLine());
                                    switch (my_chose2)
                                    {
                                        case 0:
                                            my_chose2 = -1;
                                            finish = true;
                                            break;
                                        case 1:
                                            Console.WriteLine("Введите сумму: ");
                                            summ = Convert.ToInt32(Console.ReadLine());
                                            chouse_client.sav_acc.put_Fund(summ);
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        case 2:
                                            Console.WriteLine("Введите сумму: ");
                                            summ = Convert.ToInt32(Console.ReadLine());
                                            chouse_client.sav_acc.take_Fund(summ); 
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        case 3:
                                            Console.WriteLine("Баланс составляет" + Convert.ToString(chouse_client.sav_acc.get_Balance()));
                                            my_chose2 = -1;
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                        default:
                                            my_chose2 = -1;
                                            break;
                                    }
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("Работа Производится со счетом Timed Maturity Account");
                                    Console.WriteLine(" Выберите операцию: \n1)Вложить фонд \n2)Снять фонд \n3)Проверить баланс \n 0)Выход из системы");
                                    my_chose2 = Convert.ToInt32(Console.ReadLine());
                                    switch (my_chose2)
                                    {
                                        case 0:
                                            my_chose2 = -1;
                                            finish = true;
                                            break;
                                        case 1:
                                            Console.WriteLine("Введите сумму: ");
                                            summ = Convert.ToInt32(Console.ReadLine());
                                            chouse_client.tim_acc.put_Fund(summ);
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        case 2:
                                            Console.WriteLine("Введите сумму: ");
                                            summ = Convert.ToInt32(Console.ReadLine());
                                            chouse_client.tim_acc.take_Fund(summ);
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        case 3:
                                            Console.WriteLine("Баланс составляет" + Convert.ToString(chouse_client.tim_acc.get_Balance()));
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        default:
                                            my_chose2 = -1;
                                            break;
                                    }
                                    break;
                                case 3:
                                    Console.Clear();
                                    Console.WriteLine("Работа Производится со счетом Checking Account");
                                    Console.WriteLine(" Выберите операцию: \n1)Вложить фонд \n2)Снять фонд \n3)Проверить баланс \n 0)Выход из системы");
                                    my_chose2 = Convert.ToInt32(Console.ReadLine());
                                    switch (my_chose2)
                                    {
                                        case 0:
                                            my_chose2 = -1;
                                            finish = true;
                                            break;
                                        case 1:
                                            Console.WriteLine("Введите сумму: ");
                                            summ = Convert.ToInt32(Console.ReadLine());
                                            chouse_client.chec_acc.put_Fund(summ);
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        case 2:
                                            Console.WriteLine("Введите сумму: ");
                                            summ = Convert.ToInt32(Console.ReadLine());
                                            chouse_client.chec_acc.take_Fund(summ);
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        case 3:
                                            Console.WriteLine("Баланс составляет" + Convert.ToString(chouse_client.chec_acc.get_Balance()));
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        default:
                                            my_chose2 = -1;
                                            break;
                                    }
                                    break;
                                case 4:
                                    Console.Clear();
                                    Console.WriteLine("Работа Производится со счетом Overdraft_Account");
                                    Console.WriteLine(" Выберите операцию: \n1)Вложить фонд \n2)Снять фонд \n3)Проверить баланс \n 0)Выход из системы");
                                    my_chose2 = Convert.ToInt32(Console.ReadLine());
                                    switch (my_chose2)
                                    {
                                        case 0:
                                            my_chose2 = -1;
                                            finish = true;
                                            break;
                                        case 1:
                                            Console.WriteLine("Введите сумму: ");
                                            summ = Convert.ToInt32(Console.ReadLine());
                                            chouse_client.over_acc.put_Fund(summ);
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        case 2:
                                            Console.WriteLine("Введите сумму: ");
                                            summ = Convert.ToInt32(Console.ReadLine());
                                            chouse_client.over_acc.take_Fund(summ);
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        case 3:
                                            Console.WriteLine("Баланс составляет" + Convert.ToString(chouse_client.over_acc.get_Balance()));
                                            Console.ReadKey();
                                            Console.Clear();
                                            my_chose2 = -1;
                                            break;
                                        default:
                                            my_chose2 = -1;
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Поле пароля включает только цифренные значения.\n Повторите!");
                    Console.ReadKey();
                }
                
            }
        }
    }
}
