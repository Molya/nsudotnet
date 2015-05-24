using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json_serial
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClass a = new TestClass();
            try
            {
                Serializer.SerializeObject(a);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }
    }
}
