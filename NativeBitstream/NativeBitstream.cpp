// Dies ist die Haupt-DLL.

#include "stdafx.h"

#include "NativeBitstream.h"
#include "bitstream.h"

NativeBitstream::CppBitstream::CppBitstream(array<Byte>^ data)
{
	char* mybytes = new char[data->Length];
	for (int i = 0; i < data->Length; i++)
		mybytes[i] = data[i];
	// convert array to string so they can convert it back to an array
	// FUCKING GENIUS -.-'
	// why would you use std::string if all you want is a goddamn byte array!?
	std::string tmpstr(mybytes, data->Length);
	bitstream = new Bitstream(tmpstr);
}

NativeBitstream::CppBitstream::~CppBitstream()
{
	this->!CppBitstream();
}

NativeBitstream::CppBitstream::!CppBitstream()
{
	if (bitstream != nullptr)
	{
		delete bitstream; // no memory leakerinos pls
		bitstream = nullptr;
	}
}

UInt32 NativeBitstream::CppBitstream::ReadInt(int bits)
{
	return bitstream->get_bits(bits);
}

Boolean NativeBitstream::CppBitstream::ReadBit()
{
	return bitstream->get_bits(1);
}
