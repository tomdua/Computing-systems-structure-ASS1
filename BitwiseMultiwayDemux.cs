using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a demux with k outputs, each output with n wires. The input also has n wires.

    class BitwiseMultiwayDemux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Input { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Outputs { get; private set; }

        private WireSet[] wireSetArr;
        private BitwiseDemux[] BitwiseDemuxArr;

        public BitwiseMultiwayDemux(int iSize, int cControlBits)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Outputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }

            //----//

            int inputArray = Outputs.Length;
            int bitWiseSize = 1;
            while (inputArray != 1)
            {
                bitWiseSize += inputArray;
                inputArray = inputArray / 2;
            }


            int initial = 0, contrelNum = 0;
            wireSetArr = new WireSet[bitWiseSize - 1];
            while (initial < bitWiseSize - 1)
            {
                wireSetArr[initial] = new WireSet(Size);
                initial++;
            }
            initial = 0;
            BitwiseDemuxArr = new BitwiseDemux[bitWiseSize];
            while (initial < bitWiseSize)
            {
                BitwiseDemuxArr[initial] = new BitwiseDemux(Size);
                initial++;
            }

            int j = 0;
            BitwiseDemuxArr[j] = new BitwiseDemux(Size);
            BitwiseDemuxArr[j].ConnectInput(Input);
            BitwiseDemuxArr[j].ConnectControl(Control[cControlBits - 1]);
            wireSetArr[j].ConnectInput(BitwiseDemuxArr[j].Output1);
            j++;
            wireSetArr[j].ConnectInput(BitwiseDemuxArr[j - 1].Output2);
            j++;

            inputArray = Outputs.Length;
            int inputIndex = 1; contrelNum = 1;
            inputArray = inputArray / 2;
            while (contrelNum < cControlBits)
            {
                int inputWire = 0;
                int bitWiseLoop = ((int)Math.Pow(2, contrelNum));
                while (inputWire < bitWiseLoop)
                {
                    BitwiseDemuxArr[inputIndex] = new BitwiseDemux(Size);
                    BitwiseDemuxArr[inputIndex].ConnectInput(wireSetArr[inputIndex - 1]);
                    BitwiseDemuxArr[inputIndex].ConnectControl(Control[cControlBits - 1 - contrelNum]);
                    wireSetArr[j].ConnectInput(BitwiseDemuxArr[inputIndex].Output1);
                    j++;
                    wireSetArr[j].ConnectInput(BitwiseDemuxArr[inputIndex].Output2);
                    j++;
                    inputWire++; inputIndex++;

                }
                contrelNum++;
            }
            int x = 0;
            j = bitWiseSize - Outputs.Length - 1;
            while (x < Outputs.Length)
            {
                Outputs[x].ConnectInput(wireSetArr[j]);
                x++; j++;

            }
        }


        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }


        public override bool TestGate()
        {
            Control[0].Value = 0;
            Control[1].Value = 0;
            Input.SetValue(3);
            int i = 0;
            while (i < Size) { 
            if (Outputs[0][i].Value != Input[i].Value) {
                return false;
            }
            i++;
        }
            return true;
        }
    }
}