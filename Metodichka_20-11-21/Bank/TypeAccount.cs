using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodichka_20_11_21
{
    class TypeAccount : Factory
    {
        BankAccount.accountType type;
        public TypeAccount(BankAccount.accountType type) : base()
        {
            this.type = type;
        }
        public override BankAccount CreateAccount()
        {
            BankAccount acc = new BankAccount(type);
            table.Add(acc.GetID(), acc);
            return acc;
        }
    }
}
