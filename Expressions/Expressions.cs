
using RationalNumberLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationalNumbersMultiple.Expressions
{
    class RationalNuberExpression : IExpression
    {
        public int Priority { get; } = 0;

        public RationalNumber RationalNumber { get; set; }

        public RationalNuberExpression(RationalNumber rationalNumber)
        {
            RationalNumber = rationalNumber;
        }

        public RationalNumber Solve()
        {
            return RationalNumber;
        }
    }

    abstract class ExpressionBinary : IExpression
    {
        public abstract int Priority { get; }

        public abstract IExpression LeftOperand { get; set; }

        public abstract IExpression RightOperand { get; set; }

        public ExpressionBinary(IExpression left, IExpression right)
        {
            LeftOperand = left;
            RightOperand = right;
        }

        public ExpressionBinary() { }


        public abstract RationalNumber Solve();


    }

    class Plus : ExpressionBinary
    {
        public override int Priority => 1;

        public override IExpression LeftOperand { get; set; }
        public override IExpression RightOperand { get; set; }

        public Plus(IExpression left, IExpression right)
            : base(left, right) { }

        public Plus() { }

        public override RationalNumber Solve() => LeftOperand.Solve() + RightOperand.Solve();
    }

    class Minus : ExpressionBinary
    {
        public override int Priority => 1;

        public override IExpression LeftOperand { get; set; }
        public override IExpression RightOperand { get; set; }

        public Minus(IExpression left, IExpression right)
            : base(left, right) { }

        public Minus() { }

        public override RationalNumber Solve() => LeftOperand.Solve() - RightOperand.Solve();
    }

    class Multiply : ExpressionBinary
    {
        public override int Priority => 2;

        public override IExpression LeftOperand { get; set; }
        public override IExpression RightOperand { get; set; }

        public Multiply(IExpression left, IExpression right)
            : base(left, right) { }

        public Multiply() { }

        public override RationalNumber Solve() => LeftOperand.Solve() * RightOperand.Solve();
    }

    class Divide : ExpressionBinary
    {
        public override int Priority => 2;

        public override IExpression LeftOperand { get; set; }
        public override IExpression RightOperand { get; set; }

        public Divide(IExpression left, IExpression right)
            : base(left, right) { }

        public Divide() { }

        public override RationalNumber Solve() => LeftOperand.Solve() / RightOperand.Solve();
    }
}
