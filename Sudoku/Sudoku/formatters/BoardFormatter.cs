using System;
using System.Collections.Generic;
using System.Text;

class BoardFormatter : IFormatter
{
    const int CELL_SIZE = 3;
    public string Format(string input)
    {
        SudokuBoard printBoard = new SudokuBoard(input);
        string formattedBoard = "";
        //for (int i = 0; i < printBoard.getSingleRowSize() * CELL_SIZE; i++)
        //{
        //    formattedBoard += "━";
        //}

        for (int i = 0; i < printBoard.getSingleRowSize(); i++)
        {
            for (int j = 0; j < printBoard.getSingleRowSize(); j++)
            {
                formattedBoard += printBoard[i, j] + '\t';
            }
            formattedBoard += '\n';
        }

        return formattedBoard;
    }
}
