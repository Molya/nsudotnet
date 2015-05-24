using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json_serial
{
    class TestClass
    {
        public List<int> List;
        [NonSerialized]
        public String c;
        public int a;
        public char b;
        public String[] AStrings;

        public TestClass()
        {
            var random = new Random();
            AStrings = new string[10];
            for (int i = 0; i < AStrings.Length; i++ )
            {
                //List.Add(random.Next());
                AStrings[i] = random.Next().ToString();
            }
        }
    }
}
