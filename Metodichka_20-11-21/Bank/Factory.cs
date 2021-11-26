using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodichka_20_11_21
{
    abstract class Factory
    {
        public static Dictionary<int, BankAccount> table = new Dictionary<int, BankAccount>();
        public abstract BankAccount CreateAccount();
        public void DeleteAccount(int ID)
        {
            table.Remove(ID);
        }
        public Dictionary<int, BankAccount> GetDict()
        {
            return table;
        }
    }
}
