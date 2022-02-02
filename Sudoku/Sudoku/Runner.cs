using System;
using System.IO;

class Runner
{
    public static void RunAll()
    {
        char input = '-';
        do
        {
            SingleRun();
            Console.WriteLine("want to continue? press 'Y', otherwise press 'N'");
            input = Console.ReadKey().KeyChar;
            
        } while (input != 'N' && input != 'n');
    }

    public static void SingleRun()
    {
        MainWriter mainWriter = new MainWriter();
        ConsoleWriter consoleWriter = new ConsoleWriter(new BoardFormatter());
        ErrorWriter errorWriter = new ErrorWriter();
        mainWriter.AddWriter(consoleWriter);

        Console.WriteLine("\nhow to you want to insert your board?\nF - file \nT -typing");
        char input = Console.ReadKey().KeyChar;
        Console.WriteLine();
        IReader reader;
        switch (input)
        {
            case 'T':
            case 't':
                reader = new ConsoleReader();
                break;
            case 'F':
            case 'f':
                string inputFilePath = FilePathesHandle.GetSelectedFilePath();
                if (inputFilePath == null)
                {
                    errorWriter.Write("Failed to open file");
                    return;
                }
                string outputFilePath = FilePathesHandle.CreateResultFilePath(inputFilePath);
                reader = new FileReader(inputFilePath);
                mainWriter.AddWriter(new FileWriter(outputFilePath));
                break;
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
        catch (FileNotFoundException e)
        {
            errorWriter.Write(e.Message);
            return;
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
            }
            else if (e is InvalidBoardException)
            {
                consoleWriter.Write(input_str);
                errorWriter.Write(e.Message);
            }
            return;
        }
        consoleWriter.Write(input_str);
        input_board = new SudokuBoard(input_str);
        Solver solver = new Solver(input_board);
        SudokuBoard solved = solver.GetSolution();
        string solutionStr = solved.getBoardStr();
        mainWriter.Write(solutionStr);
    }

    
}

