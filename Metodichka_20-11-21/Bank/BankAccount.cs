using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Metodichka_20_11_21
{
    class BankAccount
    {
        System.Collections.Queue transactions = new System.Collections.Queue();
        public enum accountType { current, saving }
        private static int id = 100;
        private decimal balance = 0;
        private accountType type;
        private int ID = id;
        internal BankAccount()
        {
            balance = 0;
            type = accountType.current;
            AddID();
            Console.Clear();
            Console.WriteLine("Счёт открыт. Его номер: {0}", GetID());
        }
        internal BankAccount(decimal balance)
        {
            this.balance = balance;
            type = accountType.current;
            AddID();
            Console.Clear();
            Console.WriteLine("Счёт открыт. Его номер: {0}", GetID());
        }
        internal BankAccount(accountType type)
        {
            this.balance = 0;
            this.type = type;
            AddID();
            Console.Clear();
            Console.WriteLine("Счёт открыт. Его номер: {0}", GetID());
        }
        internal BankAccount(decimal balance, accountType type)
        {
            this.balance = balance;
            this.type = type;
            AddID();
            Console.Clear();
            Console.WriteLine("Счёт открыт. Его номер: {0}", GetID());
        }
        public void SwitchType()
        {
            if (type == accountType.current)
            {
                type = accountType.saving;
            }
            else
            {
                type = accountType.current;
            }
            Console.Clear();
            Console.WriteLine("Тип аккаунта изменен на " + type);
        }
        public void Deposite(decimal amount)
        {
            balance += amount;
            transactions.Enqueue(new BankTransaction(amount));
        }
        public void Withdraw(decimal amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                transactions.Enqueue(new BankTransaction((-1) * amount));
                Console.Clear();
                Console.WriteLine("Деньги выведены со счета");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Недостаточно средств");
            }
        }
        public void GetAccountInfo()
        {
            Console.WriteLine("Номер счета: {0}\nТип счёта: {1}\nБаланс: {2}", ID, type, balance);
        }
        public void Trasfer(int a, int b, decimal amount, Dictionary<int, BankAccount> accounts)
        {
            if (accounts[a].GetBalance() >= amount)
            {
                accounts[a].Withdraw(amount);
                accounts[b].Deposite(amount);
                Console.Clear();
                Console.WriteLine("Деньги переведены");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Недостаточно средств для перевода");
            }
        }
        public void Dipose()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"transactions");
            using (FileStream fs = new FileStream($@"{path}\{ID}.txt", FileMode.Create))
            {
                foreach (var transaction in transactions)
                {
                    GC.SuppressFinalize(transaction);
                    BankTransaction t = (BankTransaction)transaction;
                    string output = t.GetInfo();
                    byte[] array = System.Text.Encoding.Default.GetBytes(output);
                    fs.Write(array, 0, array.Length);
                }
            }
        }
        public int GetID()
        {
            return ID;
        }
        public decimal GetBalance()
        {
            return balance;
        }
        public static int AddID()
        {
            return ++id;
        }
    }
}
