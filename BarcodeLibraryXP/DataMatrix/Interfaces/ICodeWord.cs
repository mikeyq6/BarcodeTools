using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarcodeLibrary.DataMatrix.Interfaces
{
    public interface ICodeWord
    {
        byte Word { get; }

        bool BitSetAtLocation(int location);
    }
}
