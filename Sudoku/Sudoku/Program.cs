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

            ////SudokuBoard s = new SudokuBoard("318005406000603810006080503864952137123476958795318264030500780000007305000039641");
            //SudokuBoard s = new SudokuBoard("762008001980000006150000087478003169526009873319800425835001692297685314641932758");

            //IntersectionsStrategy i = new IntersectionsStrategy();
            //Console.WriteLine(i.Solve(s));

            Runner.RunAll();
        }
    }
}
