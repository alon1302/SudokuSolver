using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.readers
{
    interface IReader
    {
        /// <summary>
        /// function that read text from any source
        /// </summary>
        /// <returns>all the text from the source</returns>
        string Read();
    }
}

