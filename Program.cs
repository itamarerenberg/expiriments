// See https://aka.ms/new-console-template for more information
using experiments;


queensProblem();


void calculator()
{
    Calculator calc = new Calculator();

    string input = "";
    Printer printer = new Printer();

    while (input != "exit")
    {
        Console.WriteLine("insert expression (only [+-*:() or a one digit number])");
        input = Console.ReadLine() ?? "";
        try
        {
            var tree = calc.buildTree(input);
            printer.PrintTree(tree);
            Console.WriteLine("result " + calc.calcTree(tree));
        }
        catch (Exception ex)
        {
            Console.WriteLine("invalid expression" + ex);
        }
    }
}

void queensProblem()
{
    BoardPrinter bp = new();

    bp.PrintBoard(new int[] { 0, 3, 6, 1, 4, 7, 2, 5 });
}
