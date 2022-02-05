using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Validators;
using Sudoku.exceptions;
using System;
using System.Collections.Generic;

namespace SudokuTest
{
    /// <summary>
    /// this test class test the input validation in the Sudoku Solver
    /// it creates dictionary of invalid boards before solution and expected exception message pairs
    /// and checks for every pair if the validator throws the expected exception for the board
    /// </summary>
    [TestClass]
    public class ValidationTest
    {
        // board with size of 0
        Tuple<string, string> boardSize0 = new Tuple<string, string>("", "the board size can't be 0\n");
        // board with invalid size
        Tuple<string, string> invalidBoardSize = new Tuple<string, string>("00001100", "invalid board size\n");
        // board that contains invalid character (*)
        Tuple<string, string> invalidChar = new Tuple<string, string>("00600000797000004050000080*000700500400003170050008006000301002000805000603902000", "the char * can't be part of this size of sudoku board\n");
        // board that contains two same characters (6) in the same row (0)
        Tuple<string, string> twiceInRow = new Tuple<string, string>("006006007970000040500000800000700500400003170050008006000301002000805000603902000", "the char 6 is appear more than once in a single row\n");
        // board that contains two same characters (6) in the same column (2)
        Tuple<string, string> twiceInCol = new Tuple<string, string>("006000007970000040500000800000700500406003170050008006000301002000805000603902000", "the char 6 is appear more than once in a single column\n");
        // board that contains two same characters (6) in the same box (5)
        Tuple<string, string> twiceInbox = new Tuple<string, string>("006000007970000040500000800000700560400003170050008006000301002000805000603902000", "the char 6 is appear more than once in a single box\n");

        List<Tuple<string, string>> boardsExceptionsPairs; // list of tuples of invalid boards and exception message

        InputValidator inputValidator; // the input validator

        /// <summary>
        /// function that iterate over the list of boards and expected exception message for them
        /// and checks for each of them if the exception matching to the expected one
        /// </summary>
        [TestMethod]
        public void InputValidationTests()
        {
            CreateBoardsExceptionPairs();
            foreach (Tuple<string, string> boardException in boardsExceptionsPairs)
            {
                inputValidator = new InputValidator(boardException.Item1);
                try
                {
                    inputValidator.Validate();
                }
                catch (Exception e)
                {
                    if (e is InvalidBoardException || e is InvalidBoardSizeException || e is InvalidCharacterException)
                    {
                        StringAssert.Contains(e.Message, boardException.Item2);
                        continue;
                    }
                    else
                    {
                        Assert.Fail("The expected exception was not thrown.");
                    }
                }
                Assert.Fail("The expected exception was not thrown.");
            }
        }

        /// <summary>
        /// function that insert into the list tuples of invalid boards
        /// and the expected exception message that supposed to thrown 
        /// </summary>
        private void CreateBoardsExceptionPairs()
        {
            boardsExceptionsPairs = new List<Tuple<string, string>>();
            boardsExceptionsPairs.Add(boardSize0);
            boardsExceptionsPairs.Add(invalidBoardSize);
            boardsExceptionsPairs.Add(invalidChar);
            boardsExceptionsPairs.Add(twiceInRow);
            boardsExceptionsPairs.Add(twiceInCol);
            boardsExceptionsPairs.Add(twiceInbox);
        }
    }
}
