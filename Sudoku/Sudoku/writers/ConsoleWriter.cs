using System;
using System.Collections.Generic;
using System.Text;

class ConsoleWriter : IWriter
{
    private IFormatter _formatter;

    public ConsoleWriter(IFormatter formatter)
    {
        _formatter = formatter;
    }

    public void Write(string data)
    {
        Console.WriteLine(_formatter.Format(data));
    }
}

