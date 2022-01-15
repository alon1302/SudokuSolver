using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class InputValidator
{
    private const char MIN_VALUE = '0';
    private int _rowSize;
    private string _boardRepresentation;

    public InputValidator(string board)
    {
        this._boardRepresentation = board;
    }

    public void SetBoardRepresentation(string board) { _boardRepresentation = board; }

    private char GetChar(int row, int col)
    {
        return this._boardRepresentation[row * _rowSize + col];
    }

    public bool IsCharAppearOnceInRow(char ch, int row)
    {
        int count = 0;
        for (int col = 0; col < _rowSize; col++)
        {
            if (ch == GetChar(row,col))
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
        for (int row = 0; row < _rowSize; row++)
        {
            if (ch == GetChar(row,col))
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
        int boxSize = (int)Math.Sqrt(_rowSize);
        int boxRow = row - (row % boxSize);
        int boxCol = col - (col % boxSize);
        int count = 0;
        for (int i = boxRow; i < boxRow + boxSize; i++)
        {
            for (int j = boxCol; j < boxCol + boxSize; j++)
            {
                if (ch == GetChar(i, j))
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
        return ch == '0' || (IsCharAppearOnceInRow(ch, row) &&
               IsCharAppearOnceInCol(ch, col) &&
               IsCharAppearOnceInBox(ch, row, col));
    }

    private bool ValiadateValuesPlacment()
    {
        for (int i = 0; i < _rowSize; i++)
        {
            for (int j = 0; j < _rowSize; j++)
            {
                bool isCurrentPointValid = IsValidPlace(GetChar(i, j), i, j);
                if (!isCurrentPointValid)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool ValidateValues()
    {
        char maxValue = (char)(MIN_VALUE + (int)_rowSize);
        for (int i = 0; i < _rowSize * _rowSize; i++)
        {
            char currentCell = _boardRepresentation[i];
            if (currentCell < MIN_VALUE || currentCell > maxValue)
            {
                return false;
            }
        }
        return true;
    }

    public bool Validate()
    {
        double inputSize = _boardRepresentation.Length;
        double rowSize = Math.Sqrt(inputSize);
        if (Math.Floor(rowSize) != rowSize)
        {
            return false;
        }
        this._rowSize = (int)rowSize;
        return ValidateValues() && ValiadateValuesPlacment();
    }
}

