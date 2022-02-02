using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SudokuBoard : ICloneable
{
    private SudokuCell[,] _board;
    private int _rowSize;

    public SudokuBoard(string board_str)
    {
        _rowSize = (int)Math.Sqrt(board_str.Length);
        _board = new SudokuCell[_rowSize,_rowSize];
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                char currValue = board_str[row * _rowSize + col];
                _board[row, col] = new SudokuCell(currValue, _rowSize);
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

    public int RowSize
    {
        get { return _rowSize; }
    }

    public bool isSolved()
    {
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                if (!_board[row,col].IsSolved())
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void RemoveOptionFromRow(char option, int row)
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
    private void RemoveOptionFromColumn(char option, int col)
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
    private void RemoveOptionFromBox(char option, int row, int col)
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
    public void RemoveOptionFromRegion(char ch, int row, int col)
    {
        RemoveOptionFromRow(ch, row);
        RemoveOptionFromColumn(ch, col);
        RemoveOptionFromBox(ch, row, col);
    }
    private void FixAllOptions()
    {
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                SudokuCell current = _board[row, col];
                if (current.IsSolved())
                {
                    RemoveOptionFromRegion(current.Value, row, col);
                }
            }
        }
    }

    public object Clone()
    {
        SudokuBoard ClonedBoard = new SudokuBoard();
        ClonedBoard._rowSize = this._rowSize;
        ClonedBoard._board = new SudokuCell[_rowSize, _rowSize];
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                ClonedBoard[row, col] = (SudokuCell)this._board[row, col].Clone();
            }
        }
        return ClonedBoard;
    }

    public override string ToString()
    {
        string board = "";
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                board += _board[row, col].Value;
            }
        }
        return board;
    }

}

