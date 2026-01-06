using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expiriments
{
    class BeNode
    {
        public string _value { get; set; }
        public BeNode? _right { get; set; }
        public BeNode? _left { get; set; }

        public BeNode(string value, BeNode? right=null, BeNode? left=null) { 
            _value = value;
            _right = right;
            _left = left;
        }

        public int treeDepth()
        {
            int rd = _right != null? _right.treeDepth() : 0;
            int ld = _left != null? _left.treeDepth() : 0; 
            return 1 + Math.Max(rd, ld);
        }


        public void printTree()
        {
            int depth = treeDepth();
            string[] board = new string[depth];

            writeToPrint(0, 0, depth, board);

            for (int i = 0; i < depth; i++)
            {
                Console.WriteLine(board[i]);
                Console.WriteLine();
            }
        }

        private void writeToPrint(int level, int margin, int depth, string[] board)
        {
            int finalMargin = Math.Max((depth - level + 1) * margin - 1, depth - level);
            board[level] += new string(' ', finalMargin);
            board[level] += _value;

            if (_right != null)
                _right.writeToPrint(level + 1, margin-1, depth, board); 
            if (_left != null)
                _left.writeToPrint(level + 1, margin + 1, depth, board);
        }

    }

    internal class calculator
    {
        public BeNode buildTree(string expression)
        {
            // peel brackets 
            if (expression[0] == '(' && expression[expression.Length - 1] == ')')  
                expression = expression.Substring(1, expression.Length-2);

            // if expression is only a number -> return it as a leaf with expression as its value
            if (expression.All(c => char.IsDigit(c))) // '0' <= 'c' <= '9'
                return new BeNode(expression);

            // else -> build the tree recursively
            int bracketsLevel = 0;
            char breakChar = '@';
            int breakIndx = 0;
            Dictionary<char, int> opPriority = new Dictionary<char, int>
            {
                {'*', 10 },
                {'/', 10 },
                {'+', 5 },
                {'-', 5},
                {'@', int.MaxValue } // default - only for place holder
            };
            char c = expression[0];
            for (int i = 0; i < expression.Length; i++)
            {
                c = expression[i];
                if (c == '(')
                    bracketsLevel++;
                else if (c == ')')
                    bracketsLevel--;
                else if (bracketsLevel > 0 || char.IsDigit(c))
                    continue;
                else if (opPriority[c] <= opPriority[breakChar])
                {
                    breakChar = c;
                    breakIndx = i;
                }
            }

            return new BeNode(breakChar.ToString(), 
                buildTree(expression.Substring(0, breakIndx)),
                buildTree(expression.Substring(breakIndx+1)));
        }

        public float calcTree(BeNode node)
        {
            if (node._value.All(c => char.IsDigit(c)))
                return int.Parse(node._value);

            var opDict = new Dictionary<string, Func<float, float, float>>
            {
                {"*", (a, b) => a*b },
                {"/", (a, b) => a/b },
                {"+", (a, b) => a+b },
                {"-", (a, b) => a-b }
            };

            return opDict[node._value](
                calcTree(node._right), 
                calcTree(node._left));
        }
    }
}
