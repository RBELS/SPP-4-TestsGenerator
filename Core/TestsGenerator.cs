namespace Core;

public class TestsGenerator
{
    private TestsCompiler _compiler = new();
    private string _directoryToLoadTests = @"./tests";

    public void Generate(IEnumerable<string> pathsToFiles, string pathToLoad)
    {
        if (!Directory.Exists(pathToLoad))
            Directory.CreateDirectory(pathToLoad);

        foreach (var path in pathsToFiles)
        {
            _ = ParseFile(path, pathToLoad);
        }
    }

    private async Task ParseFile(string path, string pathToLoad)
    {
        var items = LoadClassTree(path);
        foreach (var item in items)
        {
            GenerateTestClass(item);
            item.PathToGeneratedClass = Path.Combine(pathToLoad, $"{item.ClassName}Tests.cs");
            await LoadToFile(item);
        }
        Console.WriteLine("Parsing done");
    }

    private List<GenerateItem> LoadClassTree(string path)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(path));
        var walker = new CustomSyntaxWalker();
        walker.Visit(syntaxTree.GetRoot());
        Console.WriteLine("Loading done");
        return walker.Items;
    }

    private void GenerateTestClass(GenerateItem item)
    {
        item.Code = _compiler.GenerateCodeByItem(item);
        item.PathToGeneratedClass = Path.Combine(_directoryToLoadTests, $"{item.ClassName}Tests.cs");
        Console.WriteLine("Generation done");
    }

    private async Task LoadToFile(GenerateItem item)
    {
        if (!string.IsNullOrEmpty(item.PathToGeneratedClass) && item.Code != null)
            await File.WriteAllTextAsync(item.PathToGeneratedClass, item.Code.ToString());
        else
            Console.WriteLine("Path is null");
    }
}