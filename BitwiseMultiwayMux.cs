using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a mux with k input, each input with n wires. The output also has n wires.

    class BitwiseMultiwayMux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Output { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Inputs { get; private set; }

        private WireSet[] wireSetArr;
        private BitwiseMux[] bitWiseMuxArr;

        public BitwiseMultiwayMux(int iSize, int cControlBits)
        {
            Size = iSize;
            Output = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Inputs = new WireSet[(int)Math.Pow(2, cControlBits)];

            for (int i = 0; i < Inputs.Length; i++)
            {
                Inputs[i] = new WireSet(Size);

            }

            int inputArray = Inputs.Length;
            int bitWiseSize = 1;
            while (inputArray != 1)
            {
                bitWiseSize += inputArray;
                inputArray = inputArray / 2;
            }

            wireSetArr = new WireSet[bitWiseSize+1];
            bitWiseMuxArr = new BitwiseMux[bitWiseSize+1];

            int initial = 0, contrelNum = 0;
            while(initial < bitWiseSize)
            {
                bitWiseMuxArr[initial] = new BitwiseMux(Size);
                wireSetArr[initial] = new WireSet(Size);
                initial++;
            }


            inputArray = Inputs.Length;
            int j = 0, indexWire = 0, inputIndex = 0; contrelNum = 1;
            while (contrelNum < cControlBits)
                {
                while (inputIndex < Inputs.Length)
                {
                    bitWiseMuxArr[j].ConnectControl(Control[0]);
                    bitWiseMuxArr[j].ConnectInput1(Inputs[inputIndex]);
                    inputIndex++;
                    bitWiseMuxArr[j].ConnectInput2(Inputs[inputIndex]);
                    wireSetArr[j].ConnectInput(bitWiseMuxArr[j].Output);
                    j++;
                    inputIndex++;

                }
                int inputWire = 0;
                inputArray = inputArray / 2;
                while (inputWire< inputArray)
                        {
                        bitWiseMuxArr[j].ConnectControl(Control[contrelNum]);
                        bitWiseMuxArr[j].ConnectInput1(wireSetArr[indexWire]);
                        inputWire++; indexWire++;
                         bitWiseMuxArr[j].ConnectInput2(wireSetArr[indexWire]);
                        wireSetArr[j].ConnectInput(bitWiseMuxArr[j].Output);
                        j++;
                        inputWire ++; indexWire++;
                        }
                         contrelNum++;
            }
            Output.ConnectInput(wireSetArr[j-1]);
        }

        public void ConnectInput(int i, WireSet wsInput)
        {
            Inputs[i].ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }



        public override bool TestGate()
        {
            Inputs[0][0].Value = 0;
            Control[0].Value = 0;
            if (Output[0].Value == 1)
            {
                return false;
            }

            Inputs[0][1].Value = 1;
            Control[0].Value = 1;
            if (Output[0].Value == 1 && Output[0].Value==1)
            {
                return false;
            }
            return true;
        }
    }
}
