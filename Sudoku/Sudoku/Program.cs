using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //SudokuBoard s = new SudokuBoard("000000900000800003004691000009040500620008000007500301900100208001005070000002000");
            //SudokuBoard c = (SudokuBoard)s.Clone();
            //s[1, 1].RemoveOption('1');
            //s = (SudokuBoard)c.Clone();

            Runner.RunAll();
        }
    }
}
