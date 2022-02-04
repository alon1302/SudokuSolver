using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// object that represent the full sudoku board
/// hold a matrix of SudokuCell objects
/// </summary>
class SudokuBoard : ICloneable
{
    // ------------------------PRIVATE FIELDS----------------------------
    private SudokuCell[,] _board; // matrix of sudoku cells
    private int _rowSize; // number of cells in each row
    // ------------------------------------------------------------------

    // ------------------------Constructors------------------------------
    /// <summary>
    /// Constractor that receives string that represent sudoku board
    /// create cells metrix and calls the cell constructor for each cell
    /// than it calls for function that initiates the options of each cell in the board
    /// </summary>
    /// <param name="board_str">string that represent sudoku board</param>
    public SudokuBoard(string board_str)
    {
        _rowSize = (int)Math.Sqrt(board_str.Length);
        _board = new SudokuCell[_rowSize,_rowSize];
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                char currValue = board_str[row * _rowSize + col];
                _board[row, col] = new SudokuCell(currValue, _rowSize, row, col);
            }
        }
        FixAllOptions(); // initiate the options of each cell in the board
    }

    /// <summary>
    /// private empty constructor for the Clone method
    /// </summary>
    private SudokuBoard()
    { 
    }
    // ------------------------------------------------------------------

    // ------------------------PUBLIC PROPERTIES-------------------------
    /// <summary>
    /// Indexer that recieves row and column indices
    /// and return reference to the cell in this location
    /// </summary>
    /// <param name="row">row index</param>
    /// <param name="col">column index</param>
    /// <returns></returns>
    public SudokuCell this[int row, int col]
    {
        get => _board[row, col];
        set => _board[row, col] = value;
    }

    /// <summary>
    /// Get Property for the _rowSize private field
    /// </summary>
    public int RowSize
    {
        get { return _rowSize; }
    }
    // ------------------------------------------------------------------

    // ------------------------PUBLIC METHODS----------------------------
    /// <summary>
    /// function that returns true if all the cells in the board are solved or false otherwise
    /// </summary>
    /// <returns>true if the board is solved or false otherwise</returns>
    public bool IsSolved()
    {
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                if (!_board[row,col].IsSolved())
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// function that receives option and cell location 
    /// the function calls little functions that remove the options from each region of that cell
    /// </summary>
    /// <param name="option">option to remove</param>
    /// <param name="row">row index</param>
    /// <param name="col">column index</param>
    public void RemoveOptionFromCellRegions(char option, int row, int col)
    {
        RemoveOptionFromRow(option, row);
        RemoveOptionFromColumn(option, col);
        RemoveOptionFromBox(option, row, col);
    }

    /// <summary>
    /// function that receives an option, column and row indices
    /// the function remove the option from all the cells in the box of this location
    /// except the cells in the row index
    /// the function returns true if the board has changed of false is not
    /// </summary>
    /// <param name="option">option to remove</param>
    /// <param name="row">row index</param>
    /// <param name="col">column index</param>
    /// <returns>true if the board has changed or false otherwise</returns>
    public bool RemoveOptionFromBoxExceptRow(char option, int row, int col)
    {
        bool removeOption = false;
        int boxSize = (int)Math.Sqrt(_rowSize);
        int boxRow = row - (row % boxSize);
        int boxCol = col - (col % boxSize);
        for (int i = boxRow; i < boxRow + boxSize; i++)
        {
            for (int j = boxCol; j < boxCol + boxSize; j++)
            {
                if (i != row)
                {
                    SudokuCell current = _board[i, j];
                    if (!current.IsSolved())
                    {
                        if (current.HasOption(option))
                        {
                            current.RemoveOption(option);
                            removeOption = true;
                        }
                    }
                }
            }
        }
        return removeOption;
    }

    /// <summary>
    /// function that receives an option, column and row indices
    /// the function remove the option from all the cells in the box of this location
    /// except the cells in the column index
    /// the function returns true if the board has changed of false is not
    /// </summary>
    /// <param name="option">option to remove</param>
    /// <param name="row">row index</param>
    /// <param name="col">column index</param>
    /// <returns>true if the board has changed or false otherwise</returns>
    public bool RemoveOptionFromBoxExceptCol(char option, int row, int col)
    {
        bool removeOption = false;
        int boxSize = (int)Math.Sqrt(_rowSize);
        int boxRow = row - (row % boxSize);
        int boxCol = col - (col % boxSize);
        for (int i = boxRow; i < boxRow + boxSize; i++)
        {
            for (int j = boxCol; j < boxCol + boxSize; j++)
            {
                if (j != col)
                {
                    SudokuCell current = _board[i, j];
                    if (!current.IsSolved())
                    {
                        if (current.HasOption(option))
                        {
                            current.RemoveOption(option);
                            removeOption = true;
                        }
                    }
                }
            }
        }
        return removeOption;
    }
    // ------------------------------------------------------------------

    // ------------------------PRIVATE METHODS---------------------------
    /// <summary>
    /// function that receives option and row index
    /// the function remove the option from all the cells in this row
    /// </summary>
    /// <param name="option">option to remove</param>
    /// <param name="row">row index</param>
    private void RemoveOptionFromRow(char option, int row)
    {
        for (int col = 0; col < _rowSize; col++)
        {
            SudokuCell current = _board[row, col];
            if (!current.IsSolved())
            {
                current.RemoveOption(option);
            }
        }
    }

    /// <summary>
    /// function that receives option and column index
    /// the function remove the option from all the cells in this column
    /// </summary>
    /// <param name="option">option to remove</param>
    /// <param name="col">column index</param>
    private void RemoveOptionFromColumn(char option, int col)
    {
        for (int row = 0; row < _rowSize; row++)
        {
            SudokuCell current = _board[row, col];
            if (!current.IsSolved())
            {
                current.RemoveOption(option);
            }
        }
    }

    /// <summary>
    /// function that receives an option, column and row indices
    /// the function remove the option from all the cells in the box of this location
    /// </summary>
    /// <param name="option">option to remove</param>
    /// <param name="row">row index</param>
    /// <param name="col">column index</param>
    private void RemoveOptionFromBox(char option, int row, int col)
    {
        int boxSize = (int)Math.Sqrt(_rowSize);
        int boxRow = row - (row % boxSize);
        int boxCol = col - (col % boxSize);
        for (int i = boxRow; i < boxRow + boxSize; i++)
        {
            for (int j = boxCol; j < boxCol + boxSize; j++)
            {
                SudokuCell current = _board[i, j];
                if (!current.IsSolved())
                {
                    current.RemoveOption(option);
                }
            }
        }
    }

    /// <summary>
    /// function that initiate the options for each cell in the board 
    /// according to the values of the cells in overlapping regions
    /// </summary>
    private void FixAllOptions()
    {
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                SudokuCell current = _board[row, col];
                if (current.IsSolved())
                {
                    RemoveOptionFromCellRegions(current.Value, row, col);
                }
            }
        }
    }
    // ------------------------------------------------------------------

    // ------------------------OVERRIDES----------------------------------
    /// <summary>
    /// override to the object.Clone function in order to create full deep copy of the board
    /// </summary>
    /// <returns>new object that is the same board by value</returns>
    public object Clone()
    {
        SudokuBoard ClonedBoard = new SudokuBoard();
        ClonedBoard._rowSize = this._rowSize;
        ClonedBoard._board = new SudokuCell[_rowSize, _rowSize];
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                ClonedBoard[row, col] = (SudokuCell)this._board[row, col].Clone();
            }
        }
        return ClonedBoard;
    }

    /// <summary>
    /// override to the object.ToString() function in order to return string that represent the board
    /// </summary>
    /// <returns>string that represent this sudoku board</returns>
    public override string ToString()
    {
        string board = "";
        for (int row = 0; row < _rowSize; row++)
        {
            for (int col = 0; col < _rowSize; col++)
            {
                board += _board[row, col].Value;
            }
        }
        return board;
    }
    // ------------------------------------------------------------------

}

