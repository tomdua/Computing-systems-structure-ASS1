using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This gate implements the or operation. To implement it, follow the example in the And gate.
    class OrGate : TwoInputGate
    {
        //your code here 
        private NotGate NotGate1;
        private NotGate NotGate2;
        private NAndGate NandGate1;


        public OrGate()
        {
            //init the gates
             NotGate1 = new NotGate();
             NotGate2 = new NotGate();
             NandGate1 = new NAndGate();

            NotGate1.ConnectInput(this.Input1);
            NotGate2.ConnectInput(this.Input2);

            NandGate1.ConnectInput1(NotGate1.Output);
            NandGate1.ConnectInput2(NotGate2.Output);
            Output = NandGate1.Output;

         
        }


        public override string ToString()
        {
            return "Or " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }

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
            if (Output.Value != 1)
                return false;
            return true;
        }
    }

}
