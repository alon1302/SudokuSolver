using System;

class BoardValidator : Ivalidator
{
    private SudokuBoard _board;

    public BoardValidator(SudokuBoard board)
    {
        _board = board;
    }

    public bool IsCharAppearOnceInRow(char ch, int row)
    {
        int count = 0;
        for (int col = 0; col < _board.SingleRowSize; col++)
        {
            if (ch == _board[row,col].Value)
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

    public bool IsCharAppearOnceInCol(char ch, int col)
    {
        int count = 0;
        for (int row = 0; row < _board.SingleRowSize; row++)
        {
            if (ch == _board[row, col].Value)
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

    public bool IsCharAppearOnceInBox(char ch, int row, int col)
    {
        int boxSize = (int)Math.Sqrt(_board.SingleRowSize);
        int boxRow = row - (row % boxSize);
        int boxCol = col - (col % boxSize);
        int count = 0;
        for (int i = boxRow; i < boxRow + boxSize; i++)
        {
            for (int j = boxCol; j < boxCol + boxSize; j++)
            {
                if (ch == _board[i,j].Value)
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

    private bool IsValidCharInPlace(char ch, int row, int col)
    {
        return IsCharAppearOnceInRow(ch, row) && IsCharAppearOnceInCol(ch, col) &&
               IsCharAppearOnceInBox(ch, row, col);
    }

    public bool Validate()
    {      
        for (int row = 0; row < _board.SingleRowSize; row++)
        {
            for (int col = 0; col < _board.SingleRowSize; col++)
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

    public bool IsValidPlace(char ch, int row, int col)
    {
        _board[row, col].Value = ch;
        if (IsValidCharInPlace(ch, row, col))
        {
            return true;
        }
        _board[row, col].Value = '0';
        return false;
    }
}

