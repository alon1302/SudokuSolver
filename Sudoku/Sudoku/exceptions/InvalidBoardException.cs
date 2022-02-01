using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class InvalidBoardException : Exception
{
    public InvalidBoardException(string message) : base(message)
    {
    }
}

