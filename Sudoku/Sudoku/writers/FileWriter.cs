using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.writers
{
    public class FileWriter : IWriter
    {
        private string _filePath; // file path

        /// <summary>
        /// Constructor that receives a file path to write to
        /// </summary>
        /// <param name="filePath">the file path</param>
        public FileWriter(string filePath)
        {
            this._filePath = filePath;
        }

        /// <summary>
        /// function that receives string of some data and print it to the file
        /// </summary>
        /// <param name="data">the data to write</param>
        public void Write(string data)
        {
            File.WriteAllText(_filePath, data);
        }
    }
}

