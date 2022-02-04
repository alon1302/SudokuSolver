using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Solver
{
    private SudokuBoard _board; // sudoku board
    private ICollection<IStrategy> _strategies; // collection of the strategies
    
    /// <summary>
    /// Constructor that receives the sudoku board ???and some strategies
    /// </summary>
    /// <param name="board"></param>
    public Solver(SudokuBoard board /*params???*/)
    {
        this._board = board;
        _strategies = new List<IStrategy>();
        // add all the strategies to the list
        _strategies.Add(new NakedSingleStrategy()); 
        _strategies.Add(new HiddenSingleStrategy());
        _strategies.Add(new IntersectionsStrategy());
    }

    /// <summary>
    /// the most important function of the project
    /// the function tries to solve the board with the following algorithm:
    /// * calls to function that solve the board by the strategies until it can't continue 
    /// * if the current board is invalid -> return false
    /// * if the current board is fully solved -> return true
    /// * calls to function that returns the location of the optimal empty cell
    ///   (optimal cell - the empty cell with the minimum options in the board)
    /// * if there is no optimal cell (no empty cells or something wrong with the options) -> return false
    /// * make a deep copy of the current board in order to make guesses with no worries
    /// * iterate over the options of the optimal cell
    ///   for each option:
    ///     - make a guess on the current board and update all the options 
    ///     - calls the function again (recursion)
    ///         + if the recursion returns true -> return true
    ///     - make a deep copy of the clone board to the current board (because the guess was bad)
    ///     - get new reference to the optimal cell (because the current board has changed by value)
    /// * return false
    /// </summary>
    /// <returns>true if the board was solved</returns>
    private bool Solve()
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
            if (Solve())
            {
                return true; // in case the guess was successful
            }
            // else - bad guess
            _board = (SudokuBoard)clone.Clone(); // retrive refrence to the original board before the guess 
            current = _board[currentRow, currentCol]; // get refrence to the optimal cell (needed because of the clone)
        }
        return false; // can't solve the current board
    }

    /// <summary>
    /// function that make a circular iteration over all the strategies and tries to solve by every one of them
    /// while any of them return true (means that something in the board has changed) 
    /// when the function finish its run we know that no strategy can help to solve the current board
    /// so we have to make a guess
    /// </summary>
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

    /// <summary>
    /// function that find the location of the optimal empty cell
    /// optimal - the cell that has the minimum number of options
    /// </summary>
    /// <param name="row">out parameter to "return" the row index</param>
    /// <param name="col">out parameter to "return" the column index</param>
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

    /// <summary>
    /// function that calls to the solve function
    /// returns the solution if the solve function returns true
    /// </summary>
    /// <exception cref="UnsolvableBoardException">if the solve function returns false</exception>
    /// <returns>the solution sudoku board</returns>
    public SudokuBoard GetSolution()
    {
        if (!Solve())
        {
            throw new UnsolvableBoardException("Sorry, This Board is Unsolvable");
        }
        return _board;
    }
}

