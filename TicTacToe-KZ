using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace gameConsole
{
    class Program
    {
        private static string template = " {0}|{1}|{2} I";
        private static string separator = "{0}-----{1}-";

        private static string smallTemplate = "{0}|{1}|{2}";
        private static string smallSeparator = "-----";

        private static char currentChar;

        static void Main(string[] args)
        {
            Print();

            while (true)
            {
                bool isEnd = false;

                currentChar = GameManager.GetCurrentChar();
                Console.WriteLine("Ходит {0}", currentChar);

                try
                {
                    Console.WriteLine("Введите номер строки");
                    int row = int.Parse(Console.ReadLine()) - 1;
                    Console.WriteLine("Введите номер столбца");
                    int column = int.Parse(Console.ReadLine()) - 1;

                    isEnd = GameManager.SetValueAndCheckWinner(row, column);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Введи цифры для указания места твоего хода");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Print();

                if (isEnd)
                    break;
            }

            Console.WriteLine("Выиграл {0}", currentChar);
        }

        private static void Print()
        {
            var fields = GameManager.GameField;
            var commonFields = GameManager.CommonGameField;

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine();
                Console.WriteLine(smallTemplate, commonFields[i,0].Value, commonFields[i,1].Value, commonFields[i,2].Value);
                Console.Write(smallSeparator);
            }

            Console.WriteLine();

            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < 9; j+=3)
                {
                    Console.Write(template, fields[i,j].Value, fields[i,j + 1].Value, fields[i,j + 2].Value);
                }
                Console.WriteLine();

                for (int j = 0; j < 3; j++)
                {
                    if ((i + 1)%3 == 0)
                        Console.Write(separator, '-', '-');
                    else
                        Console.Write(separator, ' ', ' ');
                }
            }

            Console.WriteLine();
        }
    }
}
