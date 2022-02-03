using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

class FileReader : IReader
{
    private string _filePath;

    public FileReader(string filePath)
    {
        _filePath = filePath;
    }

    public string Read()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException($"There is No File Located at {_filePath}"); 
        }
        return File.ReadAllText(_filePath);
    }
}

