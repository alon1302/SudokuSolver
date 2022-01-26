using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


class ConsoleReader : IReader
{
    public string Read()
    {
        Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
        Console.WriteLine("enter string that represent sudoku board: ");
        return Console.ReadLine();
    }
}

