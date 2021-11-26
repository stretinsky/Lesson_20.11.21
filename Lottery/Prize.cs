using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lottery
{
    class Prize
    {
        public string name;
        public int count;
        public List<Student> members;
        DateTime date;
        public Prize(string name, int count)
        {
            this.name = name;
            this.count = count;
            members = new List<Student>();
            date = DateTime.Now;
        }
        public void WriteResult(Student student)
        {
            string path = $@"{Directory.GetCurrentDirectory()}\winners.txt";
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                string output = $"{date}; {name}. {student.name} {student.group}\n";
                byte[] array = System.Text.Encoding.Default.GetBytes(output);
                fs.Write(array, 0, array.Length);
            }
        }
    }
}
