using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Program
    {
        static void Main(string[] args)
        {
            //WireSet wire = new WireSet(8);
            //wire.Set2sComplement(2);
            //int m = wire.Get2sComplement();
            MultiBitAdder half = new MultiBitAdder(4);
            //Test that the unit testing works properly
            if (!half.TestGate())
                Console.WriteLine("bugbug");


            Console.WriteLine("done");
            Console.ReadLine();

        }
    }
}
