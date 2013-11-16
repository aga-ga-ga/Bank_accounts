using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба_5
{
    public abstract class Universal_account
    {
        public Universal_account() { }
        public Universal_account(int balance)
        {
            set_Balance(balance);// баланс который мы передаем
            set_Month_Flowing(1);// отсчет ведется от 1 месяца
        }
        // Вложить фонд
        public abstract void put_Fund(int put_balance);
        // Снять фонд
        public abstract void take_Fund(int take_balance);
        // Проверить текущий баланс
        public void check_Up_Balance()
        {
            string mes = "Текущий баланс составляет: \n";
            mes += Convert.ToString(get_Balance());

            Console.WriteLine(mes);
        }

        // текущий месяц (для коректной работы месяцов будет более 12)
        private int month_Flowing;
        public void set_Month_Flowing(int month_Flowing)
        {
            this.month_Flowing = month_Flowing;
        }
        public int get_Month_Flowing()
        {
            return month_Flowing;
        }
        // Баланс
        private int balance;
        public void set_Balance(int balance)
        {
            this.balance = balance;
        }
        public int get_Balance()
        {
            return balance;
        }
    }
    public class Savings_Account : Universal_account
    {
        public Savings_Account() { }
        public Savings_Account(int balance)
        {
            set_Interest_Rate(0.02);// процентная ставка равна 2%
            set_Balance(balance);  // баланс который мы передаем
            set_Month_Flowing(1); // отсчет ведется от 1 месяца
        }
        // мы будем считать что сначала мы вкладываем деньги после чего подсчитываем процентную ставку
        public override void put_Fund(int put_balance)
        {
            set_Balance(get_Balance() + put_balance);
            set_Balance(get_Balance() + Convert.ToInt32((get_Balance() * get_Interest_Rate())));
            set_Month_Flowing(get_Month_Flowing() + 1);
        }
        // мы будем считать что сначала мы снимаем деньги(не превышающие баланс) после чего подсчитываем процентную ставку за месяц
        public override void take_Fund(int take_balance)
        {
            if (take_balance > get_Balance())
            {
                Console.WriteLine("Недостаточно средств на счете.");
            }
            else
                if (take_balance < get_Balance())
                {
                    set_Balance(get_Balance() - take_balance);
                    set_Balance(get_Balance() + Convert.ToInt32((get_Balance() * get_Interest_Rate())));
                    set_Month_Flowing(get_Month_Flowing() + 1);
                }
        }
        // процентная ставка
        private double interest_Rate;
        public void set_Interest_Rate(double interest_rate)
        {
            this.interest_Rate = interest_rate;
        }
        public double get_Interest_Rate()
        {
            return interest_Rate;
        }
    }
    public class Timed_Maturity_Account : Savings_Account
    {
        public Timed_Maturity_Account() { }
        public Timed_Maturity_Account(int balance)
        {
            set_Interest_Rate(0.02);// процентная ставка равна 2%
            set_Balance(balance);// баланс который мы передаем
            set_Month_Flowing(1);// отсчет ведется от 1 месяца
            set_Month_Payment(5);// срок наступления пладежа будем считать за 5 месяц
            set_Penalty_Rate(0.05); // процент неустойки 5%
        }
        // процент неустойки
        private double penalty_Rate;
        public double get_Penalty_Rate()
        {
            return penalty_Rate;
        }
        public void set_Penalty_Rate(double penalty_Rate)
        {
            this.penalty_Rate = penalty_Rate;
        }
        // месяц наступления срока платежа
        private int month_Payment;
        public int get_Month_Payment()
        {
            return month_Payment;
        }
        public void set_Month_Payment(int month_Payment)
        {
            this.month_Payment = month_Payment;
        }
        public override void take_Fund(int take_balance)
        {
            if (get_Month_Flowing() >= get_Month_Payment())
            {
                if (take_balance > get_Balance())
                {
                    Console.WriteLine("Недостаточно средств на счете.");
                }
                else
                    if (take_balance < get_Balance())
                    {
                        set_Balance(get_Balance() - take_balance);
                        string mes = "Снятие проводилось после наступления срока платежа, в результате вы сняли: \n";
                        mes += Convert.ToString(take_balance);
                        Console.WriteLine(mes);
                        set_Month_Flowing(get_Month_Flowing() + 1);
                    }
            }
            else
            {
                if (take_balance > get_Balance())
                {
                    Console.WriteLine("Недостаточно средств на счете.");
                }
                else
                    if (take_balance < get_Balance())
                    {
                        set_Balance(get_Balance() - take_balance);
                        int amount_given_to_depositor = take_balance - Convert.ToInt32((take_balance * get_Penalty_Rate()));
                        string mes = "Снятие проводилось до наступления срока платежа, в результате вы сняли: \n";
                        mes += Convert.ToString(amount_given_to_depositor);
                        Console.WriteLine(mes);
                        set_Month_Flowing(get_Month_Flowing() + 1);
                    }
            }
        }
    }
    public class Checking_Account : Universal_account
    {
        public Checking_Account() { }
        //переход на следующий месяц будем осуществлять когда превышение операций составит 3 операции
        public Checking_Account(int balance)
        {
            set_Balance(balance); // баланс который мы передаем
            set_Month_Flowing(1); // отсчет ведется от 1 месяца
            set_Month_Quota(6); // количество разрешенных операций будем считаль равно 6
            set_Total_Transactions(0); // количество проведенных операций изначально 0
            set_Per_Transaction_Fee(3); // штраф за каждое превышение операции равен 3
        }
        // количество допустимых операций в месяц
        private int month_Quota;
        public int get_Month_Quota()
        {
            return month_Quota;
        }
        public void set_Month_Quota(int month_Quota)
        {
            this.month_Quota = month_Quota;
        }
        // количество проведенных операций
        private int total_Transactions;
        public int get_Total_Transactions()
        {
            return total_Transactions;
        }
        public void set_Total_Transactions(int total_Transactions)
        {
            this.total_Transactions = total_Transactions;
        }
        // штраф за превышение одной операции
        private int per_Transaction_Fee;
        public int get_Per_Transaction_Fee()
        {
            return per_Transaction_Fee;
        }
        public void set_Per_Transaction_Fee(int per_Transaction_Fee)
        {
            this.per_Transaction_Fee = per_Transaction_Fee;
        }
        public override void put_Fund(int put_balance)
        {
            set_Total_Transactions(get_Total_Transactions() + 1);
            set_Balance(get_Balance() + put_balance);
            if (get_Total_Transactions() == 9)
            {
                set_Total_Transactions(0);
                set_Month_Flowing(get_Month_Flowing() + 1);
            }
            else
            {
                if (get_Total_Transactions() > get_Month_Quota())
                {
                    int fee = (get_Total_Transactions() - get_Month_Quota()) * get_Per_Transaction_Fee();
                    set_Balance(get_Balance() - fee);
                    string mes = "Количество операций было превышено с вас сняли: \n";
                    mes += Convert.ToInt32(fee);
                    mes += "\nВаш текущий баланс составляет: \n";
                    mes += Convert.ToInt32(get_Balance());
                    Console.WriteLine(mes);
                }
            }
        }
        public override void take_Fund(int take_balance)
        {
            set_Total_Transactions(get_Total_Transactions() + 1);
            if (take_balance > get_Balance())
                Console.WriteLine("Недостаточно средств на счете.");
            else
                if (take_balance < get_Balance())
                    set_Balance(get_Balance() - take_balance);
            if (get_Total_Transactions() == 9)
            {
                set_Total_Transactions(0);
                set_Month_Flowing(get_Month_Flowing() + 1);
            }
            else
            {
                if (get_Total_Transactions() > get_Month_Quota())
                {
                    int fee = (get_Total_Transactions() - get_Month_Quota()) * get_Per_Transaction_Fee();
                    set_Balance(get_Balance() - fee);
                    string mes = "Количество операций было превышено с вас сняли: \n";
                    mes += Convert.ToInt32(fee);
                    mes += "\nВаш текущий баланс составляет: \n";
                    mes += Convert.ToInt32(get_Balance());
                    Console.WriteLine(mes);
                }
            }
        }
    }
    public class Overdraft_Account : Universal_account
    {
        public Overdraft_Account() { }
        public Overdraft_Account(int balance)
        {
            set_Balance(balance); // баланс который мы передаем
            set_Month_Flowing(1); // отсчет ведется от 1 месяца
            set_Interest_Rate(0.2); // процентные выплаты будем считать 20%
        }
        // мы будем считать что сначала мы вкладываем деньги после чего подсчитываем процентную ставку за месяц
        public override void put_Fund(int put_balance)
        {
            set_Balance(get_Balance() + put_balance);
            if (get_Balance() < 0)
                set_Balance(get_Balance() + Convert.ToInt32((get_Balance() * get_Interest_Rate())));
            set_Month_Flowing(get_Month_Flowing() + 1);
        }
        // мы будем считать что сначала мы снимаем деньги(не превышающие баланс) после чего подсчитываем процентную ставку
        public override void take_Fund(int take_balance)
        {
            set_Balance(get_Balance() - take_balance);
            if (get_Balance() < 0)
                set_Balance(get_Balance() + Convert.ToInt32((get_Balance() * get_Interest_Rate())));
            set_Month_Flowing(get_Month_Flowing() + 1);
        }
        // процентная ставка
        private double interest_Rate;
        public void set_Interest_Rate(double interest_rate)
        {
            this.interest_Rate = interest_rate;
        }
        public double get_Interest_Rate()
        {
            return interest_Rate;
        }
    }

}
