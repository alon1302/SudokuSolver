using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sudoku.exceptions
{
    /// <summary>
    /// custom exception that represent state of invalid board
    /// </summary>
    public class InvalidBoardException : Exception
    {
        /// <summary>
        /// Construtctor that receives a massege for the exception
        /// and passing it to the base class
        /// </summary>
        /// <param name="message">exception massege</param>
        public InvalidBoardException(string message) : base(message)
        {
        }
    }
}

