using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarcodeTools;

namespace BarcodeTools.Tests
{
    [TestClass]
    public class BarcodeTests
    {
        [TestMethod]
        public void DataMatrixChecksum_Correct()
        {
            // 1 + 7 + 8 + 9 + 2 + 3 + 1
            string[] inputs = { "C99082234100000DF761", // 93 + 36 = 129
                                "0289264234GDP0000091", // 96 + 27 = 123
                                "100901103856653HHHQ0", // 99 + 22 = 121
                                "C99082234100000DF771", // 93 + 36 = 130
                                "AAA082234100000DF761", // 81 + 30 = 101
                                "C99082234100000DF791", // 93 + 39 = 132
                                "C99082234100000D5791", // 93 + 44 = 137
                                "C99082234100000DF761",
                                "C99082234100000DF761"};
            string[] expecteds = { "1", "7", "9", "0", "9", "8", "3", "1", "1", "1" };
            string result;

            // Act
            // Assert
            for (int i=0; i<inputs.Length; i++)
            {
                result = BarcodeLibrary.BarcodeTools.getChecksumBitForDataMatrix(inputs[i]);
                Assert.AreEqual(expecteds[i], result, $"Case {i} :: Expected {expecteds[i]}, but result was {result}");
            }
        }
    }
}
