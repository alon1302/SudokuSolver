using System;
using System.Collections.Generic;
using System.Text;

class ConsoleWriter : IWriter
{


    public ConsoleWriter()
    {
        // TODO get Formatter
    }

    public void Write(string data)
    {
        // TODO use formatter in print
        Console.WriteLine(data);
    }
}

