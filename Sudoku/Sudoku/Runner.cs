using System;
using System.Diagnostics;
using System.IO;

class Runner
{
    public static void RunAll()
    {
        Console.WriteLine("Wellcome To My Sudoku Solver!!!\n");
        char input;
        while (true)
        {
            Console.WriteLine("Choose Format To Insert Your Sudoku Board:");
            Console.WriteLine("Press The ESC Key To Exit The Program");
            Console.WriteLine("Press 'F' to select text file");
            Console.WriteLine("Press any other key to insert board in the console");
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

    public static void SingleRun(char mode)
    {
        MainWriter mainWriter = new MainWriter();
        ConsoleWriter consoleWriter = new ConsoleWriter(new BoardFormatter());
        ErrorWriter errorWriter = new ErrorWriter();
        mainWriter.AddWriter(consoleWriter);
        IReader reader;
        switch (mode)
        {
            case 'F':
            case 'f':
                string inputFilePath;
                try
                {
                    inputFilePath = FilePathesHandle.GetSelectedFilePath();
                }
                catch (FileDialogException)
                {
                    Console.WriteLine("There Was An Error With The Open File Dialog, Please Enter Full File Path");
                    inputFilePath = Console.ReadLine();
                }
                string outputFilePath = FilePathesHandle.CreateResultFilePath(inputFilePath);
                reader = new FileReader(inputFilePath);
                mainWriter.AddWriter(new FileWriter(outputFilePath));
                break;
            case 'T':
            case 't':
            default:
                reader = new ConsoleReader();
                break;
        }

        SudokuBoard input_board;
        string input_str = null;
        try
        {
            input_str = reader.Read();
        }
        catch (Exception e)
        {
            if (e is FileNotFoundException || e is FileTypeException)
            {
                errorWriter.Write(e.Message);
                return;
            }
            throw;
        }
        InputValidator validator = new InputValidator(input_str);
        try
        {
            validator.Validate();
        }
        catch (Exception e)
        {
            if (e is InvalidBoardSizeException || e is InvalidCharacterException)
            {
                errorWriter.Write(e.Message);
                return;
            }
            else if (e is InvalidBoardException)
            {
                consoleWriter.Write(input_str);
                errorWriter.Write(e.Message);
                return;
            }
            throw;
        }
        Console.WriteLine("The Initial Board: ");
        consoleWriter.Write(input_str);
        input_board = new SudokuBoard(input_str);
        Solver solver = new Solver(input_board);
        Stopwatch stopwatch = new Stopwatch();
        SudokuBoard solved = null;
        try
        {
            stopwatch.Start();
            solved = solver.GetSolution();
            stopwatch.Stop();
        }
        catch (UnsolvableBoardException e)
        {
            errorWriter.Write(e.Message);
            return;
        }
        string solutionStr = solved.ToString();
        Console.WriteLine("The Solution: ");
        mainWriter.Write(solutionStr);
        Console.WriteLine("Successful solving after: " + stopwatch.ElapsedMilliseconds + " milliseconds\n");
    }
}

