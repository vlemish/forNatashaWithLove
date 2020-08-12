using RationalNumberLib;
using RationalNumbersMultiple.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RationalNumbersMultiple
{
    class Context
    {
        public string Data { get; set; }

        private List<IExpression> expressions;
        private ExpressionBaseTree baseTree;


        public Context(string data)
        {
            Data = data;
            expressions = new List<IExpression>();
            baseTree = new ExpressionBaseTree();
        }

        private void Parse()
        {
            var parsedData = Data.Split(' ');
            
            foreach(var str in parsedData)
            {
                expressions.Add(DefineType(str));                
            }

            foreach(var expression in expressions)
            {
                baseTree.Add(expression);
            }
        }

        private IExpression DefineType(string str)
        {
            Regex rationalMixedPattern = new Regex(@"^\d+\.\d+[/]\d+");
            Regex rationalPattern = new Regex(@"^\d+/\d+");

            if(rationalPattern.IsMatch(str))
            {
                var subset = str.Split('/');
                var numerator = subset[0];
                var denumerator = subset[1];

                return new RationalNuberExpression(new RationalNumber(int.Parse(numerator), int.Parse(denumerator)));
            }

            else if (rationalMixedPattern.IsMatch(str))
            {
                var integer = string.Join("",str.TakeWhile(i=>i!='.'));
                var subset = str.Remove(0, str.IndexOf('.')+1).Split('/');
                var numerator = subset[0];
                var denumerator = subset[1];

                return new RationalNuberExpression(new RationalNumber(int.Parse(integer), int.Parse(numerator), int.Parse(denumerator)));
            }

            else if (str.All(i => Char.IsDigit(i)))
            {
                return new RationalNuberExpression(new RationalNumber(int.Parse(str), 1));
            }

            else if (str == "+")
            {
                return new Plus();
            }

            else if (str == "-")
            {
                return new Minus();
            }

            else if (str == "*")
            {
                return new Multiply();
            }

            else if (str == "/")
            {
                return new Divide();
            }

            throw new Exception($"Uncorrect input: {str} isn't supported!");

        }

        public void Solve()
        {
            Parse();

            string res = $"{baseTree.RootNode.Solve().ToString()}";

            Console.WriteLine(res);
        }
    }
}
