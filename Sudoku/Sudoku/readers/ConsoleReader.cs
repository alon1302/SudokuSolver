using System;
using System.Collections.Generic;
using System.Text;


class ConsoleReader : IReader
{
    public string Read()
    {
        Console.WriteLine("enter string that represent sudoku board: ");
        return Console.ReadLine();
    }
}

