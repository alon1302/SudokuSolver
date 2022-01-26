using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class FileWriter : IWriter
{
    private string _filePath;

    public FileWriter(string filePath)
    {
        this._filePath = filePath;
    }

    public void Write(string data)
    {
        File.WriteAllText(_filePath, data);
    }
}

