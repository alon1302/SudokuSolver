using System;
using System.Collections.Generic;
using System.Text;

class BoardFormatter : IFormatter
{
    private int _rowSize;

    public string Format(string input)
    {
        SudokuBoard printBoard = new SudokuBoard(input);
        _rowSize = printBoard.getSingleRowSize();
        string formattedBoard = "";

        for (int i = 0; i < _rowSize; i++)
        {
            formattedBoard += " _____";
        }
        formattedBoard += '\n';

        for (int i = 0; i < _rowSize; i++)
        {
            formattedBoard += GetRowOfCellsTop();
            formattedBoard += '|';
            for (int j = 0; j < _rowSize; j++)
            {
                formattedBoard += "  ";
                if (printBoard[i, j] == '0')
                {
                    formattedBoard += ' ';
                }
                else
                {
                    formattedBoard += printBoard[i, j];
                }
                formattedBoard += "  |";
            }
            formattedBoard += GetRowOfCellsBottom();
        }
        return formattedBoard;
    }

    private string GetRowOfCellsTop()
    {
        string row = "|";
        for (int i = 0; i < _rowSize; i++)
        {
            row += "     |";
        }
        row += '\n';
        return row;
    }

    private string GetRowOfCellsBottom()
    {
        string row = "\n|";
        for (int i = 0; i < _rowSize; i++)
        {
            row += "_____|";
        }
        row += '\n';
        return row;
    }
}
