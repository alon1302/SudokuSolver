using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Solver
{
    private SudokuBoard _board;
    private List<IStrategy> _strategies;

    public Solver(SudokuBoard board)
    {
        this._board = board;
        _strategies = new List<IStrategy>();
        _strategies.Add(new NakedSingleStrategy());
        _strategies.Add(new HiddenSingleStrategy());

    }

    private bool Solve()
    {
        SolveByStrategies();
        new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
        BacktrackingStrategy backtracking = new BacktrackingStrategy();
        return backtracking.Solve(_board);
    }

    private bool TrySolve()
    {
        SolveByStrategies();
        //new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
        BoardValidator _validator = new BoardValidator(_board);
        if (!_validator.Validate())
        {
            //new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
            return false;
        }
        if (_board.isSolved())
        {
            return true;
        }
        //new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
        int currentRow = -1, currentCol = -1;
        FindOptimalEmptyCellLocation(out currentRow, out currentCol);
        if (currentRow == -1 || currentCol == -1)
        {
            //new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
            return false;
        }
        SudokuCell current = _board[currentRow, currentCol];
        SudokuBoard clone = (SudokuBoard)_board.Clone();
        
        foreach (char charToTry in current.GetOptions())
        {
            current.Value = charToTry;
            _board.RemoveOption(charToTry, currentRow, currentCol);
            //Console.WriteLine("making guess" + charToTry + " in [" + currentRow + "," + currentCol + "]");
            //new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
            if (TrySolve())
            {
                return true;
            }
            _board = (SudokuBoard)clone.Clone();
            current = _board[currentRow, currentCol];
        }
        //new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
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
                solves.Add(strategy.Solve(_board));
            }
        } while (solves.Contains(true));
    }

    private void FindOptimalEmptyCellLocation(out int row, out int col)
    {
        int minOptions = -1;
        row = -1;
        col = -1;
        for (int i = 0; i < _board.SingleRowSize; i++)
        {
            for (int j = 0; j < _board.SingleRowSize; j++)
            {
                SudokuCell current = _board[i, j];
                if (!current.IsSolved())
                {
                    if (minOptions == -1 || current.NumOfOptions < minOptions)
                    {
                        minOptions = current.NumOfOptions;
                        row = i;
                        col = j;
                    }
                }
            }
        }
    }

    public SudokuBoard GetSolution()
    {
        //if (!Solve())
        //{
        //    throw new Exception(); // TODO custom exception - unsolveable board
        //}
        if (!TrySolve())
        {
            throw new Exception();
        }
        return _board;
    }
}

