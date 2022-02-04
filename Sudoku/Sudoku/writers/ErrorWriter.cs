using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.writers
{
    class ErrorWriter : IWriter
    {
        /// <summary>
        /// function that receives string of some data and prints it to the console in ERROR format
        /// </summary>
        /// <param name="data">the data to write</param>
        public void Write(string data)
        {
            Console.WriteLine("\nERROR -> " + data + "\n");
        }
    }
}

