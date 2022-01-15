using System;
using System.Collections.Generic;
using System.Text;
class Solver
{
    private SudokuBoard _board;
    private SudokuBoard _solution;
    private int _boardSize;
    private SudokuValidator _validator;

    public Solver(SudokuBoard board , SudokuValidator validator)
    {
        this._board = board;
        this._validator = validator;
        this._boardSize = board.getSingleRowSize();
    }

    private bool Solve()
    {
        for (int row = 0; row < _boardSize; row++)
        {
            for (int col = 0; col < _boardSize; col++)
            {
                if (_solution[row, col] == '0')
                {
                    for (char charToTry = '1'; charToTry <= '0' + _boardSize; charToTry++)
                    {
                        if (_validator.IsValidPlace(charToTry, row, col))
                        {
                            _solution[row, col] = charToTry;
                            if (Solve())
                            {
                                return true;
                            }
                            _solution[row, col] = '0';
                        }
                    }
                    return false;
                }
            }
        }
        return true;
    }

    public SudokuBoard GetSolution()
    {
        if (_solution == null)
        {
            _solution = new SudokuBoard(_board);
            if (!Solve())
            {
                throw new Exception(); // TODO custom exception - unsolveable board
            }

        }
        return _solution;
    }
}
