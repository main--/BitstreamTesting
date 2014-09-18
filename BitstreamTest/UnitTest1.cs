using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenericBitstream;
using NativeBitstream;

namespace BitstreamTest
{
    [TestClass]
    public class UnitTest1
    {
        private const int Bytes = 1337;

        private Random RnJesus;
        private IBitstream Alpha;
        private IBitstream Bravo;

        [TestInitialize]
        public void Init()
        {
            RnJesus = new Random(42);
            var adata = new byte[Bytes];
            var bdata = new byte[Bytes];
            RnJesus.NextBytes(adata);
            adata.CopyTo(bdata, 0);
            Assert.IsTrue(adata.SequenceEqual(bdata));
            Alpha = new CppBitstream(bdata);
            Bravo = new BitArrayStream(adata);
        }

        [TestMethod]
        public void TestReadInt()
        {
            var remainingBits = Bytes * 8;
            do
            {
                var thisTime = Math.Min(RnJesus.Next(32) + 1, remainingBits);
                remainingBits -= thisTime;
                Assert.AreEqual(Alpha.ReadInt(thisTime), Bravo.ReadInt(thisTime));
            } while (remainingBits > 0);
        }

        [TestMethod]
        public void TestReadBit()
        {
            for (int i = 0; i < Bytes * 8; i++)
                Assert.AreEqual(Alpha.ReadBit(), Bravo.ReadBit());
        }
    }
}
