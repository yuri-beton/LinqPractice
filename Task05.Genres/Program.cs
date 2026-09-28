using System.Globalization;
using System.Security.Cryptography;
using System.Text;

// Complete Solve. The assignment is in LinqPracticeTasks.pdf.
// Leave the sample data, the records, and the checker unchanged.

try
{
    Report(Format(Solve(Sample.Books(), Sample.Loans())));
}
catch (NotImplementedException ex)
{
    Console.WriteLine(ex.Message);
}

static IEnumerable<GenreReport> Solve(IReadOnlyList<Book> books, IReadOnlyList<Loan> loans)
{
    throw new NotImplementedException("Complete the Solve method. The assignment is in LinqPracticeTasks.pdf.");
}

static string Format(IEnumerable<GenreReport> reports) =>
    string.Join("\n", reports.Select(report =>
        $"{report.Genre} | members {report.MemberCount} | avg {report.AverageOverdue.ToString("F2", CultureInfo.InvariantCulture)} | {report.MostOverdueBook}"));

static void Report(string output)
{
    const string ExpectedHash = "1C6576480D346BBD2A10B9AB9B55C6D9644A6E7FF27A3D7388A857B9614D2E6F";

    var normalized = output.Replace("\r\n", "\n").TrimEnd('\n');
    Console.WriteLine(normalized);
    Console.WriteLine();
    var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(normalized)));
    Console.WriteLine(hash == ExpectedHash
        ? "Correct."
        : "Not yet correct. Compare your output with LinqPracticeTasks.pdf.");
}

record Book(int Id, string Title, string Genre);

record Loan(int BookId, string Member, int DaysOverdue);

record GenreReport(string Genre, int MemberCount, decimal AverageOverdue, string MostOverdueBook);

static class Sample
{
    public static IReadOnlyList<Book> Books() =>
    [
        new(1, "Dune", "SciFi"),
        new(2, "Neuromancer", "SciFi"),
        new(3, "Foundation", "SciFi"),
        new(4, "Persuasion", "Classic"),
        new(5, "Emma", "Classic"),
        new(6, "Pride and Prejudice", "Classic"),
        new(7, "The Hobbit", "Fantasy"),
    ];

    public static IReadOnlyList<Loan> Loans() =>
    [
        new(1, "Ana", 0),
        new(1, "Ben", 4),
        new(2, "Ana", 2),
        new(2, "Cal", 0),
        new(3, "Ben", 3),
        new(3, "Ana", 6),
        new(4, "Ben", 3),
        new(4, "Ana", 1),
        new(5, "Cal", 4),
        new(6, "Ben", 0),
        new(6, "Cal", 0),
        new(7, "Cal", 0),
    ];
}
