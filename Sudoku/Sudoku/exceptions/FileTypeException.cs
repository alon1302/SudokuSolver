using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class FileTypeException : Exception
{
    public FileTypeException(string massege) : base(massege)
    { 
    }
}

