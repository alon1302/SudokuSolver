using Sudoku.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sudoku.solver
{
    /// <summary>
    /// Hidden Single is a famous strategy among all the sudoku solvers
    /// Basic Definition:
    /// if there is an empty cell that one of it's options doesn't exist anywhere else
    /// in one of the cell regions (row, column or box) 
    /// when this situation heppens we can be sure that this specific option is the sulution to that cell
    /// so we set this cell the value of that option
    /// and fix the options of all the cells in the board that share a region with this cell
    /// this strategy make quick and effective reduction of empty cells in the sudoku board 
    /// </summary>
    public class HiddenSingleStrategy : IStrategy
    {
        private SudokuBoard _board; // the sudoku board 

        /// <summary>
        /// function that receives sudoku board and try to solve it using the hidden single strategy
        /// when there is a single the function set the value of it to the cell
        /// and calls a function that remove all the options from the regions
        /// for every single the function found it start iterarete from the start of the bourd for more hidden singles
        /// returns true is one or more hidden single was found
        /// </summary>
        /// <param name="board">the sudoku board</param>
        /// <returns>true if the function found one hidden or more or false otherwise</returns>
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
                        foreach (char option in current.Options)
                        {
                            if (IsSingle(option, row, col))
                            {
                                count++;
                                current.Value = option;
                                _board.RemoveOptionFromCellRegions(option, row, col);
                                row = 0;
                                col = 0;
                                break;
                            }
                        }
                    }
                }
            }
            return count > 0;
        }

        /// <summary>
        /// function that receives an option of a cell and the location of the cell
        /// calls functions to check if this option is single option in any of the regions (row, column, box)
        /// the function return true if this option is hidden single or false if not
        /// </summary>
        /// <param name="option">the option to check</param>
        /// <param name="row">row index</param>
        /// <param name="col">column index</param>
        /// <returns>true if the option is single or false otherwise</returns>
        private bool IsSingle(char option, int row, int col)
        {
            return IsSingleInRow(option, row) || IsSingleInCol(option, col) || IsSingleInBox(option, row, col);
        }

        /// <summary>
        /// function that receives an option of a cell and the row index of the cell
        /// checks if this option is single option in the row
        /// the function returns true if this option is single in row or false if not
        /// </summary>
        /// <param name="option">the option to check</param>
        /// <param name="row">row index</param>
        /// <returns>true if the option is single in row or false otherwise</returns>
        private bool IsSingleInRow(char option, int row)
        {
            int count = 0;
            for (int col = 0; col < _board.RowSize; col++)
            {
                SudokuCell current = _board[row, col];
                if (!current.IsSolved() && current.HasOption(option))
                {
                    count++;
                    if (count > 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// function that receives an option of a cell and the column index of the cell
        /// checks if this option is single option in the column
        /// the function returns true if this option is single in row or false if not
        /// </summary>
        /// <param name="option">the option to check</param>
        /// <param name="col">column index</param>
        /// <returns>true if the option is single in column or false otherwise</returns>
        private bool IsSingleInCol(char option, int col)
        {
            int count = 0;
            for (int row = 0; row < _board.RowSize; row++)
            {
                SudokuCell current = _board[row, col];
                if (!current.IsSolved() && current.HasOption(option))
                {
                    count++;
                    if (count > 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// function that receives an option of a cell and the location of the cell
        /// checks if this option is single option in the box of this cell
        /// the function returns true if this option is single in box or false if not
        /// </summary>
        /// <param name="option">the option to check</param>
        /// <param name="row">row index</param>
        /// <param name="col">column index</param>
        /// <returns>true if the option is single in column or false otherwise</returns>
        private bool IsSingleInBox(char option, int row, int col)
        {
            int count = 0;
            int boxSize = (int)Math.Sqrt(_board.RowSize);
            int boxRow = row - (row % boxSize);
            int boxCol = col - (col % boxSize);
            for (int i = boxRow; i < boxRow + boxSize; i++)
            {
                for (int j = boxCol; j < boxCol + boxSize; j++)
                {
                    SudokuCell current = _board[i, j];
                    if (!current.IsSolved() && current.HasOption(option))
                    {
                        count++;
                        if (count > 1)
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }
    }
}
