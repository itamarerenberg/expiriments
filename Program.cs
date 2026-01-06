// See https://aka.ms/new-console-template for more information
using expiriments;

calculator calc = new calculator();

string input = "";

while (input != "exit")
{
    input = Console.ReadLine()?? "";
    try
    {
        var tree = calc.buildTree(input);
        tree.printTree();
        Console.WriteLine("result " + calc.calcTree(tree));
    }
    catch (Exception ex)
    {
        Console.WriteLine("invalid expression" + ex);
    }


}


