using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
interface Ivalidator
{

    bool IsCharAppearOnceInRow(char ch, int row);
    bool IsCharAppearOnceInCol(char ch, int col);
    bool IsCharAppearOnceInBox(char ch, int row, int col);
}

