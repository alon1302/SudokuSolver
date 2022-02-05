using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sudoku.exceptions
{
    /// <summary>
    /// custom exception represent unsupported file type
    /// </summary>
    public class FileTypeException : Exception
    {
        /// <summary>
        /// Construtctor that receives a massege for the exception
        /// and passing it to the base class
        /// </summary>
        /// <param name="message">exception massege</param>
        public FileTypeException(string massege) : base(massege)
        {
        }
    }
}

