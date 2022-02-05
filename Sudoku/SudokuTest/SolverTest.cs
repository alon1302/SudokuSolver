using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sudoku;
using System.Collections.Generic;
using Sudoku.model;
using Sudoku.solver;

namespace SudokuTest
{
    /// <summary>
    /// this test class test the Sudoku Solver
    /// it creates dictionary of valid boards before solution and expected solution pairs
    /// and checks for every pair if the solver returns the expected solution for the board
    /// </summary>
    [TestClass]
    public class SolverTest
    {
        Dictionary<string, string> boardsSolutionsPairs; // (board : expected solution) pairs
        SudokuBoard board; // current board
        Solver solver; // current solver
        SudokuBoard solution; // current solution

        /// <summary>
        /// function that iterate over the dictionary of boards and expectes solutions and
        /// checks for each of them if the solution equals to the expected one
        /// </summary>
        [TestMethod]
        public void SolveTests()
        {
            CreateBoardsSolutionsPairs();
            foreach (KeyValuePair<string, string> sudokuSolve in boardsSolutionsPairs)
            {
                board = new SudokuBoard(sudokuSolve.Key);
                solver = new Solver(board, new NakedSingleStrategy(), new HiddenSingleStrategy(), new IntersectionsStrategy());
                solution = solver.GetSolution();
                Assert.AreEqual(solution.ToString(), sudokuSolve.Value); // check if the current solution equals to the expected one
            }
        }

        /// <summary>
        /// function that inserts to the dictionary some differents sudoku boards and their expected solutions
        /// </summary>
        private void CreateBoardsSolutionsPairs()
        {
            boardsSolutionsPairs = new Dictionary<string, string>();
            // 1x1 board
            boardsSolutionsPairs.Add("0", "1");
            // 4x4 boards
            boardsSolutionsPairs.Add("1000020100000304", "1423324141322314");
            boardsSolutionsPairs.Add("2000000300000410", "2341412312343412");
            boardsSolutionsPairs.Add("0003100000400100", "2413132432414132");
            boardsSolutionsPairs.Add("0134000003100000", "2134342143121243");
            // 9x9 boards
            boardsSolutionsPairs.Add("100907003080000070009000600007209400410000095008504300003000700050000040200806009",
                                     "164957283385621974729438651537289416412763895698514327843195762956372148271846539");
            boardsSolutionsPairs.Add("040070306600200004090600070000060000050801060700090001200500000009400703870030040",
                                     "145978326687253194392614875421765938953841267768392451234587619519426783876139542");
            boardsSolutionsPairs.Add("002000000000000000000008000050001406000000170601000090000000000000009000003600000",
                                     "462713859938524761175968234357891426894256173621347598549132687286479315713685942");
            boardsSolutionsPairs.Add("000000900000800003004691000009040500620008000007500301900100208001005070000002000",
                                     "568723914192854763734691852319247586625318497847569321953176248281435679476982135");
            boardsSolutionsPairs.Add("006000007970000040500000800000700500400003170050008006000301002000805000603902000",
                                     "126584937978136245534297861361729584482653179759418326845361792297845613613972458");
            // 16x16 boards
            boardsSolutionsPairs.Add("00<00010020008000003?=<001:4500000@>;007500=?30020=706800?>0410;000000?>23000000<02;=90@:05>1?07>50000000000003600180002;0009=0000?:00014000@<004;000000000000?8107<240;=0?83:0500000063<:000000@09?0<200;70=5030031500?>0027;0000057>;00<13800000;000@004000900",
                                     "5?<4>31:@29;687=;683?=<971:45>2@91@>;247586=?3:<2:=7@6853?><419;=769<8?>2341;@5:<42;=93@:65>1?87>5:@1;7489=?<236?31865:2;7<@9=4>3=?:87514>26@<;94;>6:@=<153927?8197<24>;=@?83:658@529?63<:;7>4=1@>9?4<286;7:=51368315:9?>=@27;<4:2457>;=9<1386@?7<;=31@6?485:9>2");
            boardsSolutionsPairs.Add("102000;680054<00>00;08:0<09007000<00000002700?090090070000:0>85;0:0@1002;40600080300000900000000;942050>00=030000000008@3920040000100:?39600000000060900@0<02;4>00000000200000102000@0>8100=<06054?10>0000600@0060@00250000000<000<00@0:0710=00400:>?00;43000501",
                                     "172:93;68>?54<@=>?5;=8:4<@931726=<84>1@5627;:?39@693<72?=1:4>85;<:=@1?32;4569>787368;4=9?<>@51:2;942:5<>78=136?@?1>5768@392:;4=<4@1<2:?396;>7=85:836591=@?<72;4>9>;=6<472538@:1?257?@;>81:4=<96354?13>9<:=628@;76=@74251>;89?3<:3;<98@6:571?=2>482:>?=7;43@<6591");
            boardsSolutionsPairs.Add("8090:00<@0?0000400?00050070:00=02000000400000@0>060300>000;090000000090010020007;000>00600000=00070@0200<00000100005800=03000009<0400070=080;00005080=0:0@04000000>00090600;010310003005000060@0>07000:00<0908200@0<0>030000=00;5:0060;000>00700?01000020005000<",
                                     "8197:32<@>?=5;644;?>9@58276:<3=12<5:;6=48193?@7>=6@31?>754;<92:838=?59<@1;:246>7;4<1>:?6987@2=35976@423;<5=>8?1::>25871=?346@<;9<346@17>=98?;:5275;82=6:3@14>9<?@2>=<49?6:5;71831?:93;85>2<764@=>=74?5:1;<@938266@8<7>43:?21=59;5:326<;94=>817?@?91;=8@27635:>4<");
            boardsSolutionsPairs.Add("030>0:060092040?5@00<00;006300070?09000000040;@007000010@;00000000010>0000000=00600;0000092=4001090008;00000207000040<0?0008050000>=160<0:700983000200001000;<?0<000;00:00@0=000@090>3070200:006000041?020050009>000070000;06030000060000>0:1@50?20000300000000:",
                                     ";3<>8:@67=92541?5@=:<4>;?16382971?6972=3:584>;@<2748?519@;><3:6=3:@19>42;657?=<868?;:@75<92=43>1=95<38;14?:>267@7>24=<6?3@1895:;4;>=162<5:7?@983:672@=541839;<?><183;?9:>4@6=725@59?>387=2<;:1468<:641?@23=57>;9>=1527:89<;@6?3494376;<=8>?:1@52?2;@593>6741<8=:");
        }
    }
}
