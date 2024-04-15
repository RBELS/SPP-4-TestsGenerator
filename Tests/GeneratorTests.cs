using Core;

namespace Tests;

public class GeneratorTests
{
    private readonly string _outputDir = "/Users/artyom/Documents/study/poit/6sem/spp/TestsGenerator/Tests/TestDirectory";
    private readonly TestsGenerator _testsGenerator = new();

    [Test]
    public void Test1()
    {
        const string className = "Foo";
        var files = new List<string>() { "/Users/artyom/Documents/study/poit/6sem/spp/TestsGenerator/Tests/Entities/Foo.cs" };

        _testsGenerator.Generate(files, _outputDir);
        var generated = File.ReadAllText(Path.Combine(_outputDir, GetExpectedFileName(className)));
        generated = FilterTextOfFile(generated);

        var expected = File.ReadAllText(Path.Combine(_outputDir, GetExpectedFileName(className)));
        expected = FilterTextOfFile(expected);
        Assert.That(generated, Is.EqualTo(expected));
    }

    [Test]
    public void Test2()
    {
        const string className = "Bar";
        var files = new List<string>() { "/Users/artyom/Documents/study/poit/6sem/spp/TestsGenerator/Tests/Entities/Bar.cs" };

        _testsGenerator.Generate(files, _outputDir);
        var generated = File.ReadAllText(Path.Combine(_outputDir, GetExpectedFileName(className)));
        generated = FilterTextOfFile(generated);

        var expected = File.ReadAllText(Path.Combine(_outputDir, GetExpectedFileName(className)));
        expected = FilterTextOfFile(expected);
        Assert.That(generated, Is.EqualTo(expected));
    }
    
    private  string GetExpectedFileName(string className) => $"Expected{className}Tests";
    
    private string FilterTextOfFile(string text)
    {
        return text.Replace(" ", "").Replace("\r", "").Replace("\n", "");
    }

}