using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

class FileReader : IReader
{
    private string _filePath; // input file path

    /// <summary>
    /// Constructor that receives the input file path
    /// </summary>
    /// <param name="filePath">the file path</param>
    public FileReader(string filePath)
    {
        _filePath = filePath;
    }

    /// <summary>
    /// function that reads all the text from the file located in the path _filePath
    /// in case the file exists
    /// </summary>
    /// <exception cref="FileNotFoundException">if there is no file exists in the path</exception>
    /// <returns>string that contains all the text from the file</returns>
    public string Read()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException($"There is No File Located at {_filePath}"); 
        }
        if (!Path.GetExtension(_filePath).Equals(".txt"))
        {
            throw new FileTypeException($"The File {_filePath} is not in a txt format");
        }
        return File.ReadAllText(_filePath);
    }
}

