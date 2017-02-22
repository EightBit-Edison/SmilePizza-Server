using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime current = DateTime.Today;
            current = current.AddHours(1);
            Console.WriteLine(current);
            Console.WriteLine("datetime1 = "+current.ToString(@"dd.MM.yyyy HH:mm:ss") );
            Console.ReadKey();
        }
    }
}
