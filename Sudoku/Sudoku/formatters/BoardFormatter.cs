using Sudoku.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.formatters
{
    class BoardFormatter : IFormatter
    {
        //--------------CONSTANT VALUES------------------
        private const char MIN_VALUE = '0';
        private const char NEW_LINE = '\n';
        private const char SPACE = ' ';
        private const string EMPTY_STRING = "";
        private const string WALL = "|";
        private const string DOUBLE_SPACE = "  ";
        private const string CELL_CEILING = "_____";
        private const string VOID_CELL = "     ";


        private int _rowSize; // row size of the board

        /// <summary>
        /// function that recieves a string that represent sudoku board
        /// and formats it to visible board like a table for the printing
        /// </summary>
        /// <param name="input">string that represent sudoku board</param>
        /// <returns>string that represent the visible board</returns>
        public string Format(string input)
        {
            SudokuBoard printBoard = new SudokuBoard(input);
            _rowSize = printBoard.RowSize;
            string formattedBoard = GetTableCeiling(); // first initialize the ceiling
            for (int i = 0; i < _rowSize; i++)
            {
                formattedBoard += GetRowOfCellsTop(); // for each row add row top
                formattedBoard += WALL; // start of the row
                for (int j = 0; j < _rowSize; j++)
                {
                    int currValue = printBoard[i, j].Value - MIN_VALUE;
                    formattedBoard += DOUBLE_SPACE;
                    formattedBoard += GetCurrentCellString(currValue);
                    formattedBoard += WALL;
                }
                formattedBoard += GetRowOfCellsBottom(); // for each row add row top
            }
            return formattedBoard;
        }

        private string GetCurrentCellString(int value)
        {
            string returnVal = "";
            if (value == 0)
            {
                returnVal += SPACE;
            }
            else
            {
                returnVal += value;
            }
            if (value > 9)
            {
                returnVal += SPACE;
            }
            else
            {
                returnVal += DOUBLE_SPACE;
            }
            return returnVal;
        }

        /// <summary>
        /// function that bulid row of '_' characters for the board ceiling
        /// </summary>
        /// <returns>the board ceiling</returns>
        private string GetTableCeiling()
        {
            string row = EMPTY_STRING;
            for (int i = 0; i < _rowSize; i++)
            {
                row += SPACE + CELL_CEILING;
            }
            row += NEW_LINE;
            return row;
        }

        /// <summary>
        /// function that bulid row of walls with spaces for the cells top
        /// </summary>
        /// <returns>row of cells top</returns>
        private string GetRowOfCellsTop()
        {
            string row = WALL;
            for (int i = 0; i < _rowSize; i++)
            {
                row += VOID_CELL + WALL;
            }
            row += NEW_LINE;
            return row;
        }

        /// <summary>
        /// function that bulid row of walls with ceilings for the cells bottom
        /// </summary>
        /// <returns>row of cells bottom</returns>
        private string GetRowOfCellsBottom()
        {
            string row = EMPTY_STRING;
            row += NEW_LINE + WALL;
            for (int i = 0; i < _rowSize; i++)
            {
                row += CELL_CEILING + WALL;
            }
            row += NEW_LINE;
            return row;
        }
    }
}
