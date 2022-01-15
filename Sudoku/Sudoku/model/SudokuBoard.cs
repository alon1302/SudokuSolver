using System;

class SudokuBoard
{

    private const char MIN_VALUE = '0';
    private string board_str;
    private int _size;
    private double _rowSize;

    public string BoardStr
    {
        get => board_str;
    }

    private char[,] _board;

    public SudokuBoard(string board_str)
    {
        _size = board_str.Length;
        _rowSize = Math.Sqrt(_size);
        if (Validate(board_str))
        {
            this._board = CreateCharMatrix(board_str);
        }
        else
        {
            throw new Exception(); // TODO Custom Exception
        }
    }
    public SudokuBoard(SudokuBoard b)
    {
        this._board = b.clone();
    }

    public char this[int row, int col]
    {
        get
        {
            return _board[row, col];
        }

        set
        {
            _board[row, col] = value;
        }
    }

    public int getSingleRowSize()
    {
        return _board.GetLength(0);
    }

    private char[,] CreateCharMatrix(string boardStr)
    {
        _size = (int)Math.Sqrt(boardStr.Length);
        char[,] matrix = new char[_size, _size];
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                matrix[i, j] = boardStr[i * _size + j];
            }
        }
        return matrix;
    }

    public bool IsCharAppearOnceInRow(char ch, int row)
    {
        int count = 0;
        for (int col = 0; col < _rowSize; col++)
        {
            if (ch == _board[row, col])
            {
                count++;
                if(count > 1)
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
        for (int row = 0; row < _rowSize; row++)
        {
            if (ch == _board[row, col])
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
        int boxSize = (int)Math.Sqrt(_board.GetLength(0));
        int boxRow = row - (row % boxSize);
        int boxCol = col - (col % boxSize);
        int count = 0;
        for (int i = boxRow; i < boxRow + boxSize; i++)
        {
            for (int j = boxCol; j < boxCol + boxSize; j++)
            {
                if (ch == _board[i,j])
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

    public bool IsValidPlace(char ch, int row, int col)
    {
        return IsCharAppearOnceInRow(ch, row) &&
               IsCharAppearOnceInCol(ch, col) &&
               IsCharAppearOnceInBox(ch, row, col);
    }

    public char[,] clone()
    {
        return (char[,])_board.Clone();
    }

    private bool ValiadateValuesPlacment(string allValues)
    {
        char[,] currentBoard = CreateCharMatrix(allValues);
        for (int i = 0; i < currentBoard.GetLength(0); i++)
        {
            for (int j = 0; j < currentBoard.GetLength(1); j++)
            {
                bool isCurrentPointValid = IsValidPlace(currentBoard[i, j], i, j);
                if (!isCurrentPointValid)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool ValidateValues(string allValues)
    {
        char maxValue = (char)(MIN_VALUE + (int)_rowSize);
        for (int i = 0; i < _size; i++)
        {
            char currentCell = allValues[i];
            if (currentCell < MIN_VALUE || currentCell > maxValue)
            {
                return false;
            }
        }
        return true;
    }

    private bool Validate(string board_input)
    {
        double inputSize = board_input.Length;
        double rowSize = Math.Sqrt(inputSize); 
        if (Math.Floor(rowSize) != rowSize)
        {
            return false;
        }

        return ValidateValues(board_input) && ValiadateValuesPlacment(board_input);
    }

    public override string ToString()
    {
        string board_text = "";
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                board_text += _board[i, j];
            }
        }
        return board_text;
    }
}
