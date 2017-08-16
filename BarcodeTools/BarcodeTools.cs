using DataMatrix.net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using IEC16022Sharp;

namespace BarcodeLibrary
{
    public static class BarcodeTools
    {
        public const int SET_A = 0;
        public const int SET_B = 1;
        public const int SET_C = 2;

        public const int START_A = 103;
        public const int START_B = 104;
        public const int START_C = 105;

        private const char FROM_C_TO_A = (char)101;
        private const char FROM_A_TO_C = (char)99;

        public static Image getGS1128Barcode(string value, string readable)
        {
            GS1_128 gs1128Encoder = new GS1_128();
            return gs1128Encoder.Encode(value, readable);
        }

        public static string encode128barcode(string value)
        {


            string result = string.Empty;
            List<char> encoded = new List<char>();
            int currentEncoding = -1;

            encoded.Add((char)getFirstCode(value));

            if (!string.IsNullOrEmpty(value))
            {
                // Get start character
                if (value[0] >= '0' && value[0] <= '9')
                {
                    currentEncoding = SET_C;
                }
                else
                {
                    currentEncoding = SET_A;
                }

                for (int i = 0; i < value.Length; i++)
                {
                    char c = value[i];
                    int val = (int)c;
                    if (c >= '0' && c <= '9')
                    {
                        if (currentEncoding != SET_C)
                        {
                            encoded.Add(FROM_A_TO_C);
                            currentEncoding = SET_C;
                        }
                    }
                    else
                    {
                        if (currentEncoding != SET_A)
                        {
                            encoded.Add(FROM_C_TO_A);
                            currentEncoding = SET_A;
                        }
                    }
                    encoded.Add((char)convertCharacter(c));
                }

                // Add the checksum
                encoded.Add((char)generate128checksum(value));
            }


            return new string(encoded.ToArray());
        }

        private static int generate128checksum(string value)
        {
            int checksum = 0;
            int total = 0;

            if (!string.IsNullOrEmpty(value))
            {
                total += getFirstCode(value);

                for (int i = 0; i < value.Length; i++)
                {
                    total += (i + 1) * (int)value[i];
                }

                checksum = total % 103;
            }

            return checksum;
        }

        private static int getFirstCode(string value)
        {
            int first = 0;

            if (!string.IsNullOrEmpty(value))
            {
                // Get start character
                if (value[0] >= '0' && value[0] <= '9')
                {
                    first = START_C;
                }
                else
                {
                    first = START_A;
                }
            }

            return first;
        }

        private static int convertCharacter(char c)
        {
            int result = 0;

            if (c >= '0' && c <= '9')
            {
                result = (int)c - 32;
            }
            else if (c >= 'A' && c <= 'Z')
            {
                result = (int)c - 32;
            }
            else if (c >= 'a' && c <= 'z')
            {
                result = (int)c - 32;
            }
            else
            {
                result = (int)c - 32;
            }

            return result;
        }

        // Do the checksum digit for the barcode (Epost), using EAN/UCC-13 Check digit algorithm
        public static string get_checksum_bit(string barcode)
        {
            char[] chars = barcode.ToCharArray();

            for (int i = 0; i < 3; i++)
            {
                int val = 0;

                if (chars[i] < '0' || chars[i] > '9')
                {
                    val = ((int)chars[i]) % 10;
                    chars[i] = (char)(val + 48);
                }
            }

            int j = 0;
            int sum = 0, sum1 = 0, sum2 = 0;
            for (int i = chars.Length - 1; i >= 0; i--)
            {
                if (i % 2 == 0)
                    sum1 += (((int)chars[i]) - 48);
                else
                    sum2 += (((int)chars[i]) - 48);
            }
            sum = sum2 + (sum1 * 3);

            int digit = 0;
            if (sum % 10 != 0)
                digit = 10 - (sum % 10);

            return ((char)(digit + 48)).ToString();
        }

        public static string getChecksumBitForDataMatrix(string code)
        {
            string checksum = "";
            char[] chars = code.ToCharArray();
            int count = 0;

            // Convert any non-numeric characters into numbers according to the rules
            foreach(char c in chars)
            {
                if(!char.IsDigit(c))
                {
                    int ascVal = c % 10;
                    chars[count] = char.Parse("" + ascVal);
                }
                count++;
            }
            count = chars.Length - 1;
            int sum1 = 0;
            while (count >= 0) {
                sum1 += chars[count] - '0';
                count -= 2;
            }
            sum1 *= 3;
            count = chars.Length - 2;
            int sum2 = 0;
            while (count >= 0)
            {
                sum2 += chars[count] - '0';
                count -= 2;
            }
            int total = sum1 + sum2;

            if (total % 10 == 0)
                checksum = "0";
            else
                checksum = (10 - (total % 10)).ToString();

            return checksum;
        }

        public static Bitmap GenerateGS1DataMatrix(string code)
        {
            return GenerateGS1DataMatrix(code, 8);
        }

        public static Bitmap GenerateGS1DataMatrix(string code, int moduleSize)
        {
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();
            options.ModuleSize = moduleSize;
            options.MarginSize = 0;
            options.BackColor = Color.White;
            options.ForeColor = Color.Black;
            options.Scheme = DmtxScheme.DmtxSchemeAsciiGS1;
            return encoder.EncodeImage(code, options);
            //return GenerateGS1DataMatrix(code.Select(x => (byte)x).ToArray(), moduleSize);
        }

        /*public static Bitmap GenerateGS1DataMatrix(byte[] bytes, int moduleSize)
        {
            
            IEC16022Sharp.DataMatrix matrix = new IEC16022Sharp.DataMatrix(bytes, 26, 26, EncodingType.Ascii);
            Bitmap image = DMImgUtility.SimpleResizeBmp(matrix.Image, 3, 5);
            return image;
            
        }*/
    }
}