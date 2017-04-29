using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopProject
{
    class Program
    {
        static void Main(string[] args)
        {
            MongDbHolder.ReadCsv("FullData.csv");
            Console.ReadKey();
        }
    }
}
