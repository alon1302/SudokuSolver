using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Validators
{
    public interface Ivalidator
    {
        /// <summary>
        /// check validation
        /// </summary>
        /// <returns>true if all valid</returns>
        bool Validate();
    }
}

