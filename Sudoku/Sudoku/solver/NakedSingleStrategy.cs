using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class NakedSingleStrategy : IStrategy
{
    private SudokuBoard _board;

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
                    if (current.NumOfOptions == 1)
                    {
                        count++;
                        char lastOption = current.GetTheOnlyOption();
                        current.Value = lastOption;
                        _board.RemoveOptionFromCellRegions(lastOption, i, j);
                        i = 0;
                        j = 0;
                    }
                }
            }
        }
        return count > 0;
    }
}

