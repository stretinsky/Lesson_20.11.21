using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace Lottery
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            using (StreamReader sr = new StreamReader($@"{Directory.GetCurrentDirectory()}\students.txt", Encoding.Default))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    students.Add(new Student(s.Split('#')[0], short.Parse(s.Split('#')[1])));
                }
            }
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("Создаем розыгрыш. Введите, что собираетесь разыгрывать ('exit' Чтобы выйти)");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "exit":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Введите количество призов");
                        int count;
                        while (!int.TryParse(Console.ReadLine(), out count))
                        {
                            Console.WriteLine("Некорректное значение, попробуйте снова");
                        }
                        Prize lottery = new Prize(input, count);
                        foreach (var student in students)
                        {
                            student.lastWinPlus();
                            student.UpdateNumbersArray();
                            Console.Clear();
                            Console.WriteLine($"Вы {student.name}. Будете участвовать в розыгрыше {lottery.name}?\n> 1 - Да\n> 2 - Нет");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    lottery.members.Add(student);
                                    break;
                                default:
                                    break;
                            }
                        }
                        for (int j = 0; j < lottery.count; j++)
                        {
                            Console.WriteLine($"Разыгрываем {j+1}-ый приз");
                            
                            int number = 0;
                            foreach (var member in lottery.members)
                            {
                                member.UpdateNumbersArray();
                                for (int i = 0; i < member.numbersArray.Length; i++)
                                {
                                    member.numbersArray[i] = number;
                                    number++;
                                }
                            }

                            // выводит какие всех участников и какие у них есть числа
                            foreach (var member in lottery.members)
                            {
                                Console.Write($"{member.name}: ");
                                foreach (var n in member.numbersArray)
                                {
                                    Console.Write($"{n} ");
                                }
                                Console.WriteLine();
                            }

                            Random r = new Random();
                            int result = r.Next(number);
                            Console.WriteLine($"Выпало число {result}");
                            foreach (var member in lottery.members)
                            {
                                if (member.numbersArray.Contains(result))
                                {
                                    Console.WriteLine($"Победил(-a) {member.name}\n");
                                    member.WinInLottery();
                                    lottery.WriteResult(member);
                                    break;
                                }
                            }
                        }
                        break;
                }
                
            }


            Console.WriteLine("Task 2");
            Application excel = new Application();
            Workbook workbook = excel.Workbooks.Open($@"{Directory.GetCurrentDirectory()}\Болезни.xlsx");
            Worksheet worksheet = workbook.Worksheets[1];
            object[,] readRange = worksheet.Range["A2", "B10"].Value2;
            Dictionary<string, string> deseases = new Dictionary<string, string>();
            for (int i = 1; i <= readRange.GetLength(0); i++)
            {
                deseases.Add(readRange[i, 1].ToString().ToLower(), readRange[i, 2].ToString());
            }
            workbook.Close();
            workbook = excel.Workbooks.Open($@"{Directory.GetCurrentDirectory()}\Общее.xlsx");
            worksheet = workbook.Worksheets[1];
            readRange = worksheet.Range["G2", "G35"].Value2;
            for (int i = 1; i <= readRange.Length; i++)
            {
                string readString = readRange[i, 1].ToString().ToLower();
                foreach (var desease in deseases)
                {
                    if (readString.Contains(desease.Key))
                    {
                        readRange[i, 1] = desease.Value;
                        break;
                    }
                }
            }
            worksheet.Range["H2", "H35"].Value2 = readRange;
            workbook.Save();
            workbook.Close();
            excel.Quit();
        }
    }
}
