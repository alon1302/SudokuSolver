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
        if (data.Equals("Wrong input"))
        {
            Console.WriteLine(data);
        }
        else
        {
            Console.WriteLine(_formatter.Format(data));
        }
    }
}

