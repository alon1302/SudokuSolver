using System;
using System.Collections.Generic;
using System.Text;

class BoardFormatter : IFormatter
{
    const int CELL_SIZE = 3;
    public string Format(string input)
    {
        SudokuBoard printBoard = new SudokuBoard(input, new SudokuValidator());
        string formattedBoard = "";

        for (int i = 0; i < printBoard.getSingleRowSize(); i++)
        {
            for (int j = 0; j < printBoard.getSingleRowSize(); j++)
            {
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
