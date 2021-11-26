using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class Student
    {
        public string name;
        public short group;
        public int[] numbersArray;
        private int lastWin;
        public Student(string name, short group)
        {
            this.name = name;
            this.group = group;
            numbersArray = new int[4];
            lastWin = 4;
        }
        public void UpdateNumbersArray()
        {
            if (lastWin > 3)
            {
                numbersArray = new int[4];
            }
            else
            {
                numbersArray = new int[lastWin];
            }
        }
        public void WinInLottery()
        {
            lastWin = 0;
        }
        public void lastWinPlus()
        {
            lastWin++;
        }
    }
}
