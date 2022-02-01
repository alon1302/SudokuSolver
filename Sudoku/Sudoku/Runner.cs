using System;
using System.IO;

class Runner
{
    public static void run()
    {
        MainWriter mainWriter = new MainWriter();
        ConsoleWriter consoleWriter = new ConsoleWriter(new BoardFormatter());
        ErrorWriter errorWriter = new ErrorWriter();
        mainWriter.AddWriter(consoleWriter);

        Console.WriteLine("how to you want to insert your board?\nF - file \nT -typing");
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
                string filePath = OpenFileDialogHandle.GetSelectedFilePath();
                if (filePath == null)
                {
                    errorWriter.Write("Failed to open file");
                    return;
                }
                reader = new FileReader(filePath);
                mainWriter.AddWriter(new FileWriter(CreateResultFilePath(filePath)));
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

    private static string CreateResultFilePath(string filePath)
    {
        DirectoryInfo parentPath = Directory.GetParent(filePath);
        string newPath = parentPath.FullName;
        string newFileName = Path.GetFileNameWithoutExtension(filePath);
        newFileName += "-result.txt";
        newPath = Path.Combine(newPath, newFileName);
        return newPath;
    }
}

