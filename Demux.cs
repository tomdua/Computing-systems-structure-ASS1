using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A demux has 2 outputs. There is a single input and a control bit, selecting whether the input should be directed to the first or second output.
    class Demux : Gate
    {
        public Wire Output1 { get; private set; }
        public Wire Output2 { get; private set; }
        public Wire Input { get; private set; }
        public Wire Control { get; private set; }

        public AndGate andGate1, andGate2;
        public NotGate notGate;

        public Demux()
        {
            Input = new Wire();
            andGate1 = new AndGate();
            andGate2 = new AndGate();
            notGate = new NotGate();
            Output2 = new Wire();
            Output1 = new Wire();
            Control = new Wire();

            andGate1.ConnectInput1(Input);
            notGate.ConnectInput(Control);
            andGate1.ConnectInput2(notGate.Output);
            Output1 = andGate1.Output;

            andGate2.ConnectInput1(Input);
            andGate2.ConnectInput2(Control);
             Output2 = andGate2.Output;
            

         }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }



        public override bool TestGate()
        {
            Input.Value = 0;
            Control.Value = 0;
            if (Output1.Value != 0 || Output2.Value != 0) return false;

            Input.Value = 1;
            Control.Value = 0;
            if (Output1.Value != 1 || Output2.Value != 0) return false;

            Input.Value = 0;
            Control.Value = 1;
            if (Output1.Value != 0 || Output2.Value != 0) return false;

            Input.Value = 1;
            Control.Value = 1;
            if (Output1.Value != 0 || Output2.Value != 1) return false;

            return true;
         

        }
    }
}
