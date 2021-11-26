using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodichka_20_11_21
{
    class BankTransaction
    {
        readonly decimal amount;
        readonly DateTime time;
        public BankTransaction(decimal amount)
        {
            this.amount = amount;
            time = DateTime.Now;
        }
        public string GetInfo()
        {
            return $"{time} {amount}\n";
        }
    }
}
