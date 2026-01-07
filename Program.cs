// See https://aka.ms/new-console-template for more information
using experiments;

Calculator calc = new Calculator();

string input = "";
Printer printer = new Printer();

while (input != "exit")
{
    input = Console.ReadLine()?? "";
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


