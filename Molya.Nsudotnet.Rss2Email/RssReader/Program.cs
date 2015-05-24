using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RssReader
{
    class Program
    {
        private static DateTime lastPoll = DateTime.Now;
        private static InputContainer container = new InputContainer();

        static void Main(string[] args)
        {
            bool isParsed;
            int portNumber;

            Console.WriteLine("Логин пользователя ЖЖ");
            string blogName = Console.ReadLine();
            Console.WriteLine("Адрес SMTP Сервера");
            container.SmtpServer = Console.ReadLine();
            do
            {
                Console.WriteLine("Введите номер порта в числовом формате");
                isParsed = int.TryParse(Console.ReadLine(), out portNumber);

            } while (!isParsed);
            container.Port = portNumber;
            Console.WriteLine("Имя пользователя");
            container.Login = Console.ReadLine();
            Console.WriteLine("Пароль");
            container.Password = Console.ReadLine();
            Console.WriteLine("Кому слать письма");
            container.MailTo = Console.ReadLine();

            var reader = new LiveJournalReader(blogName);

            while (true)
            {
                Thread.Sleep(20000);

                Console.WriteLine("Загрузка данных из LiveJournal...");
                var posts = reader.ReadPosts();

                if (posts == null)
                {
                    Console.WriteLine("Неудалось загрузить данные.");
                    continue;
                }

                var chachedTime = lastPoll;
                lastPoll = DateTime.Now;


                bool isNewData = false;
                foreach (var post in posts)
                {
                    if (post.PushDateTime > chachedTime)
                    {
                        if (!isNewData)
                        {
                            isNewData = true;
                            Console.WriteLine("Обнаружены новые данные, отправка почты...");
                        }
                        SendMail(post.Title, post.Description);
                    }
                }

                if (!isNewData)
                    Console.WriteLine("Новых данных нет.");
            }
        }

        public static void SendMail(string title, string message)
        {
            try
            {
                using (SmtpClient client = new SmtpClient())
                using (MailMessage mail = new MailMessage())
                {
                    client.Host = container.SmtpServer;
                    client.Port = container.Port;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(container.Login.Split('@')[0], container.Password);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    
                    mail.From = new MailAddress(container.Login);
                    mail.To.Add(new MailAddress(container.MailTo));
                    mail.Subject = title;
                    mail.Body = message;

                    client.Send(mail);
                    Console.WriteLine("Отправлено сообщение на почту: {0}", container.MailTo);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public class InputContainer
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string MailTo { get; set; }
    }
}
