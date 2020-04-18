using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)
    class MultiBitAndGate : MultiBitGate
    {
        private AndGate[] AndGateArr;

        public MultiBitAndGate(int iInputCount)
            : base(iInputCount)
        {
            int i = 0;
            AndGateArr = new AndGate[iInputCount];
            AndGateArr[i] = new AndGate();
            AndGateArr[i].ConnectInput1(m_wsInput[i]);
            AndGateArr[i].ConnectInput2(m_wsInput[i+1]);
            i++;
            while (i != iInputCount-1)
            {
                AndGateArr[i] = new AndGate();
                AndGateArr[i].ConnectInput1(AndGateArr[i-1].Output);
                AndGateArr[i].ConnectInput2(m_wsInput[i+1]);
                i++;
            }
             Output = AndGateArr[iInputCount-2].Output;
        }


        public override bool TestGate()
        {
            int i = 0;
            int size = m_wsInput.Size;

            for (i = 0; i < size; i++) // 1
            {
                m_wsInput[i].Value = 0;
            }
            if (Output.Value == 1) return false;

            for (i = 0; i < size; i++) // 2
            {
                m_wsInput[i].Value = 1;
            }
            if (Output.Value == 0) return false;


            for (i = 0; i < size; i++) // 2
            {
                m_wsInput[i].Value = 1;
            }
            if (Output.Value == 0) return false;


            for (i = 1; i < size; i++) // 3
            {
                for (int j = 0; j < size-1; j++)
                { 
                 m_wsInput[j].Value = 1;
                 m_wsInput[i].Value = 1;
                     if (Output.Value == 0) return false;
                }
            }
            return true;

        }
    }

}
 