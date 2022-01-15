using System;
using System.Collections.Generic;
using System.Text;

class MainWriter : IWriter
{
    private IList<IWriter> _writers;

    public MainWriter()
    {
        this._writers = new List<IWriter>();
    }

    public void AddWriter(IWriter writer)
    {
        _writers.Add(writer);
    }

    public void Write(string data)
    {
        foreach (IWriter writer in _writers)
        {
            writer.Write(data);
        }
    }
}

