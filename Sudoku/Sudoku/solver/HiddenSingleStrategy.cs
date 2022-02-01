﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class HiddenSingleStrategy : IStrategy
{
    private SudokuBoard2 _board;
    private ConsoleWriter c = new ConsoleWriter(new BoardFormatter());
    public HiddenSingleStrategy(ref SudokuBoard2 board)
    {
        this._board = board;
    }
    public bool Solve()
    {
        int count = 0;
        for (int i = 0; i < _board.SingleRowSize; i++)
        {
            for (int j = 0; j < _board.SingleRowSize; j++)
            {
                SudokuCell current = _board[i, j];
                if (!current.isSolved())
                {
                    if (current.NumOfOptions == 0)
                    {
                        return false;
                    }
                    foreach (char item in current.GetOptions())
                    {
                        if (IsSingle(item, i, j))
                        {
                            count++;
                            //Console.WriteLine("find hidden single " + item + "    "+ i + "  "+ j);
                            current.SetValue(item);
                            _board.RemoveOption(item, i, j);
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
        for (int col = 0; col < _board.SingleRowSize; col++)
        {
            SudokuCell current = _board[row, col];
            if (!current.isSolved() && current.GetOptions().Contains(ch))
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
        for (int row = 0; row < _board.SingleRowSize; row++)
        {
            SudokuCell current = _board[row, col];
            if (!current.isSolved() && current.GetOptions().Contains(ch))
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
        int boxSize = (int)Math.Sqrt(_board.SingleRowSize);
        int boxRow = row - (row % boxSize);
        int boxCol = col - (col % boxSize);
        for (int i = boxRow; i < boxRow + boxSize; i++)
        {
            for (int j = boxCol; j < boxCol + boxSize; j++)
            {
                SudokuCell current = _board[i, j];
                if (!current.isSolved() && current.GetOptions().Contains(ch))
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
