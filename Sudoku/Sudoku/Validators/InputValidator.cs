using Sudoku.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Validators
{
    class InputValidator : Ivalidator
    {
        private const char EMPTY_CELL = '0'; // empty cell
        private int _rowSize; // size of one row of the board
        private string _boardRepresentation; // string that represent sudoku board

        /// <summary>
        /// Constructor thar receives string that represent sudoku board
        /// </summary>
        /// <param name="board">board representation</param>
        public InputValidator(string board)
        {
            this._boardRepresentation = board;
        }

        /// <summary>
        /// function that receives matrix location indicies
        /// returns the char in this location with simple calculation (row*size+col)  
        /// </summary>
        /// <param name="row">row index</param>
        /// <param name="col">col index</param>
        /// <returns></returns>
        private char GetChar(int row, int col)
        {
            return this._boardRepresentation[row * _rowSize + col];
        }

        /// <summary>
        /// function that receives a value and row index
        /// checks if the value apears more than once in this row
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="row">row index</param>
        /// <returns>true if the value appears only once in this row or false otherwise</returns>
        public bool IsValueAppearOnceInRow(char value, int row)
        {
            int count = 0;
            for (int col = 0; col < _rowSize; col++)
            {
                if (value == GetChar(row, col))
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
        /// function that receives a value and column index
        /// checks if the value apears more than once in this column
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="col">column index</param>
        /// <returns>true if the value appears only once in this column or false otherwise</returns>
        public bool IsValueAppearOnceInCol(char value, int col)
        {
            int count = 0;
            for (int row = 0; row < _rowSize; row++)
            {
                if (value == GetChar(row, col))
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
        /// function that receives value of a cell and matrix location indices
        /// checks if the value apears more than once in the box
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="row">row index</param>
        /// <param name="col">column index</param>
        /// <returns>true if the value appears only once in this box or false otherwise</returns>
        public bool IsValueAppearOnceInBox(char value, int row, int col)
        {
            int boxSize = (int)Math.Sqrt(_rowSize);
            int boxRow = row - (row % boxSize);
            int boxCol = col - (col % boxSize);
            int count = 0;
            for (int i = boxRow; i < boxRow + boxSize; i++)
            {
                for (int j = boxCol; j < boxCol + boxSize; j++)
                {
                    if (value == GetChar(i, j))
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
        /// function that checks for every spot in the board representation
        /// if it's value appears only once in each of it's regions
        /// </summary>
        /// <exception cref="InvalidBoardException">if any of the spots is invalid</exception>
        /// <returns>true if all the spots are valid</returns>
        private bool ValiadateValuesPlacment()
        {
            for (int row = 0; row < _rowSize; row++)
            {
                for (int col = 0; col < _rowSize; col++)
                {
                    string message = "";
                    char current = GetChar(row, col);
                    if (current == EMPTY_CELL)
                    {
                        continue;
                    }
                    if (!IsValueAppearOnceInRow(current, row))
                    {
                        message = $"the char {current} is appear more than once in a single row\n";
                    }
                    else if (!IsValueAppearOnceInCol(current, col))
                    {
                        message = $"the char {current} is appear more than once in a single column\n";
                    }
                    else if (!IsValueAppearOnceInBox(current, row, col))
                    {
                        message = $"the char {current} is appear more than once in a single box\n";
                    }
                    if (message != "")
                    {
                        throw new InvalidBoardException(message);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// function that checks if all the values are in the valid range
        /// from '0' to the board row size
        /// </summary>
        /// <exception cref="InvalidCharacterException">if one of the values are invalid</exception>
        /// <returns>true if all the values are in the valid range</returns>
        private bool ValidateValues()
        {
            char maxValue = (char)(EMPTY_CELL + (int)_rowSize);
            for (int i = 0; i < _rowSize * _rowSize; i++)
            {
                char current = _boardRepresentation[i];
                if (current < EMPTY_CELL || current > maxValue)
                {
                    throw new InvalidCharacterException($"the char {current} can't be part of this size of sudoku board\n");
                }
            }
            return true;
        }

        /// <summary>
        /// function that call functions to check if the board representation is a valid board
        /// returns true if it is or throws exception otherwise
        /// </summary>
        /// <exception cref="InvalidBoardSizeException">if the board size is not valid</exception>
        /// <returns>true is the board representation is valid</returns>
        public bool Validate()
        {
            double inputSize = _boardRepresentation.Length;
            if (inputSize == 0)
            {
                throw new InvalidBoardSizeException("the board can't be empty\n");
            }
            double rowSize = Math.Sqrt(inputSize);
            if (Math.Floor(rowSize) != rowSize)
            {
                throw new InvalidBoardSizeException("invalid board size\n");
            }
            this._rowSize = (int)rowSize;
            ValidateValues();
            ValiadateValuesPlacment();
            return true;
        }
    }
}

