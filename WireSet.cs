using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents a set of n wires (a cable)
    class WireSet
    {
        //Word size - number of bits in the register
        public int Size { get; private set; }

        public bool InputConected { get; private set; }

        //An indexer providing access to a single wire in the wireset
        public Wire this[int i]
        {
            get
            {
                return m_aWires[i];
            }
        }
        private Wire[] m_aWires;

        public WireSet(int iSize)
        {
            Size = iSize;
            InputConected = false;
            m_aWires = new Wire[iSize];
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i] = new Wire();
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = m_aWires.Length - 1; i >= 0; i--)
                s += m_aWires[i].Value;
            s += "]";
            return s;
        }

        //Transform a positive integer value into binary and set the wires accordingly, with 0 being the LSB
        public void SetValue(int iValue)
        {
            // The remainder of the division is 2, 
            //the number is divided by 2 until it reaches 0
            int i = 0;
            while (i < Size)
            {
                m_aWires[i].Value = iValue % 2;
                iValue = iValue / 2;

                i++;
            }
        }

        //Transform the binary code into a positive integer
        public int GetValue()
        {
            int num = 0, i = 0;
            while (i < Size)
            {
                    num += m_aWires[i].Value* (int)Math.Pow(2, i);
                    i++;
            }
            return num;
        }

        //Transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        public void Set2sComplement(int iValue)
        {
            if (iValue >= 0)
            {
                SetValue(iValue);
            }
            else { 
                int posValue;
                 int i = 0;
                posValue = (-1) * iValue;
                SetValue(posValue);
                while (i < Size)
                {
                    if (m_aWires[i].Value == 0)
                    {
                        m_aWires[i].Value = 1;
                    }
                    else
                    {
                        m_aWires[i].Value = 0;
                    }
                    i++;
                }

            int carryNum = 0; i = 1;

            if (m_aWires[0].Value == 0)
            {
                m_aWires[0].Value = 1;
                i = Size;
            }
            else
            {
                m_aWires[0].Value = 0;
                carryNum = 1;

            }

                while (carryNum == 1 && i < Size)
                {
                    if (m_aWires[i].Value == 0)
                    {
                        m_aWires[i].Value = 1;
                        i = Size; carryNum = 0;

                    }
                    else
                    {
                        m_aWires[i].Value = 0;
                        i++;
                    }
                }
            }
        }


        //Transform the binary code in 2`s complement into an integer
        public int Get2sComplement()
        {
            int carryNum = 1, i = 0;
            if (m_aWires[Size - 1].Value == 0)
            {
                return GetValue();
            }
            else
            {
                while (carryNum == 1)
                {
                    if (m_aWires[i].Value == 0)
                    {
                        m_aWires[i].Value = 1;
                        carryNum = 1;
                        i++;
                    }
                    else
                    {
                        m_aWires[i].Value = 0;
                        carryNum = 0;
                        i++;
                    }
                }
                i = 0; int binaryToDec = 0;
                while (i < Size)
                {
                    if (m_aWires[i].Value == 0)
                    {
                        m_aWires[i].Value = 1;
                    }
                    else
                    {
                        m_aWires[i].Value = 0;
                    }
                    i++;
                }
                binaryToDec = (GetValue() * (-1));
                Set2sComplement(binaryToDec);
                return binaryToDec;
            }
        }




        public void ConnectInput(WireSet wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            if (wIn.Size != Size)
                throw new InvalidOperationException("Cannot connect two wiresets of different sizes.");
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i].ConnectInput(wIn[i]);

            InputConected = true;

        }

    }
}
