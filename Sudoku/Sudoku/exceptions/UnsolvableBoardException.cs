using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class UnsolvableBoardException : Exception
{
    public UnsolvableBoardException(string message) : base(message)
    {
    }
}

