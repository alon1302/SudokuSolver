using System;
using System.Collections.Generic;
using System.Text;

class BoardFormatter : IFormatter
{
    const int CELL_SIZE = 3;
    private int _rowSize;

    public string Format(string input)
    {
        SudokuBoard printBoard = new SudokuBoard(input);
        _rowSize = printBoard.getSingleRowSize();
        string formattedBoard = "";

        for (int i = 0; i < _rowSize * CELL_SIZE; i++)
        {
            formattedBoard += '_';
        }
        formattedBoard += '\n';

        for (int i = 0; i < _rowSize; i++)
        {
            formattedBoard += GetRowOfVerticalLines();
            for (int j = 0; j < _rowSize; j++)
            {
                formattedBoard += ' ';
                if (printBoard[i, j] == '0')
                {
                    formattedBoard += ' ';
                }
                else
                {
                    formattedBoard += printBoard[i, j];
                }
                formattedBoard += '\t';
            }
            formattedBoard += '\n';
        }

        return formattedBoard;
    }
}
