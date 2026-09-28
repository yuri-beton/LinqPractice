using System.Security.Cryptography;
using System.Text;

// Complete Solve. The assignment is in LinqPracticeTasks.pdf.
// Leave the sample data, the records, and the checker unchanged.

try
{
    Report(Format(Solve(Sample.Tickets())));
}
catch (NotImplementedException ex)
{
    Console.WriteLine(ex.Message);
}

static IEnumerable<AgentLoad> Solve(IReadOnlyList<Ticket> tickets)
{
    throw new NotImplementedException("Complete the Solve method. The assignment is in LinqPracticeTasks.pdf.");
}

static string Format(IEnumerable<AgentLoad> loads) =>
    string.Join("\n", loads.Select(load =>
        $"{load.Agent} | tickets {load.Tickets} | hours {load.Hours} | high {load.HighPriority}"));

static void Report(string output)
{
    const string ExpectedHash = "8BB384DFAD40188976DBAB69833902FF94D9BEEBA3070CCD40E98E985606C7DE";

    var normalized = output.Replace("\r\n", "\n").TrimEnd('\n');
    Console.WriteLine(normalized);
    Console.WriteLine();
    var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(normalized)));
    Console.WriteLine(hash == ExpectedHash
        ? "Correct."
        : "Not yet correct. Compare your output with LinqPracticeTasks.pdf.");
}

record Ticket(int Id, string Agent, string Priority, int Hours);

record AgentLoad(string Agent, int Tickets, int Hours, int HighPriority);

static class Sample
{
    public static IReadOnlyList<Ticket> Tickets() =>
    [
        new(1, "Alex", "High", 3),
        new(2, "Blair", "Medium", 2),
        new(3, "Alex", "Low", 1),
        new(4, "Casey", "High", 5),
        new(5, "Blair", "High", 4),
        new(6, "Alex", "High", 2),
        new(7, "Casey", "Medium", 1),
        new(8, "Blair", "Low", 2),
    ];
}
