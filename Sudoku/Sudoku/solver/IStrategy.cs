using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IStrategy
{
    /// <summary>
    /// function that receives a board and tries to solve it according to the strategy
    /// </summary>
    /// <param name="board">the sudoku board</param>
    /// <returns>true if the function changed sometihing in the baurs</returns>
    bool Solve(SudokuBoard board);
}

