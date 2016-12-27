using BarcodeLibrary.DataMatrix.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarcodeLibrary.DataMatrix
{
    public class CodeWord : ICodeWord
    {
        byte _word;

        public byte Word
        {
            get
            {
                return _word;
            }
        }

        public CodeWord(byte word)
        {
            this._word = word;
        }
        public CodeWord(char word)
        {
            // converts only the least significant 8 bits of the char
            _word = Convert.ToByte(word);
        }
        public CodeWord(string word)
        {
            // Only takes the first character in the string
            if(word != null && word.Length > 0)
            {
                _word = Convert.ToByte(word[0]);
            }
            else
            {
                throw new EncodingException("A non-empty, initialized string must be passed to create a new CodeWord");
            }
        }

        public bool BitSetAtLocation(int location)
        {
            if (location > 8)
                throw new EncodingException("Maximum of 8 can be passed to the BitSetAtLocation method. (How many bits in an ASCII byte?)");
            else
            {
                return (Word & (1 << (8 - location))) != 0;
            }
        }
    }

    public class EncodingException : Exception
    {
        public EncodingException(string message) : base(message) { }
    }
}