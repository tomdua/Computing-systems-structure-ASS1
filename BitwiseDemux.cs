using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseDemux : Gate
    {
        public int Size { get; private set; }
        public WireSet Output1 { get; private set; }
        public WireSet Output2 { get; private set; }
        public WireSet Input { get; private set; }
        public Wire Control { get; private set; }
        private Demux[] demuxesArr;

         
        public BitwiseDemux(int iSize)
        {
            Size = iSize;
            Control = new Wire();
            Input = new WireSet(Size);
            demuxesArr = new Demux[Size];
            Output1 = new WireSet(Size);
            Output2 = new WireSet(Size);

            int i = 0;
            while (i<Size)
            {
                demuxesArr[i] = new Demux();
                demuxesArr[i].ConnectInput(Input[i]);
                demuxesArr[i].ConnectControl(Control);

                this.Output1[i].ConnectInput(demuxesArr[i].Output1);
                this.Output2[i].ConnectInput(demuxesArr[i].Output2);
                i++;
            }

        }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        public override bool TestGate()
        {
            int i = 0;
            while (i < Size)
            {
                Input[i].Value = 0;
                Control.Value = 0;
                if (Output1[i].Value != 0 || Output2[i].Value != 0) return false;
                i++;
            }
            i = 0;
            while (i < Size)
            {
                Input[i].Value = 1;
                Control.Value = 0;
                if (Output1[i].Value != 1 || Output2[i].Value != 0) return false;
                i++;
            }
            i = 0;
            while (i < Size)
            {
                Input[i].Value = 0;
                Control.Value = 1;
                if (Output1[i].Value != 0 || Output2[i].Value != 0) return false;
                i++;
            }
            i = 0;
            while (i < Size)
            {
                Input[i].Value = 1;
                Control.Value = 1;
                if (Output1[i].Value != 0 || Output2[i].Value != 1) return false;
                i++;
            }

            return true;
        }
    }
}
