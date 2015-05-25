using System;
using System.Collections.Generic;
using System.Linq;
 
namespace NumberGuesser
{
    class Program
    {
        public static void Main(string[] args)
        {
            const int numberOfBadАttempts = 4;
            var flag = true;
            const string timeSpanFormat = @"\mm";
            const string quitGame = "q";
            var result = new List<string>();
            string userName;
            int number, enteredNumber;
            var random = new Random();
            var attempts = new List<string>();
 
            string[] feedback ={ "давай again!! ",
                                   "Думай Думай, driveller, ",
                                   "Ээх.. ты тупой! P.S.Без обид!! ",
                                   "Даа.. Ты поражаешь меня(((!! "};
 
            while (flag)
            {
                Console.WriteLine("Салют! Как зовут тебя напарник? :");
                userName = Console.ReadLine();
 
                Console.WriteLine("Рад знакомству, " + userName + "!");
                number = random.Next(0, 100);
                Console.WriteLine("Давай взбодрим твои мозги!\n\nЯ загадал число от 0 до 100! Попробуй отгадать число:");
 
                while (true)
                {
                    var startTime = DateTime.Now;
                    var line = Console.ReadLine();
 
                    switch (line)
                    {
                        case quitGame:
                            Console.WriteLine("Okey, Пока " + userName + "!");
                            Console.ReadLine();
                            Environment.Exit(0);
                            break;
                        case null:
                            continue;
                    }
 
                    try
                    {
                        enteredNumber = int.Parse(line);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("некорректный ввод!");
                        continue;
                    }
 
                    if (enteredNumber == number)
                    {
                        Console.WriteLine();
                        result.Add(enteredNumber + " угадал");
                        var endTime = DateTime.Now;
                        var steps = attempts.Count;
 
                        Console.WriteLine("Ты сделал это за {0}", (endTime - startTime).ToString(timeSpanFormat) + " минут");
                        Console.WriteLine("Вот твоя история попыток:");
                        for (var j = 0; j < steps; j++)
                        {
                            Console.WriteLine(attempts.First());
                            attempts.RemoveAt(0);
                        }
                        break;
                    }
 
                    if (enteredNumber < number)
                    {
                        Console.WriteLine("Это число меньше, чем задуманное");
                        attempts.Add(enteredNumber + "меньше задуманного");
 
                    }
                    else
                    {
                        Console.WriteLine("Это число больше, чем задуманное");
                        attempts.Add(enteredNumber + "больше задуманного");
                    }
                    if (0 == attempts.Count % numberOfBadАttempts)
                    {
                        Console.WriteLine(feedback[random.Next(0, 4)] + userName);
                    }
 
 
                }
 
                Console.WriteLine("еще раз? y/n");
 
 
                do
                {
                    var answer = Console.ReadLine();
 
                    if (string.IsNullOrEmpty(answer))
                    {
                        continue;
                    }
 
                    if (answer.Equals("y"))
                    {
                        break;
                    }
                    if (answer.Equals("n"))
                    {
                        flag = false;
                        break;
                    }
 
                }
                while (true);
            }
        }
    }
}
