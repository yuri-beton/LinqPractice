using System.Globalization;
using System.Security.Cryptography;
using System.Text;

// Complete Solve. The assignment is in LinqPracticeTasks.pdf.
// Leave the sample data, the records, and the checker unchanged.

try
{
    Report(Format(Solve(Sample.Employees())));
}
catch (NotImplementedException ex)
{
    Console.WriteLine(ex.Message);
}

static PayrollSnapshot Solve(IReadOnlyList<Employee> employees)
{
    return employees.GroupBy(e => e.Department)
        .Where(e => e.Key == "Sales")
        .Select(g => new PayrollSnapshot
        (
            g.Count(), 
            g.Sum(e => e.Salary), 
            g.Average(e => e.Salary), 
            g.OrderBy(e => e.Salary).ThenBy(e => e.Name).Select(e => e.Name).First())
        ).First();
}

static string Format(PayrollSnapshot snapshot) =>
    string.Join("\n",
    [
        $"Count: {snapshot.Count}",
        $"Total: {snapshot.Total.ToString("F2", CultureInfo.InvariantCulture)}",
        $"Average: {snapshot.Average.ToString("F2", CultureInfo.InvariantCulture)}",
        $"Lowest paid: {snapshot.LowestPaid}",
    ]);

static void Report(string output)
{
    const string ExpectedHash = "2F0CE3FD46F5C5DA7F9900504F21047A726103A103BB147F07F1DE3A11AFFF06";

    var normalized = output.Replace("\r\n", "\n").TrimEnd('\n');
    Console.WriteLine(normalized);
    Console.WriteLine();
    var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(normalized)));
    Console.WriteLine(hash == ExpectedHash
        ? "Correct."
        : "Not yet correct. Compare your output with LinqPracticeTasks.pdf.");
}

record Employee(string Name, string Department, decimal Salary);

record PayrollSnapshot(int Count, decimal Total, decimal Average, string LowestPaid);

static class Sample
{
    public static IReadOnlyList<Employee> Employees() =>
    [
        new("Ann", "Sales", 52_000m),
        new("Ben", "Engineering", 90_000m),
        new("Cara", "Sales", 48_000m),
        new("Dan", "Sales", 61_000m),
        new("Eve", "Engineering", 85_000m),
        new("Finn", "Sales", 48_000m),
    ];
}
