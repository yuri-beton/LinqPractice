using System.Security.Cryptography;
using System.Text;

// Complete Solve. The assignment is in LinqPracticeTasks.pdf.
// Leave the sample data, the records, and the checker unchanged.

try
{
    Report(Format(Solve(Sample.Products())));
}
catch (NotImplementedException ex)
{
    Console.WriteLine(ex.Message);
}

static IEnumerable<string> Solve(IReadOnlyList<Product> products)
{
    return products.Where(p => p.Category == "Book" && p.InStock == true && p.Price < 25).OrderBy(p => p.Price).ThenBy(p => p.Name).Select(p => p.Name);
}

static string Format(IEnumerable<string> names) =>
    string.Join("\n", names);

static void Report(string output)
{
    const string ExpectedHash = "F89F08B647CA9FF5812528D27D646C4414048B3AF39961CCB0D6C1DF6CA69DC9";

    var normalized = output.Replace("\r\n", "\n").TrimEnd('\n');
    Console.WriteLine(normalized);
    Console.WriteLine();
    var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(normalized)));
    Console.WriteLine(hash == ExpectedHash
        ? "Correct."
        : "Not yet correct. Compare your output with LinqPracticeTasks.pdf.");
}

record Product(string Name, string Category, decimal Price, bool InStock);

static class Sample
{
    public static IReadOnlyList<Product> Products() =>
    [
        new("The Hobbit", "Book", 12.50m, true),
        new("Dune", "Book", 18.00m, true),
        new("Notebook", "Stationery", 4.00m, true),
        new("Foundation", "Book", 22.00m, false),
        new("Neuromancer", "Book", 9.99m, true),
        new("Pen", "Stationery", 1.50m, true),
        new("Snow Crash", "Book", 18.00m, true),
    ];
}
