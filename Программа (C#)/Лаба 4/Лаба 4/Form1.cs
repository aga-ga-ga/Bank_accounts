using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Лаба_4
{




    public partial class Form1 : Form
    {
        

        // new commit
        public Form1()
        {
            InitializeComponent();
            radioButton1.Text = "Сберегательный счет";
            radioButton2.Text = "Счет с временем погашения";
            radioButton3.Text = "Текущий счет";
            radioButton4.Text = "Счет с овердрафтом";
            toolStripStatusLabel1.Text = "Баланс = 0";
            textBox1.Visible = true;
            label4.Visible = true;
            panel1.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            statusStrip1.Visible = false;
            
            
        }

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

                MessageBox.Show(mes, "Баланс", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Недостаточно средств на счете.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Недостаточно средств на счете.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        if (take_balance < get_Balance())
                        {
                            set_Balance(get_Balance() - take_balance);
                            string mes = "Снятие проводилось после наступления срока платежа, в результате вы сняли: \n";
                            mes += Convert.ToString(take_balance);
                            MessageBox.Show(mes, "Снятие фонда", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            set_Month_Flowing(get_Month_Flowing() + 1);
                        }
                }
                else
                {
                    if (take_balance > get_Balance())
                    {
                        MessageBox.Show("Недостаточно средств на счете.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        if (take_balance < get_Balance())
                        {
                            set_Balance(get_Balance() - take_balance);
                            int amount_given_to_depositor = take_balance - Convert.ToInt32((take_balance * get_Penalty_Rate()));
                            string mes = "Снятие проводилось до наступления срока платежа, в результате вы сняли: \n";
                            mes += Convert.ToString(amount_given_to_depositor);
                            MessageBox.Show(mes, "Снятие фонда", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show(mes, "Отчет о работе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            public override void take_Fund(int take_balance)
            {
                set_Total_Transactions(get_Total_Transactions() + 1);
                if (take_balance > get_Balance())
                    MessageBox.Show("Недостаточно средств на счете.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show(mes, "Отчет о работе", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public Savings_Account savings_account;
        public Timed_Maturity_Account timed_maturity_account;
        public Checking_Account checking_account;
        public Overdraft_Account overdraft_account;

        // кнопка которая задает начальный баланс
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox1.Text) > 0)
                {
                    label4.Visible = false;
                    textBox1.Visible = false;
                    textBox1.Enabled = false;
                    button4.Visible = false;
                    button4.Enabled = false;
                    panel1.Visible = true;
                    radioButton1.Visible = true;
                    radioButton2.Visible = true;
                    radioButton3.Visible = true;
                    radioButton4.Visible = true;
                    statusStrip1.Visible = true;
                    savings_account = new Savings_Account(Convert.ToInt32(textBox1.Text));
                    timed_maturity_account = new Timed_Maturity_Account(Convert.ToInt32(textBox1.Text));
                    checking_account = new Checking_Account(Convert.ToInt32(textBox1.Text));
                    overdraft_account = new Overdraft_Account(Convert.ToInt32(textBox1.Text));

                }
                else
                {
                    MessageBox.Show("Не корректные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Вы ввели не число.","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textBox1.Text = "";
            }
        }
        /*
         * radioButton1.Text = "Сберегательный счет";
         * radioButton2.Text = "Счет с временем погашения";
         * radioButton3.Text = "Текущий счет";
         * radioButton4.Text = "Счет с овердрафтом";
         */
        // вложить фонд
        private void button1_Click(object sender, EventArgs e)
        {
            int put_value = 0;
            try
            {
                put_value = Convert.ToInt32(textBox2.Text);
                if (radioButton1.Checked)
                {
                    savings_account.put_Fund(put_value);
                    toolStripStatusLabel1.Text = Convert.ToString(savings_account.get_Balance());
                }
                else
                {
                    if (radioButton2.Checked)
                    {
                        timed_maturity_account.put_Fund(put_value);
                        toolStripStatusLabel1.Text = Convert.ToString(timed_maturity_account.get_Balance());
                    }
                    else
                    {
                        if (radioButton3.Checked)
                        {
                            checking_account.put_Fund(put_value);
                            toolStripStatusLabel1.Text = Convert.ToString(checking_account.get_Balance());
                        }
                        else
                        {
                            if (radioButton4.Checked)
                            {
                                overdraft_account.put_Fund(put_value);
                                toolStripStatusLabel1.Text = Convert.ToString(overdraft_account.get_Balance());
                            }
                        }
                    }
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Вы ввели не число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
                put_value = 0;
            }
        }
        // снять фонд
        private void button2_Click(object sender, EventArgs e)
        {
            int take_value = 0;
            try
            {
                take_value = Convert.ToInt32(textBox2.Text);
                if (radioButton1.Checked)
                {
                    savings_account.take_Fund(take_value);
                    toolStripStatusLabel1.Text = Convert.ToString(savings_account.get_Balance());
                }
                else
                {
                    if (radioButton2.Checked)
                    {
                        timed_maturity_account.take_Fund(take_value);
                        toolStripStatusLabel1.Text = Convert.ToString(timed_maturity_account.get_Balance());
                    }
                    else
                    {
                        if (radioButton3.Checked)
                        {
                            checking_account.take_Fund(take_value);
                            toolStripStatusLabel1.Text = Convert.ToString(checking_account.get_Balance());
                        }
                        else
                        {
                            if (radioButton4.Checked)
                            {
                                overdraft_account.take_Fund(take_value);
                                toolStripStatusLabel1.Text = Convert.ToString(overdraft_account.get_Balance());
                            }
                        }
                    }
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Вы ввели не число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
                take_value = 0;
            }
        }
        // проверить баланс
        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                toolStripStatusLabel1.Text = Convert.ToString(savings_account.get_Balance());
            else
                if (radioButton2.Checked)
                    toolStripStatusLabel1.Text = Convert.ToString(timed_maturity_account.get_Balance());
                else
                    if (radioButton3.Checked)
                        toolStripStatusLabel1.Text = Convert.ToString(checking_account.get_Balance());
                    else
                        if (radioButton4.Checked)
                            toolStripStatusLabel1.Text = Convert.ToString(overdraft_account.get_Balance());
        }

    }
}
