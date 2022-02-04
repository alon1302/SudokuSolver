using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        // this is code that make the console open in full screen
        // disclaimer - copy from the internet
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        private const int MAXIMIZE = 3;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        /// <summary>
        /// the main function of the project
        /// opens the console in full screen and calls the run function to start the solver
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
            Runner runner = new Runner(new ConsoleWriter(new BoardFormatter()), new ErrorWriter());
            runner.RunAll();
        }
    }
}
