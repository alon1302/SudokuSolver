using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.model
{
    public class SudokuCell : ICloneable
    {
        private const char EMPTY_CELL = '0'; // value of an empty cell

        // ------------------------PRIVATE FIELDS----------------------------
        private char _value; // the value of the cell
        private ISet<char> _options; // set of options that can be in an empty cell
        private int _boxIndex; // the box that this cell belong to
                               // ------------------------------------------------------------------

        // ------------------------Constructors------------------------------
        /// <summary>
        /// Constructor that receives value and the size of a row in the board
        /// as well as the location in the board
        /// the function set the value for the cell and initiates its options if its empty
        /// and set it's box index on the board by simple calculation
        /// </summary>
        /// <param name="value">the value of the cell</param>
        /// <param name="boardRowSize">the size of row in the board</param>
        /// <param name="row">row index</param>
        /// <param name="col">column index</param>
        public SudokuCell(char value, int boardRowSize, int row, int col)
        {
            _value = value;
            _options = new HashSet<char>();
            if (_value == EMPTY_CELL)
            {
                for (char ch = '1'; ch <= EMPTY_CELL + boardRowSize; ch++)
                {
                    _options.Add(ch);
                }
            }
            int boxSize = (int)Math.Sqrt(boardRowSize);
            _boxIndex = boxSize * (row / boxSize) + col / boxSize;
        }

        /// <summary>
        /// private Constructor with only value parameter for the Clone method
        /// </summary>
        /// <param name="value">the value of the cell</param>
        private SudokuCell(char value)
        {
            _value = value;
        }
        // ------------------------------------------------------------------

        // ------------------------PUBLIC PROPERTIES-------------------------
        /// <summary>
        /// Property for get and set the _value private field
        /// </summary>
        public char Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Proprety for get the amount of options that can be in this cell
        /// </summary>
        public int NumOfOptions
        {
            get { return _options.Count; }
        }

        /// <summary>
        /// Property for get reference to the _option private field 
        /// </summary>
        public ISet<char> Options
        {
            get { return _options; }
        }

        /// <summary>
        /// Proprety for get the box index of this cell
        /// </summary>
        public int BoxIndex
        {
            get { return _boxIndex; }
        }
        // ------------------------------------------------------------------

        // ------------------------PUBLIC METHODS----------------------------
        /// <summary>
        /// function that receives an option and returns true is the option is exist in the _option field
        /// or false if not
        /// </summary>
        /// <param name="option">the option to check</param>
        /// <returns>true if option exist in _options or false otherwise</returns>
        public bool HasOption(char option)
        {
            return _options.Contains(option);
        }

        /// <summary>
        /// function that receives an option and removes it from the _options
        /// it doesn't metter if the option is exist or not because the 
        /// HashSet.Romove() function remove only if exist
        /// </summary>
        /// <param name="option">the option to remove</param>
        public void RemoveOption(char option)
        {
            _options.Remove(option);
        }

        /// <summary>
        /// function that returns the "first" item in the set
        /// this function called only if the _options set contains just one value
        /// </summary>
        /// <returns>the single value in the _options set</returns>
        public char GetTheOnlyOption()
        {
            return _options.ToArray()[0];
        }

        /// <summary>
        /// function that reeturn true if this cell is already solved
        /// or false if the cell is empty cell
        /// </summary>
        /// <returns>true is cell is solved or false otherwise</returns>
        public bool IsSolved()
        {
            return _value != EMPTY_CELL;
        }
        // -------------------------------------------------------------------

        // ------------------------OVERRIDES----------------------------------
        /// <summary>
        /// override to the object.Clone function in order to create full deep copy of this cell
        /// </summary>
        /// <returns>new object that is the same cell by value</returns>
        public object Clone()
        {
            SudokuCell sudokuCell = new SudokuCell(this._value); // creat new cell with the same value
            sudokuCell._options = new HashSet<char>(this._options); // transfer all the _options set to new cell
            sudokuCell._boxIndex = this._boxIndex;
            return sudokuCell;
        }
        // -------------------------------------------------------------------
    }
}

