using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class NakedSingleStrategy : IStrategy
{
    private SudokuBoard2 _board;

    public NakedSingleStrategy(ref SudokuBoard2 board)
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
                    if (current.NumOfOptions == 1)
                    {
                        count++;
                        char lastOption = current.GetOption();
                        current.SetValue(lastOption);
                        _board.RemoveOption(lastOption, i, j);
                        //Console.WriteLine("find naked single " + current.Value + "  " + i + "  " + j);
                        //new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
                        i = 0;
                        j = 0;
                    }
                }
            }
        }
        return count > 0;
    }
}

