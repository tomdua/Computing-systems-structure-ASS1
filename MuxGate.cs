using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A mux has 2 inputs. There is a single output and a control bit, selecting which of the 2 inpust should be directed to the output.
    class MuxGate : TwoInputGate
    {
        public Wire ControlInput { get; private set; }
        private AndGate andGate1, andGate2;
        private OrGate orGate;
        private NotGate notGate;
                

        public MuxGate()
        {
            ControlInput = new Wire();
            andGate1 = new AndGate();
            andGate2 = new AndGate();
            orGate = new OrGate();
            notGate = new NotGate();

            andGate1.ConnectInput1(Input1);
            notGate.ConnectInput(ControlInput);
            andGate1.ConnectInput2(notGate.Output);

            andGate2.ConnectInput1(Input2);
            andGate2.ConnectInput2(ControlInput);

            orGate.ConnectInput1(andGate1.Output);
            orGate.ConnectInput2(andGate2.Output);

            Output = orGate.Output;

        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }


        public override string ToString()
        {
            return "Mux " + Input1.Value + "," + Input2.Value + ",C" + ControlInput.Value + " -> " + Output.Value;
        }



        public override bool TestGate()
        {
            Input1.Value = 1;
            Input2.Value = 1;
            ControlInput.Value = 0;
            if (Output.Value != 1) return false;

            Input1.Value = 1;
            Input2.Value = 1;
            ControlInput.Value = 1;
            if (Output.Value != 1) return false;

            Input1.Value = 0;
            Input2.Value = 1;
            ControlInput.Value = 1;
            if (Output.Value != 1) return false;

        
            Input1.Value = 0;
            Input2.Value = 0;
            ControlInput.Value = 0;
            if (Output.Value != 0) return false;

            Input1.Value = 0;
            Input2.Value = 1;
            ControlInput.Value = 0;
            if (Output.Value != 0) return false;

            Input1.Value = 1;
            Input2.Value = 0;
            ControlInput.Value = 0;
            if (Output.Value != 1) return false;

            Input1.Value = 0;
            Input2.Value = 0;
            ControlInput.Value = 1;
            if (Output.Value != 0) return false;

            Input1.Value = 1;
            Input2.Value = 0;
            ControlInput.Value = 1;
            if (Output.Value != 0) return false;




            return true;
        
          }
    }
}
