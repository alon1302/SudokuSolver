using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// composite design pattern
/// a group of objects that are treated the same way as a single instance of the same type of object
/// I used this becaues all the writers treated the same way (the write() method)
/// and if needed to implement some more writers in the project its very easy
/// </summary>
class MainWriter : IWriter // implements composite design pattern
{
    private IList<IWriter> _writers; // list of writers

    /// <summary>
    /// empty constructor just to initiate the list
    /// </summary>
    public MainWriter()
    {
        this._writers = new List<IWriter>();
    }

    /// <summary>
    /// function that receives any writer and add it to the list
    /// </summary>
    /// <param name="writer">the writer</param>
    public void AddWriter(IWriter writer)
    {
        _writers.Add(writer);
    }

    /// <summary>
    /// function that receives string of some data and write it to all of the writers in the list
    /// </summary>
    /// <param name="data">the data</param>
    public void Write(string data)
    {
        foreach (IWriter writer in _writers)
        {
            writer.Write(data);
        }
    }
}

