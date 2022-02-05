using Sudoku.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.solver
{
    /// <summary>
    /// Naked Single is a famous strategy among all the sudoku solvers
    /// Basic Definition:
    /// if there is an empty cell that has only one option we can be sure that this specific option
    /// is the sulution to that cell
    /// so we set this cell the value of that option
    /// and fix the options of all the cells in the board that share a region with this cell
    /// this strategy make quick and effective reduction of empty in the sudoku board 
    /// </summary>
    public class NakedSingleStrategy : IStrategy
    {
        private SudokuBoard _board; // the sudoku board

        /// <summary>
        /// function that receives sudoku board and try to solve it using the naked single strategy
        /// when there is a single option in a cell the function set the value of it to the cell
        /// and calls a function that remove all the options from the regions
        /// for every single the function found it start iterarete from the start of the board for more naked singles
        /// returns true is one or more naked single was found
        /// </summary>
        /// <param name="board">the sudoku board</param>
        /// <returns>true if the function found one naled or more or false otherwise</returns>
        public bool Solve(SudokuBoard board)
        {
            this._board = board;
            int count = 0;
            for (int row = 0; row < _board.RowSize; row++)
            {
                for (int col = 0; col < _board.RowSize; col++)
                {
                    SudokuCell current = _board[row, col];
                    if (!current.IsSolved())
                    {
                        if (current.NumOfOptions == 0)
                        {
                            return false;
                        }
                        if (current.NumOfOptions == 1)
                        {
                            count++;
                            char lastOption = current.GetTheOnlyOption();
                            current.Value = lastOption;
                            _board.RemoveOptionFromCellRegions(lastOption, row, col);
                            row = 0;
                            col = 0;
                        }
                    }
                }
            }
            return count > 0;
        }
    }
}

