﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a HalfAdder, taking as input 2 bits - 2 numbers and computing the result in the output, and the carry out.

    class HalfAdder : TwoInputGate
    {
        public Wire CarryOutput { get; private set; }

        private XorGate xor;
        private AndGate and;

        public HalfAdder()
        {
            xor = new XorGate();
            and = new AndGate();
            CarryOutput = new Wire();

            and.ConnectInput1(Input1);
            and.ConnectInput2(Input2);
            CarryOutput.ConnectInput(and.Output);

            xor.ConnectInput1(Input1);
            xor.ConnectInput2(Input2);
            Output.ConnectInput(xor.Output);

        }


        public override string ToString()
        {
            return "HA " + Input1.Value + "," + Input2.Value + " -> " + Output.Value + " (C" + CarryOutput + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0; Input2.Value = 0;
            if (CarryOutput.Value != 0 || Output.Value != 0) return false;

            Input1.Value = 0; Input2.Value = 1;
            if (CarryOutput.Value != 0 || Output.Value != 1) return false;

            Input1.Value = 1; Input2.Value = 0;
            if (CarryOutput.Value != 0 || Output.Value != 1) return false;

             Input1.Value = 1; Input2.Value = 1;
            if (CarryOutput.Value != 1 || Output.Value != 0) return false;

            return true;
        }
    }
}
