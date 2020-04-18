using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This gate implements the xor operation. To implement it, follow the example in the And gate.
    class XorGate : TwoInputGate
    {
        private NAndGate NandGate1;
        private AndGate AndGate1;
        private OrGate OrGate1;

        //public static int XOR_GATES = 0;

        public XorGate()
        {
           
            //init the gates
            OrGate1 = new OrGate();
            AndGate1 = new AndGate();
            NandGate1 = new NAndGate();

            NandGate1.ConnectInput1(this.Input1);
            NandGate1.ConnectInput2(this.Input2);

            OrGate1.ConnectInput1(this.Input1);
            OrGate1.ConnectInput2(this.Input2);


            AndGate1.ConnectInput1(NandGate1.Output);
            AndGate1.ConnectInput2(OrGate1.Output);
            Output = AndGate1.Output;
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(xor)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Xor " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }


        //this method is used to test the gate. 
        //we simply check whether the truth table is properly implemented.
        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
    }

