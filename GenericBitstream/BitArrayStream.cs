using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using GenericBitstream;

namespace GenericBitstream
{
    public class BitArrayStream : IBitstream
    {
        BitArray array;

        int index = 0;

        public BitArrayStream(byte[] data)
        {
            array = new BitArray(data);
        }

        public void Seek(int pos, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Begin)
                index = pos;

            if (origin == SeekOrigin.Current)
                index += pos;

            if (origin == SeekOrigin.End)
                index = array.Count - pos;
        }

        public uint ReadInt(int numBits)
        {

            uint result = PeekInt(numBits);
            index += numBits;

            return result;
        }

        public uint PeekInt(int numBits)
        {
            uint result = 0;
            int intPos = 0;

            for (int i = 0; i < numBits; i++)
            {
                result |= ((array[i + index] ? 1u : 0u) << intPos++);
            }

            return result;
        }

        public uint ReadInt(uint numBits)
        {
            return ReadInt((int)numBits);
        }

        public bool ReadBit()
        {
            return array[index++];
        }

        public byte ReadByte()
        {
            return (byte)ReadInt(8);
        }

        public byte ReadByte(int numBits)
        {

            return (byte)ReadInt(numBits);
        }

        public byte[] ReadBytes(int length)
        {
            byte[] result = new byte[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = this.ReadByte();
            }

            return result;
        }

        public string ReadString(int size)
        {
            List<Byte> result = new List<byte>(512);
            int pos = 0;
            while (true)
            {
                byte a = ReadByte();
                if (a == 0)
                    break;

                result.Add(a);

                if (++pos == size)
                {
                    break;
                }
            }

            return Encoding.ASCII.GetString(result.ToArray());
        }

        public string ReadString()
        {
            return ReadString(int.MaxValue);
        }

        public uint ReadVarInt()
        {
            int count = 0;
            uint result = 0;

            while (true)
            {
                if (count > 5)
                    throw new InvalidDataException("VarInt32 out of range");


                uint tmpByte = ReadByte();

                result |= (tmpByte & 0x7F) << (7 * count);


                if ((tmpByte & 0x80) == 0)
                    break;

                count++;
            }

            return result;
        }

        public string PeekBools(int length)
        {
            byte[] buffer = new byte[length];

            int idx = 0;
            for (int i = index; i < Math.Min(index + length, array.Count); i++)
            {
                if (array[i])
                    buffer[idx++] = 49;
                else
                    buffer[idx++] = 48;
            }

            return Encoding.ASCII.GetString(buffer, 0, Math.Min(length, array.Count - index));
        }
    }
}