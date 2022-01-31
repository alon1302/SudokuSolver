using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BoardValidator2
{
    SudokuBoard2 _board;

    public BoardValidator2(ref SudokuBoard2 board)
    {
        _board = board;
    }

    public bool isExistInRow(char ch, int row)
    {
        for (int col = 0; col < _board.SingleRowSize; col++)
        {
            if (ch == _board[row, col].Value)
            {
                return true;
            }
        }
        return false;
    }

    public bool isExistInCol(char ch, int col)
    {
        for (int row = 0; row < _board.SingleRowSize; row++)
        {
            if (ch == _board[row, col].Value)
            {
                return true;
            }
        }
        return false;
    }

    public bool isExistInBox(char ch, int row, int col)
    {
        int boxSize = (int)Math.Sqrt(_board.SingleRowSize);
        int boxRow = row - (row % boxSize);
        int boxCol = col - (col % boxSize);
        for (int i = boxRow; i < boxRow + boxSize; i++)
        {
            for (int j = boxCol; j < boxCol + boxSize; j++)
            {
                if (ch == _board[i, j].Value)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsValidPlace(char ch, int row, int col)
    {
        return !isExistInRow(ch, row) && !isExistInCol(ch, col) && !isExistInBox(ch, row, col);
    }
}

