using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericBitstream
{
    public interface IBitstream
    {
        uint ReadInt(int bits);
        bool ReadBit();
    }
}
