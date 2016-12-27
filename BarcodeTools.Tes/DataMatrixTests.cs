using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeLibrary.DataMatrix.Interfaces;
using BarcodeLibrary.DataMatrix;

namespace BarcodeTools.Tests
{
    [TestClass]
    public class DataMatrixTests
    {
        public object Moq { get; private set; }

        [TestMethod]
        public void TestGetBitInCodeWord()
        {
            // Arrange
            CodeWord codeWord = new CodeWord((byte)217); // 11011001

            // Act
            bool r1 = codeWord.BitSetAtLocation(1);
            bool r2 = codeWord.BitSetAtLocation(2);
            bool r3 = codeWord.BitSetAtLocation(3);
            bool r4 = codeWord.BitSetAtLocation(4);
            bool r5 = codeWord.BitSetAtLocation(5);
            bool r6 = codeWord.BitSetAtLocation(6);
            bool r7 = codeWord.BitSetAtLocation(7);
            bool r8 = codeWord.BitSetAtLocation(8);

            // Assert
            Assert.IsTrue(r1, "Bit 1 should be on");
            Assert.IsTrue(r2, "Bit 2 should be on");
            Assert.IsFalse(r3, "Bit 3 should be off");
            Assert.IsTrue(r4, "Bit 4 should be on");
            Assert.IsTrue(r5, "Bit 5 should be on");
            Assert.IsFalse(r6, "Bit 6 should be off");
            Assert.IsFalse(r7, "Bit 7 should be off");
            Assert.IsTrue(r8, "Bit 8 should be on");
        }
    }
}
