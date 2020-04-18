using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseMux : BitwiseTwoInputGate
    {
        public Wire ControlInput { get; private set; }
        private MuxGate[] muxGateArr;
        


        public BitwiseMux(int iSize)
            : base(iSize)
        {

            ControlInput = new Wire();

            muxGateArr = new MuxGate[iSize];

            int i = 0;
            while (i < iSize)
            {
                muxGateArr[i] = new MuxGate();
                muxGateArr[i].ConnectInput1(Input1[i]);
                muxGateArr[i].ConnectInput2(Input2[i]);
                muxGateArr[i].ConnectControl(ControlInput);

                Output[i].ConnectInput(muxGateArr[i].Output);
                i++;

              }
        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }



        public override string ToString()
        {
            return "Mux " + Input1 + "," + Input2 + ",C" + ControlInput.Value + " -> " + Output;
        }




        public override bool TestGate()
        {
            int i = 0;
            while (i < Size)
            {
                Input1[i].Value = 1;
                Input2[i].Value = 1;
                ControlInput.Value = 0;
                if (Output[i].Value != 1) return false;
                i++;
            }
            i = 0;
            while (i < Size)
            {
                Input1[i].Value = 1;
                Input2[i].Value = 1;
                ControlInput.Value = 1;
                if (Output[i].Value != 1) return false;
                i++;
            }

            i = 0;
            while (i < Size)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 1;
                ControlInput.Value = 1;
                if (Output[i].Value != 1) return false;
                i++;
            }


            i = 0;
            while (i < Size)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 0;
                ControlInput.Value = 0;
                if (Output[i].Value != 0) return false;
                i++;
            }

            i = 0;
            while (i < Size)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 1;
                ControlInput.Value = 0;
                if (Output[i].Value != 0) return false;
                i++;
            }

            i = 0;
            while (i < Size)
            {
                Input1[i].Value = 1;
                Input2[i].Value = 0;
                ControlInput.Value = 0;
                if (Output[i].Value != 1) return false;
                i++;
            }

            i = 0;
            while (i < Size)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 0;
                ControlInput.Value = 1;
                if (Output[i].Value != 0) return false;
                i++;
            }

            i = 0;
            while (i < Size)
            {
                Input1[i].Value = 1;
                Input2[i].Value = 0;
                ControlInput.Value = 1;
                if (Output[i].Value != 0) return false;
                i++;
            }



             return true;
        }
    }
}
