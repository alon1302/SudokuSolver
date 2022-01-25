using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.writers
{
    class FileWriter : IWriter
    {
        private string filePath;

        public void Write(string data)
        {
            File.WriteAllText(filePath, data);
        }
    }
}
