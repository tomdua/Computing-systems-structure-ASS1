using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements an adder, receving as input two n bit numbers, and outputing the sum of the two numbers
    class MultiBitAdder : Gate
    {
        //Word size - number of bits in each input
        public int Size { get; private set; }

        public WireSet Input1 { get; private set; }
        public WireSet Input2 { get; private set; }
        public WireSet Output { get; private set; }
        //An overflow bit for the summation computation
        public Wire Overflow { get; private set; }
        private HalfAdder half;
        private FullAdder full;
        private Wire CarryOutputVal;

        public MultiBitAdder(int iSize)
        {
            Size = iSize;
            Input1 = new WireSet(Size);
            Input2 = new WireSet(Size);
            Output = new WireSet(Size);
            Overflow = new Wire();
            full = new FullAdder();
            half = new HalfAdder();



            int i = 0;
            if (Size == 1)
            {
                half = new HalfAdder();
                half.ConnectInput1(Input1[i]);
                half.ConnectInput2(Input2[i]);
                Output[i].ConnectInput(half.Output);
                Overflow.ConnectInput(half.CarryOutput);
               //CarryOutputVal = new Wire();
                //CarryOutputVal = half.CarryOutput;
            }
            else {
                while (i < Size)
                {
                    if (i == 0)
                    {
                        half = new HalfAdder();
                        half.ConnectInput1(Input1[i]);
                        half.ConnectInput2(Input2[i]);
                        Output[i].ConnectInput(half.Output);
                        CarryOutputVal = new Wire();
                        CarryOutputVal = half.CarryOutput;
                        i++;
                    }
                    else
                    {
                        full = new FullAdder();
                        full.ConnectInput1(Input1[i]);
                        full.ConnectInput2(Input2[i]);
                        full.CarryInput.ConnectInput(CarryOutputVal);
                        Output[i].ConnectInput(full.Output);
                        CarryOutputVal = new Wire();
                        CarryOutputVal = full.CarryOutput;
                        i++;
                        if (i == Size)
                        Overflow.ConnectInput(full.CarryOutput);
                    }
                    
                }
            }
        }

        public override string ToString()
        {
            return Input1 + "(" + Input1.Get2sComplement() + ")" + " + " + Input2 + "(" + Input2.Get2sComplement() + ")" + " = " + Output + "(" + Output.Get2sComplement() + ")";
        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }


        public override bool TestGate()
        {
            Input1.SetValue(0);Input2.SetValue(0);
            int i = 0;
            if (Overflow.Value != 0) return false;
            while (i < Size)
            {
                if (Output[i].Value != 0 ) return false;
                i++;
            }


            Input1.SetValue(1); Input2.SetValue(0);
            i = 1;
            if (Overflow.Value != 0) return false;
            while (i < Size)
            {
                if (Output[i].Value != 0) return false;
                i++;
            }
        
            Input1.SetValue(0); Input2.SetValue(1);
            i = 1;
            if (Overflow.Value != 0) return false;
            while (i < Size)
            {
                if (Output[i].Value != 0) return false;
                i++;
            }

            Input1.SetValue(1); Input2.SetValue(1);
            
           if (Overflow.Value != 0) return false;
            i = 2;
            while (i < Size)
            {
                if (Output[i].Value != 0) return false;
                i++;
            }
            return true;
        }
    }
}
