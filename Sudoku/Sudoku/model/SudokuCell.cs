using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SudokuCell
{
    private char _value;
    private HashSet<char> _options;

    public SudokuCell(char value, int boardSize)
    {
        _value = value;
        for (char ch = '1'; ch <= '0' + boardSize; ch++)
        {
            if (ch != value)
            {
                _options.Add(ch);
            }
        }
    }

    public char Value
    {
        get { return _value; }
    }

    public void RemoveOption(char ch)
    {
        _options.Remove(ch);
    }

    public bool isSolved()
    {
        return _value != '0';
    }
}

