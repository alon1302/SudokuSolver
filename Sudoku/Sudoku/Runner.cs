using System;
using System.IO;

class Runner
{
    public static void run()
    {
        MainWriter mainWriter = new MainWriter();
        ConsoleWriter consoleWriter = new ConsoleWriter(new BoardFormatter());
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
                reader = new FileReader(filePath);
                mainWriter.AddWriter(new FileWriter(CreateResultFilePath(filePath)));
                break;
            default:
                reader = new ConsoleReader();
                break;
        }

        SudokuBoard input_board;

        string input_str = reader.Read();
        InputValidator validator = new InputValidator(input_str);
        if (validator.Validate())
        {
            input_board = new SudokuBoard(input_str);
            consoleWriter.Write(input_str);
        }
        else
        {
            mainWriter.Write("Wrong input");
            return;
        }
        Solver solver = new Solver(input_board);
        string solutionStr = solver.GetSolution().getBoardStr();
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

