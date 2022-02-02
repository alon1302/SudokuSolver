using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class HiddenSingleStrategy : IStrategy
{
    private SudokuBoard _board;
    private ConsoleWriter c = new ConsoleWriter(new BoardFormatter());
    public HiddenSingleStrategy()
    { 
    }
    public bool Solve(SudokuBoard board)
    {
        this._board = board;
        int count = 0;
        for (int i = 0; i < _board.RowSize; i++)
        {
            for (int j = 0; j < _board.RowSize; j++)
            {
                SudokuCell current = _board[i, j];
                if (!current.IsSolved())
                {
                    if (current.NumOfOptions == 0)
                    {
                        return false;
                    }
                    foreach (char item in current.Options)
                    {
                        if (IsSingle(item, i, j))
                        {
                            count++;
                            //Console.WriteLine("find hidden single " + item + "    "+ i + "  "+ j);
                            current.Value = item;
                            _board.RemoveOptionFromRegion(item, i, j);
                            //c.Write(_board.getBoardStr());
                            i = 0;
                            j = 0;
                            break;
                        }
                    }
                }
            }
        }
        return count > 0;
    }

    private bool IsSingle(char ch, int row, int col)
    {
        return IsSingleInRow(ch, row) || IsSingleInCol(ch, col) || IsSingleInBox(ch, row, col);
    }

    private bool IsSingleInRow(char ch, int row)
    {
        int count = 0;
        for (int col = 0; col < _board.RowSize; col++)
        {
            SudokuCell current = _board[row, col];
            if (!current.IsSolved() && current.HasOption(ch))
            {
                count++;
                if (count > 1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private bool IsSingleInCol(char ch, int col)
    {
        int count = 0;
        for (int row = 0; row < _board.RowSize; row++)
        {
            SudokuCell current = _board[row, col];
            if (!current.IsSolved() && current.HasOption(ch))
            {
                count++;
                if (count > 1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private bool IsSingleInBox(char ch, int row, int col)
    {
        int count = 0;
        int boxSize = (int)Math.Sqrt(_board.RowSize);
        int boxRow = row - (row % boxSize);
        int boxCol = col - (col % boxSize);
        for (int i = boxRow; i < boxRow + boxSize; i++)
        {
            for (int j = boxCol; j < boxCol + boxSize; j++)
            {
                SudokuCell current = _board[i, j];
                if (!current.IsSolved() && current.HasOption(ch))
                {
                    count++;
                    if (count > 1)
                    {
                        return false;
                    }
                }
            }
        }
        return false;
    }
}

