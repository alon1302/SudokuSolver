using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SudokuBoard : ICloneable
{
    private int _size;
    private int _rowSize;
    private SudokuCell[,] _board;

    public SudokuBoard(string board_str)
    {
        _size = board_str.Length;
        _rowSize = (int)Math.Sqrt(_size);
        _board = new SudokuCell[_rowSize,_rowSize];
        for (int i = 0; i < _rowSize; i++)
        {
            for (int j = 0; j < _rowSize; j++)
            {
                char currValue = board_str[i * _rowSize + j];
                _board[i, j] = new SudokuCell(currValue, _rowSize);
            }
        }
        FixAllOptions();
    }

    public SudokuBoard()
    { 
    }

    public SudokuCell this[int row, int col]
    {
        get => _board[row, col];
        set => _board[row, col] = value;
    }

    public int SingleRowSize
    {
        get { return _rowSize; }
    }

    public string getBoardStr()
    {
        string board = "";
        for (int i = 0; i < _rowSize; i++)
        {
            for (int j = 0; j < _rowSize; j++)
            {
                board += _board[i, j].Value;
            }
        }
        return board;
    }

    public void RemoveOptionFromRow(char option, int row)
    {
        for (int col = 0; col < _rowSize; col++)
        {
            SudokuCell current = _board[row, col];
            if (!current.IsSolved())
            {
                current.RemoveOption(option);
            }
        }
    }
    public void RemoveOptionFromColumn(char option, int col)
    {
        for (int row = 0; row < _rowSize; row++)
        {
            SudokuCell current = _board[row, col];
            if (!current.IsSolved())
            {
                current.RemoveOption(option);
            }
        }
    }

    public void RemoveOptionFromBox(char option, int row, int col)
    {
        int boxSize = (int)Math.Sqrt(_rowSize);
        int boxRow = row - (row % boxSize);
        int boxCol = col - (col % boxSize);
        for (int i = boxRow; i < boxRow + boxSize; i++)
        {
            for (int j = boxCol; j < boxCol + boxSize; j++)
            {
                SudokuCell current = _board[i, j];
                if (!current.IsSolved())
                {
                    current.RemoveOption(option);
                }
            }
        }
    }

    public void RemoveOption(char ch, int row, int col)
    {
        RemoveOptionFromRow(ch, row);
        RemoveOptionFromColumn(ch, col);
        RemoveOptionFromBox(ch, row, col);
    }

    public void FixAllOptions()
    {
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                SudokuCell current = _board[row, col];
                if (current.IsSolved())
                {
                    RemoveOption(current.Value, row, col);
                }
            }
        }
    }

    public object Clone()
    {
        SudokuBoard ClonedBoard = new SudokuBoard();
        ClonedBoard._size = this._size;
        ClonedBoard._rowSize = this._rowSize;
        ClonedBoard._board = new SudokuCell[_rowSize, _rowSize];
        for (int i = 0; i < _rowSize; i++)
        {
            for (int j = 0; j < _rowSize; j++)
            {
                ClonedBoard[i, j] = (SudokuCell)this._board[i, j].Clone();
            }
        }
        return ClonedBoard;
    }
}

