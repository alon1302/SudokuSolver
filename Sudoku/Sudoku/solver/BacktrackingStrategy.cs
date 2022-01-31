using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BacktrackingStrategy : IStrategy
{
    private SudokuBoard2 _board;
    private BoardValidator2 _validator;
    public BacktrackingStrategy(ref SudokuBoard2 board)
    {
        this._board = board;
        _validator = new BoardValidator2(ref board);
    }

    public bool Solve()
    {
        for (int row = 0; row < _board.SingleRowSize; row++)
        {
            for (int col = 0; col < _board.SingleRowSize; col++)
            {
                SudokuCell current = _board[row, col];
                if (!current.isSolved())
                {
                    foreach(char charToTry in current.GetOptions())
                    {
                        if (_validator.IsValidPlace(charToTry, row, col))
                        {
                            _board[row, col].Value = charToTry;
                            if (Solve())
                            {
                                return true;
                            }
                            _board[row, col].Value = '0';
                        }
                    }
                    return false;
                }
            }
        }
        return true;
    }
}

