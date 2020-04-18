using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a FullAdder, taking as input 3 bits - 2 numbers and a carry, and computing the result in the output, and the carry out.
    class FullAdder : TwoInputGate
    {
        public Wire CarryInput { get; private set; }
        public Wire CarryOutput { get; private set; }

        private HalfAdder half1, half2;
        private OrGate or;

        public FullAdder()
        {
            CarryInput = new Wire();
            or = new OrGate();
            half1 = new HalfAdder();
            half2 = new HalfAdder();

            half1.ConnectInput1(Input1);
            half1.ConnectInput2(Input2);

            half2.ConnectInput1(half1.Output);
            half2.ConnectInput2(CarryInput);

            or.ConnectInput1(half1.CarryOutput);
            or.ConnectInput2(half2.CarryOutput);

            Output.ConnectInput(half2.Output);
            CarryOutput = or.Output;

        }


        public override string ToString()
        {
            return Input1.Value + "+" + Input2.Value + " (C" + CarryInput.Value + ") = " + Output.Value + " (C" + CarryOutput.Value + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0; Input2.Value = 0; CarryInput.Value = 0;
            if (CarryOutput.Value != 0 || Output.Value != 0) return false;

            Input1.Value = 0; Input2.Value = 0; CarryInput.Value = 1;
            if (CarryOutput.Value != 0 || Output.Value != 1) return false;

            Input1.Value = 0; Input2.Value = 1; CarryInput.Value = 0;
            if (CarryOutput.Value != 0 || Output.Value != 1) return false;

            Input1.Value = 0; Input2.Value = 1; CarryInput.Value = 1;
            if (CarryOutput.Value != 1 || Output.Value != 0) return false;

            Input1.Value = 1; Input2.Value = 0; CarryInput.Value = 0;
            if (CarryOutput.Value != 0 || Output.Value != 1) return false;

            Input1.Value = 1; Input2.Value = 0; CarryInput.Value = 1;
            if (CarryOutput.Value != 1 || Output.Value != 0) return false;

            Input1.Value = 1; Input2.Value = 1; CarryInput.Value = 0;
            if (CarryOutput.Value != 1 || Output.Value != 0) return false;

            Input1.Value = 1; Input2.Value = 1; CarryInput.Value = 1;
            if (CarryOutput.Value != 1 || Output.Value != 1) return false;

            return true;
        }
    }
}
