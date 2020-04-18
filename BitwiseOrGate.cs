using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseOrGate : BitwiseTwoInputGate
    {
        private OrGate[] orGateArr;

        public BitwiseOrGate(int iSize)
            : base(iSize)
        {
            orGateArr = new OrGate[iSize];
            int i = 0;
            while (i < iSize)
            {
                orGateArr[i] = new OrGate();
                orGateArr[i].ConnectInput1(Input1[i]);
                orGateArr[i].ConnectInput2(Input2[i]);

                Output[i].ConnectInput(orGateArr[i].Output);
                i++;

            }
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(or)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Or " + Input1 + ", " + Input2 + " -> " + Output;
        }

       
    public override bool TestGate()
        {
            int i = 0;
            while(i< Size)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 0;
                if (Output[i].Value == 1) return false;
                i++;
            }
            i = 0;
            while (i < Size)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 1;
                if (Output[i].Value == 0) return false;
                i++;
            }
            i = 0;
            while (i < Size)
            {
                Input1[i].Value = 1;
                Input2[i].Value = 0;
                if (Output[i].Value == 0) return false;
                i++;
            }

            i = 0;
            while (i < Size)
            {
                    Input1[i].Value = 1;
                    Input2[i].Value = 1;
                    if (Output[i].Value == 0) return false;
                i++;
                
            }
            return true;
        }
    }
}
