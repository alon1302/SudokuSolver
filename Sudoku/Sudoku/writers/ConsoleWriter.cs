using Sudoku.formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.writers
{
    class ConsoleWriter : IWriter
    {
        private IFormatter _formatter; // any formatter

        /// <summary>
        /// Constructor that receives any formatter
        /// </summary>
        /// <param name="formatter">the formatter</param>
        public ConsoleWriter(IFormatter formatter)
        {
            _formatter = formatter;
        }

        /// <summary>
        /// function that receives string of some data and print it to the console
        /// right after formatting it by the formatter
        /// </summary>
        /// <param name="data">the data to write</param>
        public void Write(string data)
        {
            Console.WriteLine(_formatter.Format(data));
        }
    }
}

