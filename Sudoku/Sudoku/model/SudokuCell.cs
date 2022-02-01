using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SudokuCell : ICloneable
{
    private char _value;
    private HashSet<char> _options;

    public SudokuCell(char value, int boardSize)
    {
        _value = value;
        _options = new HashSet<char>();
        if (_value == '0')
        {
            for (char ch = '1'; ch <= '0' + boardSize; ch++)
            {
                if (ch != value)
                {
                    _options.Add(ch);
                }
            }
        }
    }

    public SudokuCell(char value)
    {
        _value = value;
    }

    public char Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public int NumOfOptions
    {
        get {return _options.Count;}
    }

    public HashSet<char> GetOptions()
    {
        return _options;
    }

    public void RemoveOption(char ch)
    {
        _options.Remove(ch);
    }

    public void SetValue(char ch)
    {
        _value = ch;
        //_options = null;
    }

    public char GetOption()
    {
        char ch = '0';
        foreach (char item in _options)
        {
            ch = item;
        }
        return ch;
    }

    public bool IsSolved()
    {
        return _value != '0';
    }

    public object Clone()
    {
        SudokuCell sudokuCell = new SudokuCell(this._value);
        sudokuCell._options = new HashSet<char>(this._options);
        return sudokuCell;
    }
}

