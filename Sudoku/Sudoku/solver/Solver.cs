using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Solver
{
    private SudokuBoard _board;
    private ICollection<IStrategy> _strategies;

    public Solver(SudokuBoard board)
    {
        this._board = board;
        _strategies = new List<IStrategy>();
        _strategies.Add(new NakedSingleStrategy());
        _strategies.Add(new HiddenSingleStrategy());
    }

    //private bool Solve()
    //{
    //    SolveByStrategies();
    //    new ConsoleWriter(new BoardFormatter()).Write(_board.getBoardStr());
    //    BacktrackingStrategy backtracking = new BacktrackingStrategy();
    //    return backtracking.Solve(_board);
    //}

    private bool TrySolve()
    {
        SolveByStrategies(); // try to solve to current board with all the strategies
        BoardValidator _validator = new BoardValidator(_board);
        if (!_validator.Validate()) 
        {
            return false; // in case the board is not valid
        }
        if (_board.IsSolved())
        {
            return true; // in case the board is fully solved
        }
        int currentRow = -1, currentCol = -1;
        FindOptimalEmptyCellLocation(out currentRow, out currentCol); // find the current optimal cell (with the minimum number of options)
        if (currentRow == -1 || currentCol == -1)
        {
            return false; // if thete is no match cell
        }
        SudokuCell current = _board[currentRow, currentCol]; // get refrence to the optimal cell
        SudokuBoard clone = (SudokuBoard)_board.Clone(); // clone the current board to make guesses with no worries
        foreach (char charToTry in current.Options) // iterate over the options of the current optimal cell
        {
            current.Value = charToTry; // make a guess on the current board
            _board.RemoveOptionFromCellRegions(charToTry, currentRow, currentCol); // fix the options on the board according to the guess
            if (TrySolve())
            {
                return true; // in case the guess was successful
            }
            // else - bad guess
            _board = (SudokuBoard)clone.Clone(); // retrive refrence to the original board before the guess 
            current = _board[currentRow, currentCol]; // get refrence to the optimal cell (needed because of the clone)
        }
        return false; // can't solve the current board
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
        for (int i = 0; i < _board.RowSize; i++)
        {
            for (int j = 0; j < _board.RowSize; j++)
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
            throw new UnsolvableBoardException("Sorry, This Board is Unsolvable");
        }
        return _board;
    }
}

