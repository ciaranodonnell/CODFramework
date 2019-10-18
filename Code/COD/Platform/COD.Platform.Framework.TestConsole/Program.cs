using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Framework.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TestStringBuilder();
            GC.Collect();
            GC.Collect();

            TestStringManipulator();
            GC.Collect();
            GC.Collect();
            TestRope();
            Console.ReadLine();
        }

        private static void TestStringBuilder()
        {
            Console.Write("String Builder: ");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var sb = new StringBuilder();
            for (int x = 0; x < 1000; x++)
            {
                sb.Append("Hello ");
            }

            for (int x = 0; x < 1000; x++)
            {
                sb.Insert(50, "World");
                //sb.Insert("World", 50);
            }

            for (int x = 0; x < 1000; x++)
            {
                sb.Replace("Hello ", "Hey ");
            }
            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
        private static void TestStringManipulator()
        {
            Console.Write("String Manipulator: ");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var sb = new StringManipulator();
            for (int x = 0; x < 1000; x++)
            {
                sb.Append("Hello ");
            }

            for (int x = 0; x < 1000; x++)
            {
                sb.Insert("World", 50);
            }

            for (int x = 0; x < 1000; x++)
            {
                sb.Replace("Hello ", "Hey ");
            }
            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
        private static void TestRope()
        {
            Console.Write("Rope: ");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var sb = new Rope();
            for (int x = 0; x < 1000; x++)
            {
                sb.Append("Hello ");
            }

            for (int x = 0; x < 1000; x++)
            {
                sb.Insert("World", 50);
            }

            for (int x = 0; x < 1000; x++)
            {
                sb.Replace("Hello ", "Hey ");
            }
            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
