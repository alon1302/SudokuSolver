using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sudoku.readers
{
    public class ConsoleReader : IReader
    {
        /// <summary>
        /// function that read user input from the console
        /// </summary>
        /// <returns>the string that the user entered</returns>
        public string Read()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
            Console.WriteLine("enter string that represent sudoku board: ");
            return Console.ReadLine();
        }
    }
}

