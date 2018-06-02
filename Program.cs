using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter number of profiles: ");
            string inputSize = Console.ReadLine();
            int listSize = Convert.ToUInt16(inputSize);
            MuserList muserList = new MuserList();
            muserList.createMusers(listSize);

            Console.WriteLine("\n~~~~~~ END OF PROGRAM ~~~~~~");
            Console.Read();
        }
    }
}