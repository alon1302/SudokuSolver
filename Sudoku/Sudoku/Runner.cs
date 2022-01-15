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
        IReader reader;
        switch (input)
        {
            case 'T':
            case 't':
                reader = new ConsoleReader();
                break;
            case 'F':
            case 'f':
                reader = new FileReader("D:\\הורדות\\check_file.txt");
                break;
            default:
                reader = new ConsoleReader();
                break;
        }

        SudokuBoard input_board;
        SudokuValidator validator = new SudokuValidator();
        try
        {
            input_board = new SudokuBoard(reader.Read(), validator);
        }
        catch (Exception e)
        {
            Console.WriteLine("Wrong input {0}", e.StackTrace);
            return;
        }

        Solver solver = new Solver(input_board, validator);
        mainWriter.Write(solver.GetSolution().BoardStr);
    }
}

