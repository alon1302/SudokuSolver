using Sudoku.exceptions;
using Sudoku.model;
using Sudoku.readers;
using Sudoku.solver;
using Sudoku.Validators;
using Sudoku.writers;
using System;
using System.Diagnostics;
using System.IO;

namespace Sudoku
{
    class Runner
    {
        private MainWriter _mainWriter; // the main writer (composite)
        private IWriter _consoleWriter; // console writer
        private IWriter _errorWriter; // error writer
        private IReader _reader; // reader

        /// <summary>
        /// Constructor that receives some writer and an error writer 
        /// initiates the Runner for the sudoku solver
        /// </summary>
        /// <param name="writer">the writer</param>
        /// <param name="errorWriter">the error writer</param>
        public Runner(IWriter writer, IWriter errorWriter)
        {
            _mainWriter = new MainWriter();
            _consoleWriter = writer;
            _errorWriter = errorWriter;
            _mainWriter.AddWriter(_consoleWriter);
        }

        /// <summary>
        /// function that display the choosing menu for the user
        /// </summary>
        private void DisplayMenu()
        {
            Console.WriteLine("Choose Format To Insert Your Sudoku Board:");
            Console.WriteLine("Press The ESC Key To Exit The Program");
            Console.WriteLine("Press 'F' to select text file");
            Console.WriteLine("Press any other key to insert board in the console");
        }

        /// <summary>
        /// function that tries to get the file path from the user
        /// and creates the file path to the output 
        /// add the new file writer to the composit
        /// </summary>
        /// <returns>true if there was no exceptions</returns>
        private bool HandleFileInput()
        {
            string inputFilePath;
            try
            {
                inputFilePath = FilePathesHandle.GetSelectedFilePath();
            }
            catch (FileDialogException)
            {
                return false;
            }
            string outputFilePath = FilePathesHandle.CreateResultFilePath(inputFilePath);
            _reader = new FileReader(inputFilePath);
            _mainWriter.AddWriter(new FileWriter(outputFilePath));
            return true;
        }

        /// <summary>
        /// function that tries to read the input from the reader and returns it
        /// if there was exception return null
        /// </summary>
        /// <returns>the input or null if exception</returns>
        private string GetStringInput()
        {
            try
            {
                return _reader.Read();
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException || e is FileTypeException)
                {
                    _errorWriter.Write(e.Message);
                    return null;
                }
                throw;
            }
        }

        /// <summary>
        /// function that receives input and call function to validate the input
        /// returns true if valid or false if not
        /// </summary>
        /// <param name="input">the input</param>
        /// <returns>true if valid or false otherwise</returns>
        private bool ValidateInput(string input)
        {
            InputValidator validator = new InputValidator(input);
            try
            {
                validator.Validate();
            }
            catch (Exception e)
            {
                if (e is InvalidBoardSizeException || e is InvalidCharacterException)
                {
                    _errorWriter.Write(e.Message);
                    return false;
                }
                else if (e is InvalidBoardException)
                {
                    _consoleWriter.Write(input);
                    _errorWriter.Write(e.Message);
                    return false;
                }
                throw;
            }
            return true;
        }

        /// <summary>
        /// function that recieves a valid sudoku board and tries to solve it
        /// return the solution or null if there was exception
        /// </summary>
        /// <param name="board">the valid board</param>
        /// <returns>the solution or null if exception</returns>
        private SudokuBoard SolveBoard(SudokuBoard board)
        {
            Solver solver = new Solver(board, new NakedSingleStrategy(), new HiddenSingleStrategy(), new IntersectionsStrategy());
            try
            {
                return solver.GetSolution();
            }
            catch (UnsolvableBoardException e)
            {
                _errorWriter.Write(e.Message);
                return null;
            }
        }

        /// <summary>
        /// function that display the solution and the time it took to solve the board
        /// </summary>
        /// <param name="solution">string that represent the solution</param>
        /// <param name="time">the time it took to solve</param>
        private void WriteSolution(string solution, long time)
        {
            Console.WriteLine("The Solution: ");
            _mainWriter.Write(solution);
            Console.WriteLine($"Successful solving after: {time} milliseconds\n");
        }

        /// <summary>
        /// function that receives the input mode and handle single run of sudoku solving 
        /// calls to little functions that handle all the parts of the run
        /// </summary>
        /// <param name="mode">the mode of the input (file/console/anything else in the future)</param>
        private void SingleRun(char mode)
        {
            switch (mode)
            {
                case 'F':
                case 'f':
                    if (!HandleFileInput())
                    {
                        return;
                    }
                    break;
                case 'T':
                case 't':
                default:
                    _reader = new ConsoleReader();
                    break;
            }
            string stringInput = GetStringInput();
            if (stringInput == null || !ValidateInput(stringInput))
            {
                return;
            }
            Console.WriteLine("The Initial Board: ");
            _consoleWriter.Write(stringInput);
            SudokuBoard board = new SudokuBoard(stringInput);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SudokuBoard solution = SolveBoard(board);
            if (solution == null)
            {
                return;
            }
            stopwatch.Stop();
            string solutionStr = solution.ToString();
            WriteSolution(solutionStr, stopwatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// function that handle all the runs until the user choose to exit
        /// </summary>
        public void RunAll()
        {
            Console.WriteLine("Wellcome To My Sudoku Solver!!!\n");
            char input;
            while (true)
            {
                DisplayMenu();
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    return;
                }
                input = keyInfo.KeyChar;
                Console.Clear();
                SingleRun(input);
            }
        }
    }
}

