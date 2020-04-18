using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class is used to implement the ALU
    //The specification can be found at https://b1391bd6-da3d-477d-8c01-38cdf774495a.filesusr.com/ugd/56440f_2e6113c60ec34ed0bc2035c9d1313066.pdf slides 48,49
    class ALU : Gate
    {
        //The word size = number of bit in the input and output
        public int Size { get; private set; }

        //Input and output n bit numbers
        public WireSet InputX { get; private set; }
        public WireSet InputY { get; private set; }
        public WireSet Output { get; private set; }

        //Control bit 
        public Wire ZeroX { get; private set; }
        public Wire ZeroY { get; private set; }
        public Wire NotX { get; private set; }
        public Wire NotY { get; private set; }
        public Wire F { get; private set; }
        public Wire NotOutput { get; private set; }

        //Bit outputs
        public Wire Zero { get; private set; }
        public Wire Negative { get; private set; }

        private BitwiseMux BWMuxZX, BWMuxZY, BWMuxNotZX, BWMuxNotZY, BWMuxF, BWMuxNO;
        private WireSet wireMuxZX, wireMuxZY;
        private BitwiseNotGate BWNotZX, BWNotZY, BWNot;
        private BitwiseAndGate BWAnd;
        private MultiBitAdder MBAdder;
        private XorGate xor;
        private MultiBitOrGate MBOr;
        private Wire one;

        public ALU(int iSize)
        {
            Size = iSize;
            InputX = new WireSet(Size);
            InputY = new WireSet(Size);
            ZeroX = new Wire();
            ZeroY = new Wire();
            NotX = new Wire();
            NotY = new Wire();
            F = new Wire();
            NotOutput = new Wire();
            Negative = new Wire();
            Zero = new Wire();


            BWMuxZX = new BitwiseMux(Size);
            wireMuxZX = new WireSet(Size);
            BWMuxZX.ConnectControl(ZeroX);
            BWMuxZX.ConnectInput1(InputX);
            BWMuxZX.ConnectInput2(wireMuxZX);

            BWMuxZY = new BitwiseMux(Size);
            wireMuxZY = new WireSet(Size);
            BWMuxZY.ConnectControl(ZeroY);
            BWMuxZY.ConnectInput1(InputY);
            BWMuxZY.ConnectInput2(wireMuxZY);

            BWNotZX = new BitwiseNotGate(Size);
            BWNotZY = new BitwiseNotGate(Size);
            BWNotZX.ConnectInput(BWMuxZX.Output);
            BWNotZY.ConnectInput(BWMuxZY.Output);
            BWMuxNotZX = new BitwiseMux(Size);
            BWMuxNotZY = new BitwiseMux(Size);
            BWMuxNotZX.ConnectControl(NotX);
            BWMuxNotZX.ConnectInput1(BWMuxZX.Output);
            BWMuxNotZX.ConnectInput2(BWNotZX.Output);
            BWMuxNotZY.ConnectControl(NotY);
            BWMuxNotZY.ConnectInput1(BWMuxZY.Output);
            BWMuxNotZY.ConnectInput2(BWNotZY.Output);

            BWAnd = new BitwiseAndGate(Size);
            MBAdder = new MultiBitAdder(Size);
            BWAnd.ConnectInput1(BWMuxNotZX.Output);
            BWAnd.ConnectInput2(BWMuxNotZY.Output);
            MBAdder.ConnectInput1(BWMuxNotZX.Output);
            MBAdder.ConnectInput2(BWMuxNotZY.Output);

            BWMuxF = new BitwiseMux(Size);
            BWMuxF.ConnectControl(F);
            BWMuxF.ConnectInput1(BWAnd.Output);
            BWMuxF.ConnectInput2(MBAdder.Output);

            BWNot = new BitwiseNotGate(Size);
            BWNot.ConnectInput(BWMuxF.Output);

            BWMuxNO = new BitwiseMux(Size);
            Output = new WireSet(Size);
            BWMuxNO.ConnectControl(NotOutput);
            BWMuxNO.ConnectInput1(BWMuxF.Output);
            BWMuxNO.ConnectInput2(BWNot.Output);
            Output.ConnectInput(BWMuxNO.Output);

            one = new Wire();
            one.Value = 1;
            xor = new XorGate();
            MBOr = new MultiBitOrGate(Size);
            MBOr.ConnectInput(Output);
            xor.ConnectInput1(one);
            xor.ConnectInput2(MBOr.Output);

            Negative.ConnectInput(Output[--Size]);
            Zero.ConnectInput(xor.Output);

        }

        public override bool TestGate()
        {
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 1;
            NotOutput.Value = 1;
            for (int i = 1; i < Size; i++)
            {
                if (this.Output[i].Value != 0) return false;
            }

            InputX.SetValue(2);
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 0;
            NotOutput.Value = 0;
            for (int i = 1; i < Size; i++)
            {
                if (this.Output[i].Value != InputX[i].Value) return false;
            }

            InputX.SetValue(2);
            InputX.SetValue(3);
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 0;
            NotY.Value = 0;
            F.Value = 1;
            NotOutput.Value = 0;
            for (int i = 1; i < Size; i++)
            {
                if (this.Output[i].Value != InputX[i].Value+ InputY[i].Value) return false;
            }

            ZeroX.Value = 1;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 0;
            F.Value = 1;
            NotOutput.Value = 0;
            for (int i = 1; i < Size; i++)
            {
                if (this.Output[i].Value != 0) return false;
            }

            return true;

        }
    }
}