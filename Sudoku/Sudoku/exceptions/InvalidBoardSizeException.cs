using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class InvalidBoardSizeException : Exception
{
    public InvalidBoardSizeException(string message) : base(message)
    {
    }
}

