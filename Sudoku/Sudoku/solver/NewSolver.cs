using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class NewSolver
{
    private SudokuBoard2 _board;
    private List<IStrategy> _strategies;

    public NewSolver(SudokuBoard2 board)
    {
        this._board = board;
        _strategies = new List<IStrategy>();
        _strategies.Add(new NakedSingleStrategy(ref _board));
        _strategies.Add(new HiddenSingleStrategy(ref _board));
    }

    private bool Solve()
    {
        SolveByStrategies();
        BacktrackingStrategy backtracking = new BacktrackingStrategy(ref _board);
        return backtracking.Solve();
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

    public SudokuBoard2 GetSolution()
    {
        if (!Solve())
        {
            throw new Exception(); // TODO custom exception - unsolveable board
        }
        return _board;
    }
}

