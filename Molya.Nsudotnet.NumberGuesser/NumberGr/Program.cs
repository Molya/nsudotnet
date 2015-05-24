using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molya.Nsudotnet.NumberGuesser
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool flag = true;
            String answer;
            bool amiss = false;
            const string timeSpanFormat = @"hh\:mm\:ss";
           const string quitGame = "q";
            List<String> result = new List<string>();
           
        while(flag){

            
                 String[] feedback =
            {
     "давай again!! ",
     "Думай Думай, driveller, ",
     "Ээх.. ты тупой! P.S.Без обид!! ",
     "Даа.. Ты поражаешь меня(((!! "
            };

            Console.WriteLine("Салют! Как зовут тебя напарник? :");
            String _user = Console.ReadLine();

            Console.WriteLine("Рад знакомству, " + _user+"!");
            Random _randNumber = new Random();

            int _number = _randNumber.Next(0, 100);
            Console.WriteLine("Давай взбодрим твои мозги!\n\nЯ загадал число от 0 до 100! Попробуй отгадать:");


            while (!amiss)
            {

                for (var i = 0; i < 4; i++)
                {

                    DateTime startTime = DateTime.Now;
                    String line = Console.ReadLine();

                       if (line == quitGame)
                       {
                            Console.WriteLine("Okey, Пока " + _user + "!");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }

                  


                    else if (line == null)
                    {
                        continue;
                    }

                    int _enterNumber = 0;

                    try
                    {
                        _enterNumber = int.Parse(line);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Ты че?! Нужна цифра!");
                        continue;
                    }

                     
                    switch (_enterNumber < _number)
                    {
                        case true:
                            Console.WriteLine("Это число меньше, чем задуманное");

                            break;
                        case false:
                            Console.WriteLine("Это число больше, чем задуманное");
                            result.Add(_enterNumber + " больше");
                            break;
                    }

                    if (_enterNumber == _number)
                      
                    {    amiss = true;
                            result.Add(_enterNumber + " угадал");
                            DateTime endTime = DateTime.Now;
                            int steps = result.Count;

                            Console.WriteLine("Ты сделал это за {0}", (endTime - startTime).ToString(timeSpanFormat));
                            for (var j = 0; j < steps; j++)
                            {
                                Console.WriteLine(result.First());
                                result.RemoveAt(0);
                            }
                            break;

                    }
                }

                if (!amiss)
                {
                    Console.WriteLine(feedback[_randNumber.Next(0, 4)] + _user);
                }
            }

               Console.WriteLine("еще раз? y/n");
            
               while(true){
                    answer = Console.ReadLine();
                    if (answer.Equals("y")){
                        flag = true;
                        break;
                    }
                    if (answer.Equals("n"))
                    {
                        flag = false;
                        break;
                    }
                }
            Console.ReadLine();
            }
        }
    }
}
