using System;

class SudokuBoard
{
    private string board_str;
    private int _size;
    private double _rowSize;

    public string BoardStr
    {
        get => board_str;
    }

    private char[,] _board;

    public SudokuBoard(string board_str, SudokuValidator sudokuValidator)
    {
        _size = board_str.Length;
        _rowSize = Math.Sqrt(_size);
        if (sudokuValidator.Validate(board_str))
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
        this._board = (char[,])b._board.Clone();
    }

    public char this[int row, int col]
    {
        get => _board[row, col];
        set =>_board[row, col] = value;
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
