using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SudokuValidator
{
    private const char MIN_VALUE = '0';
    private int _rowSize;
    private string _boardRepresentation;

    public SudokuValidator()
    {
    }
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
        return IsCharAppearOnceInRow(ch, row) &&
               IsCharAppearOnceInCol(ch, col) &&
               IsCharAppearOnceInBox(ch, row, col);
    }

    private bool ValiadateValuesPlacment(string allValues)
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

    private bool ValidateValues(string allValues)
    {
        char maxValue = (char)(MIN_VALUE + (int)_rowSize);
        for (int i = 0; i < _rowSize * _rowSize; i++)
        {
            char currentCell = allValues[i];
            if (currentCell < MIN_VALUE || currentCell > maxValue)
            {
                return false;
            }
        }
        return true;
    }

    public bool Validate(string board_input)
    {
        this._boardRepresentation = board_input;
        double inputSize = board_input.Length;
        double rowSize = Math.Sqrt(inputSize);
        if (Math.Floor(rowSize) != rowSize)
        {
            return false;
        }
        this._rowSize = (int)rowSize;
        return ValidateValues(board_input) && ValiadateValuesPlacment(board_input);
    }
}

