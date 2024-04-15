using Core;

namespace Example;

class Program
{
    static void Main(string[] args)
    {
        var input = new string[args.Length-1];
        Array.Copy(args, 0, input, 0, input.Length);
        var testsGenerator = new TestsGenerator();
        
        testsGenerator.Generate(input, args[args.Length-1]);
    }
}