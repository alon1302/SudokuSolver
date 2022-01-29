using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ErrorWriter : IWriter
{
    public void Write(string data)
    {
        Console.Write("There was an ERROR -> " + data);
    }
}

