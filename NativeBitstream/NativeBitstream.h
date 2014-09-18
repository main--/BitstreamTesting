// NativeBitstream.h

#pragma once
class Bitstream;

using namespace System;
using namespace GenericBitstream;

namespace NativeBitstream {

	public ref class CppBitstream : public IBitstream
	{
		Bitstream* bitstream;

	public:
		CppBitstream(array<Byte>^ data);
		~CppBitstream();
		!CppBitstream();

		virtual UInt32 ReadInt(int);
		virtual Boolean ReadBit();
	};
}
