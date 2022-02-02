using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Solver
{
    private SudokuBoard _board;
    private List<IStrategy> _strategies;

    private BoardValidator _validator;

    public Solver(SudokuBoard board)
    {
        this._board = board;
        _strategies = new List<IStrategy>();
        _strategies.Add(new NakedSingleStrategy(ref _board));
        _strategies.Add(new HiddenSingleStrategy(ref _board));

        _validator = new BoardValidator(ref _board);
    }

    private bool Solve()
    {
        SolveByStrategies();
        new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
        BacktrackingStrategy backtracking = new BacktrackingStrategy(ref _board);
        return backtracking.Solve();
    }

    private bool TrySolve()
    {
        SolveByStrategies();
        if (!_validator.Validate())
        {
            new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
            return false;
        }
        if (_board.isSolved())
        {
            return true;
        }
        SudokuCell current = FindOptimalEmptyCell();
        SudokuBoard clone = (SudokuBoard)_board.Clone();
        if (current == null)
        {
            return false;
        }
        foreach (char charToTry in current.GetOptions())
        {
            current = FindOptimalEmptyCell();
            current.SetValue(charToTry);
            _board.FixAllOptions();
            new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
            if (TrySolve())
            {
                return true;
            }
            _board = (SudokuBoard)clone.Clone();
        }
        return false;
    }

    public void SolveByStrategies()
    {
        List<bool> solves;
        do
        {
            solves = new List<bool>();
            foreach (IStrategy strategy in _strategies)
            {
                solves.Add(strategy.Solve());
            }
        } while (solves.Contains(true));
    }

    private SudokuCell FindOptimalEmptyCell()
    {
        SudokuCell minOptions = null;
        for (int i = 0; i < _board.SingleRowSize; i++)
        {
            for (int j = 0; j < _board.SingleRowSize; j++)
            {
                SudokuCell current = _board[i, j];
                if (!current.IsSolved())
                {
                    if (minOptions == null || current.NumOfOptions < minOptions.NumOfOptions)
                    {
                        minOptions = current;
                    }
                }
            }
        }
        return minOptions;
    }

    public SudokuBoard GetSolution()
    {
        if (!Solve())
        {
            throw new Exception(); // TODO custom exception - unsolveable board
        }
        //if (!TrySolve())
        //{
        //    throw new Exception();
        //}
        return _board;
    }
}

