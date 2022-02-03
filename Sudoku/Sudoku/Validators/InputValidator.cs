using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class InputValidator : Ivalidator
{
    private const char MIN_VALUE = '0';
    private int _rowSize;
    private string _boardRepresentation;

    public InputValidator(string board)
    {
        this._boardRepresentation = board;
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

    private bool ValiadateValuesPlacment()
    {
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                string message = "";
                char current = GetChar(row, col);
                if (current == '0')
                {
                    continue;
                }
                if (!IsCharAppearOnceInRow(current, row))
                {
                    message = "the char " + current + " is appear more than once in a single row\n";
                }
                if (!IsCharAppearOnceInCol(current, col))
                {
                    message = "the char " + current + " is appear more than once in a single column " + (col + 1) + "\n";
                }
                if (!IsCharAppearOnceInBox(current, row, col))
                {
                    message = "the char " + current + " is appear more than once in a single box\n";
                }
                if (message != "")
                {
                    throw new InvalidBoardException(message);
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
                throw new InvalidCharacterException("the char '" + currentCell + "' can't be part of this size of sudoku board\n");
            }
        }
        return true;
    }

    public bool Validate()
    {
        double inputSize = _boardRepresentation.Length;
        if (inputSize == 0)
        {
            throw new InvalidBoardSizeException("the board can't be empty\n");
        }
        double rowSize = Math.Sqrt(inputSize);
        if (Math.Floor(rowSize) != rowSize)
        {
            throw new InvalidBoardSizeException("invalid board size\n");
        }
        this._rowSize = (int)rowSize;
        ValidateValues();
        ValiadateValuesPlacment();
        return true;
    }
}

