using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseAndGate : BitwiseTwoInputGate
    {
        private AndGate [] andGateArr;

        public BitwiseAndGate(int iSize)
            : base(iSize)
        {
            andGateArr = new AndGate[iSize];
            int i = 0;
            while (i != iSize)
            {
                andGateArr[i] = new AndGate();
                andGateArr[i].ConnectInput1(Input1[i]);
                andGateArr[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(andGateArr[i].Output);
                i++;
                
            }

        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(and)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "And " + Input1 + ", " + Input2 + " -> " + Output;
        }

        public override bool TestGate()
        {
            int i = 0;
            int size = Size;


            for (i = 0; i < size; i++) // 1
            {
                Input1[i].Value = 0;
                Input2[i].Value = 0;
                if (Output[i].Value == 1) return false;
            }

            for (i = 0; i < size; i++) // 2
            {
                Input1[i].Value = 0;
                Input2[i].Value = 1;
                if (Output[i].Value == 1) return false;
            }

            for (i = 0; i < size; i++) // 3
            {
                Input1[i].Value = 1;
                Input2[i].Value = 0;
                if (Output[i].Value == 1) return false;
            }

            for (i = 1; i < size; i++) // 4
            {
               
                    Input1[i].Value = 1;
                    Input2[i].Value = 1;
                    if (Output[i].Value == 0) return false;
              
            }

            return true;
        }
    }
}
