using System;
using System.Collections.Generic;
using System.Text;

interface IFormatter
{
    /// <summary>
    /// function that receives string of some data and format it in some way
    /// </summary>
    /// <param name="input">the data</param>
    /// <returns></returns>
    string Format(string input);
}

