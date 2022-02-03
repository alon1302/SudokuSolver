using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SudokuCell : ICloneable
{
    private const char EMPTY_CELL = '0'; // value of an empty cell
    private char _value; // the value of the cell
    private ISet<char> _options; // set of options that can be in an empty cell

    /// <summary>
    /// Constructor that receives value and the size of a row in the board
    /// the function set the value for the cell and initiates its options if its empty
    /// </summary>
    /// <param name="value">the value of the cell</param>
    /// <param name="boardRowSize">the size of row in the board</param>
    public SudokuCell(char value, int boardRowSize)
    {
        _value = value;
        _options = new HashSet<char>();
        if (_value == EMPTY_CELL)
        {
            for (char ch = '1'; ch <= EMPTY_CELL + boardRowSize; ch++)
            {
                if (ch != value)
                {
                    _options.Add(ch);
                }
            }
        }
    }

    /// <summary>
    /// private Constructor with only value parameter for the Clone method
    /// </summary>
    /// <param name="value">the value of the cell</param>
    private SudokuCell(char value)
    {
        _value = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public char Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public int NumOfOptions
    {
        get {return _options.Count;}
    }

    public ISet<char> Options
    {
        get { return _options; }
    }

    public bool HasOption(char option)
    {
        return _options.Contains(option);
    }

    public void RemoveOption(char ch)
    {
        _options.Remove(ch);
    }

    public char GetTheOnlyOption()
    {
        return _options.ToArray()[0];
    }

    public bool IsSolved()
    {
        return _value != EMPTY_CELL;
    }

    public object Clone()
    {
        SudokuCell sudokuCell = new SudokuCell(this._value);
        sudokuCell._options = new HashSet<char>(this._options);
        return sudokuCell;
    }
}

