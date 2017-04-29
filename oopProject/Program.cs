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
            var holder = new MongDbHolder("FullData.csv", "Pictures");
            holder.CreateFromCSV();
            Console.ReadKey();
        }
    }
}
