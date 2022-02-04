using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.writers
{
    interface IWriter
    {
        /// <summary>
        /// function that receives some data and write it to any destenation
        /// </summary>
        /// <param name="data"></param>
        void Write(string data);
    }
}
