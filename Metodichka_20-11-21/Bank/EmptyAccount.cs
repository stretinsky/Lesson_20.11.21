using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodichka_20_11_21
{
    class EmptyAccount : Factory
    {
        public override BankAccount CreateAccount()
        {
            BankAccount acc = new BankAccount();
            table.Add(acc.GetID(), acc);
            return acc;
        }
    }
}
