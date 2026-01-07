using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace experiments
{
    class BeNode
    {
        public char _value { get; set; }
        public BeNode? _right { get; set; }
        public BeNode? _left { get; set; }

        public BeNode(char value, BeNode? right = null, BeNode? left = null)
        {
            _value = value;
            _right = right;
            _left = left;
        }

        public int treeDepth()
        {
            int rd = _right != null ? _right.treeDepth() : 0;
            int ld = _left != null ? _left.treeDepth() : 0;
            return 1 + Math.Max(rd, ld);
        }
    }

    class Printer
    {
        public int _nodeWidth = 3;
        public int _nodeHeight = 1;

        public void PrintTree(BeNode tree)
        {
            int depth = tree.treeDepth();
            int ySize = depth * _nodeHeight;
            int xSize = (int)Math.Pow(2, depth - 1) * (_nodeWidth+1);
            char[][] board = new char[ySize][];
            for (int i = 0; i < ySize; i++)
            {
                board[i] = new char[xSize+1];
                Array.Fill(board[i], ' ');
            }

            writeToPrint(tree, 0, xSize/2, xSize/4, board);

            for (int i = 0; i < ySize; i++)
            {
                Console.WriteLine(new string(board[i]));
                Console.WriteLine();
            }
        }

        private void writeToPrint(BeNode tree, int yDepth, int xDepth, int margin, char[][] board)
        {
            //board[yDepth][xDepth] = '-';
            //board[yDepth][xDepth+1] = '-';
            //board[yDepth][xDepth+2] = '-';
            
            board[yDepth][xDepth] = '/';
            board[yDepth][xDepth+1] = tree._value;
            board[yDepth][xDepth+2] = '\\';

            //board[yDepth+2][xDepth] = '-';
            //board[yDepth+2][xDepth + 1] = '-';
            //board[yDepth+2][xDepth + 2] = '-';

            

            if (tree._right != null)
            {
                for (int i = xDepth+3; i < xDepth + margin; i++)
                    board[yDepth + 1][i] = '~';
                board[yDepth + 1][xDepth+2] = '|';
                writeToPrint(tree._right, yDepth + _nodeHeight, xDepth + margin, margin / 2, board);
            }

            if (tree._left != null)
            {
                for(int i = xDepth - margin + _nodeWidth; i < xDepth; i++)
                    board[yDepth + 1][i] = '~';
                board[yDepth + 1][xDepth] = '|';
                writeToPrint(tree._left, yDepth + _nodeHeight, xDepth - margin, margin / 2, board);
            }
        }
    }

    internal class Calculator
    {
        public BeNode buildTree(string expression)
        {
            // peel brackets 
            int bracketsLevel = 0;
            try
            {
                while (expression[0] == '(')
                {
                    bool peelBrackets = true;
                    bracketsLevel = 1;
                    for (int i = 1; i < expression.Length-1; i++)
                    {
                        if (expression[i] == '(')
                            bracketsLevel++;
                        else if (expression[i] == ')')
                            bracketsLevel--;

                        if (bracketsLevel == 0) { 
                            peelBrackets=false;
                            break;
                        } 
                    }
                    if (peelBrackets)
                        expression = expression.Substring(1, expression.Length - 2);
                    else
                        break;
                }
            }
            catch
            {
                Console.WriteLine("-");
            }

            // if expression is only a number -> return it as a leaf with expression as its value
            if (expression.All(c => char.IsDigit(c))) // '0' <= 'c' <= '9'
                return new BeNode(expression[0]);

            // else -> build the tree recursively
            bracketsLevel = 0;
            char breakChar = '@';
            int breakIndx = 0;
            Dictionary<char, int> opPriority = new Dictionary<char, int>
            {
                {'*', 10 },
                {':', 10 },
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

            return new BeNode(breakChar,
                buildTree(expression.Substring(0, breakIndx)),
                buildTree(expression.Substring(breakIndx + 1)));
        }

        public float calcTree(BeNode node)
        {
            if (char.IsDigit(node._value))
                return int.Parse(node._value.ToString());

            var opDict = new Dictionary<char, Func<float, float, float>>
            {
                {'*', (a, b) => a*b },
                {':', (a, b) => a/b },
                {'+', (a, b) => a+b },
                {'-', (a, b) => a-b }
            };

            return opDict[node._value](
                calcTree(node._right),
                calcTree(node._left));
        }
    }
}
