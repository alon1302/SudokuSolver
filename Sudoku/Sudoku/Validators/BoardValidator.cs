using Sudoku.model;
using System;
using System.Collections.Generic;

namespace Sudoku.Validators
{
    public class BoardValidator : Ivalidator
    {
        private SudokuBoard _board; // sudoku board

        /// <summary>
        /// Constructor that receives the sudoku board
        /// </summary>
        /// <param name="board">the sudoku board</param>
        public BoardValidator(SudokuBoard board)
        {
            _board = board;
        }

        /// <summary>
        /// function that receives value of a cell and row index
        /// checks if the value apears more than once in this row
        /// </summary>
        /// <param name="value">the value of a cell</param>
        /// <param name="row">the row index of a cell</param>
        /// <returns>true if the value appears only once in this row or false otherwise</returns>
        public bool IsValueAppearOnceInRow(char value, int row)
        {
            int count = 0;
            for (int col = 0; col < _board.RowSize; col++)
            {
                if (value == _board[row, col].Value)
                {
                    count++;
                    if (count > 1)
                    {
                        return false;
                    }
                }
            }
            return count == 1;
        }

        /// <summary>
        /// function that receives value of a cell and column index
        /// checks if the value apears more than once in this column
        /// </summary>
        /// <param name="value">the value of a cell</param>
        /// <param name="col">the column index of a cell</param>
        /// <returns>true if the value appears only once in this column or false otherwise</returns>
        public bool IsValueAppearOnceInCol(char value, int col)
        {
            int count = 0;
            for (int row = 0; row < _board.RowSize; row++)
            {
                if (value == _board[row, col].Value)
                {
                    count++;
                    if (count > 1)
                    {
                        return false;
                    }
                }
            }
            return count == 1;
        }

        /// <summary>
        /// function that receives value of a cell and location indices
        /// checks if the value apears more than once in the cells box
        /// </summary>
        /// <param name="value">the value of a cell</param>
        /// <param name="row">the row index of a cell</param>
        /// <param name="col">the column index of a cell</param>
        /// <returns>true if the value appears only once in this box or false otherwise</returns>
        public bool IsValueAppearOnceInBox(char value, int row, int col)
        {
            int boxSize = (int)Math.Sqrt(_board.RowSize);
            int boxRow = row - (row % boxSize);
            int boxCol = col - (col % boxSize);
            int count = 0;
            for (int i = boxRow; i < boxRow + boxSize; i++)
            {
                for (int j = boxCol; j < boxCol + boxSize; j++)
                {
                    if (value == _board[i, j].Value)
                    {
                        count++;
                        if (count > 1)
                        {
                            return false;
                        }
                    }
                }
            }
            return count == 1;
        }

        /// <summary>
        /// function that receives value of a cell and location indices
        /// calls functions to check if the value apears more than once in all of the cell's regions
        /// </summary>
        /// <param name="value">the value of a cell</param>
        /// <param name="row">the row index of a cell</param>
        /// <param name="col">the column index of a cell</param>
        /// <returns>true if the value appears only once in each cell's region</returns>
        private bool IsValidCharInPlace(char value, int row, int col)
        {
            return IsValueAppearOnceInRow(value, row) && IsValueAppearOnceInCol(value, col) &&
                   IsValueAppearOnceInBox(value, row, col);
        }

        /// <summary>
        /// function that call functions to check for every cell if its value appears only one in each region
        /// returns true if all the cells answer this condition or false if not
        /// </summary>
        /// <returns>true if the board is completely valid</returns>
        public bool Validate()
        {
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
                    }
                    else if (!IsValidCharInPlace(current.Value, row, col))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

