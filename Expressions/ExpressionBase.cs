using RationalNumberLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationalNumbersMultiple.Expressions
{
    interface IExpression
    {
        int Priority { get; }

        RationalNumber Solve();
    }
}
