using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Intersections is a famous strategy among all the sudoku solvers
/// Basic Definition:
/// if there is an empty cell that one of it's options exist only one more time in the row or the column
/// and both of the options belong to the same box.
/// when this situation heppens we can be sure that this option has to be inly in this row or column
/// so we remove this option from all other empty cells in the spicific box.
/// this strategy make quick and effective reduction of options in the sudoku board 
/// </summary>
class IntersectionsStrategy : IStrategy
{
    private SudokuBoard _board; // the sudoku board 

    /// <summary>
    /// function that receives sudoku board and try to solve it using the Intersections strategy
    /// when an option is an intersection in row or column
    /// and calls a function that remove this option from all the box except the spicific row or column
    /// returns true if there was an intersection found and the board has changed because of this
    /// </summary>
    /// <param name="board">the sudoku board</param>
    /// <returns>true if the function found one intersection and canges the board because of it</returns>
    public bool Solve(SudokuBoard board)
    {
        _board = board;
        for (int row = 0; row < _board.RowSize; row++)
        {
            for (int col = 0; col < _board.RowSize; col++)
            {
                SudokuCell current = _board[row, col];
                if (!current.IsSolved())
                {
                    if (current.NumOfOptions == 0)
                    {
                        return false;
                    }
                    foreach (char option in current.Options)
                    {
                        if (isIntersectionInRow(option, row)) 
                        {
                            // intersection was found in row
                            bool efectiveRemove = _board.RemoveOptionFromBoxExceptRow(option, row, col);
                            if (efectiveRemove)
                            {
                                // the board has changed As a result of the intersection
                                return true;
                            }
                        }
                        if (isIntersectionInCol(option, col))
                        {
                            // intersection was found in row
                            bool efectiveRemove = _board.RemoveOptionFromBoxExceptCol(option, row, col);
                            if (efectiveRemove)
                            {
                                // the board has changed As a result of the intersection
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// function that receives an option and row index 
    /// the function checks if this option apears more than once in the row
    /// and that all of the appearences happens in the same box
    /// return true if this state happens
    /// </summary>
    /// <param name="option">the option to check</param>
    /// <param name="row">row index</param>
    /// <returns>true if this option is an intersection in the row</returns>
    public bool isIntersectionInRow(char option, int row)
    {
        int count = 0;
        int boxIndexFound = -1;
        for (int col = 0; col < _board.RowSize; col++)
        {
            SudokuCell current = _board[row, col];
            if (!current.IsSolved())
            {
                if (current.HasOption(option))
                {
                    count++;
                    if (boxIndexFound == -1)
                    {
                        boxIndexFound = current.BoxIndex;
                    }
                    else
                    {
                        if (current.BoxIndex != boxIndexFound)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return count > 1;
    }

    /// <summary>
    /// function that receives an option and column index 
    /// the function checks if this option apears more than once in the column
    /// and that all of the appearences happens in the same box
    /// return true if this state happens
    /// </summary>
    /// <param name="option">the option to check</param>
    /// <param name="col">column index</param>
    /// <returns>true if this option is an intersection in the column</returns>
    public bool isIntersectionInCol(char option, int col)
    {
        int count = 0;
        int boxIndexFound = -1;
        for (int row = 0; row < _board.RowSize; row++)
        {
            SudokuCell current = _board[row, col];
            if (!current.IsSolved())
            {
                if (current.HasOption(option))
                {
                    count++;
                    if (boxIndexFound == -1)
                    {
                        boxIndexFound = current.BoxIndex;
                    }
                    else
                    {
                        if (current.BoxIndex != boxIndexFound)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return count > 1;
    }

}

