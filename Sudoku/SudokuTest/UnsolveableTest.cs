using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Sudoku.model;
using Sudoku.solver;
using Sudoku.exceptions;

namespace SudokuTest
{
    /// <summary>
    /// this test class test unsolveable boards in the sudoku solver
    /// it creates list of unsolveable boards 
    /// and checks for every board if the solver throws the expected exception for the board
    /// </summary>
    [TestClass]
    public class UnsolveableTest
    {
        const string unsolveableExceptionMessage = "Sorry, This Board is Unsolvable"; // the expected exception
        List<string> unsolveableBoards; // list of unsolveable boards

        SudokuBoard board; // current board
        Solver solver; // current solver

        /// <summary>
        /// function that iterates over a list of unsolveable boards
        /// and checks if the solve function throws the UnsolveableBoardException
        /// </summary>
        [TestMethod]
        public void TestUnsolveableBoards()
        {
            CreateUnsolveablesList();
            foreach (string boardInput in unsolveableBoards)
            {
                board = new SudokuBoard(boardInput);
                solver = new Solver(board);
                try
                {
                    solver.GetSolution();
                }
                catch (Exception e)
                {
                    if (e is UnsolvableBoardException)
                    {
                        StringAssert.Contains(e.Message, unsolveableExceptionMessage);
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
        /// function that creates list of unsolveable boards
        /// </summary>
        private void CreateUnsolveablesList()
        {
            unsolveableBoards = new List<string>();
            unsolveableBoards.Add("100000000000100000000000005000000100000000000000000000000000000000000010000000000");
            unsolveableBoards.Add("200900000000000060000001000502600407000004100000098023000003080005010000007000000");
            unsolveableBoards.Add("516849732307605000809700065135060907472591006968370050253186074684207500791050608");
        }
    }
}
