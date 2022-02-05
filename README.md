# SudokuSolver

This project is a sudoku solver project in c# console application.

To use this solver all you need is to choose the format of the board insertion:

1. write to the console a string in the sudoku format string (sudoku format explanation down here)
2. choose a text file that contains only one line string that is a sudoku format string
the program will solve your sudoku (if it is a valid one) and bring you back the result in the same mode you choose at the start display of the solved board will automatically printed on the console and you can see also how much time it took to solve this board

in case of an invalid input or any error while the solve you will get detailed notice on the console

explanation about sudoku format string:

1. you can enter board in any valid size you choose from 1x1 board up to even 36x36 board or more (of course the solver can't solve any size)
2. your board size determined by the length of the string you have entered (square root of the length)
3. The characters that the board can contain are the char '0' to represent an empty cell and all the characters from '1' - '9' and so on in the ascii values table until your sudoku board size.

this solver based on some human sudoku solving strategies like: naked single, hidden single and intersections

