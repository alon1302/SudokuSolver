using System;
using System.Collections.Generic;
using System.Text;

class Runner
{
    public static void run()
    {
        MainWriter mainWriter = new MainWriter();
        mainWriter.AddWriter(new ConsoleWriter());

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
            Console.WriteLine(new BoardFormatter().Format(input_str));
        }
        else
        {
            Console.WriteLine("Wrong input");
            return;
        }
        Solver solver = new Solver(input_board);
        string solutionStr = solver.GetSolution().getBoardStr();
        mainWriter.Write(solutionStr);
        Console.WriteLine(new BoardFormatter().Format(solutionStr));

    }
}

