using System.Security.Cryptography;
using System.Text;

// Complete Solve. The assignment is in LinqPracticeTasks.pdf.
// Leave the sample data, the records, and the checker unchanged.

try
{
    Report(Format(Solve(Sample.Customers(), Sample.Orders())));
}
catch (NotImplementedException ex)
{
    Console.WriteLine(ex.Message);
}

static IEnumerable<OrderLine> Solve(IReadOnlyList<Customer> customers, IReadOnlyList<Order> orders)
{
    throw new NotImplementedException("Complete the Solve method. The assignment is in LinqPracticeTasks.pdf.");
}

static string Format(IEnumerable<OrderLine> lines) =>
    string.Join("\n", lines.Select(line =>
        $"{line.Customer} | {line.City} | {line.Product} | {line.Amount}"));

static void Report(string output)
{
    const string ExpectedHash = "368732C135E48B99019E2A87D0E9967CA5899F570B29BF1CABA5A27A3D7E8594";

    var normalized = output.Replace("\r\n", "\n").TrimEnd('\n');
    Console.WriteLine(normalized);
    Console.WriteLine();
    var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(normalized)));
    Console.WriteLine(hash == ExpectedHash
        ? "Correct."
        : "Not yet correct. Compare your output with LinqPracticeTasks.pdf.");
}

record Customer(int Id, string Name, string City);

record Order(int Id, int CustomerId, string Product, int Amount);

record OrderLine(string Customer, string City, string Product, int Amount);

static class Sample
{
    public static IReadOnlyList<Customer> Customers() =>
    [
        new(1, "Nora", "Oslo"),
        new(2, "Lars", "Bergen"),
        new(3, "Mia", "Trondheim"),
        new(4, "Erik", "Oslo"),
    ];

    public static IReadOnlyList<Order> Orders() =>
    [
        new(101, 1, "Keyboard", 120),
        new(102, 1, "Mouse", 40),
        new(103, 2, "Monitor", 300),
        new(104, 3, "Desk", 250),
        new(105, 4, "Chair", 180),
        new(106, 2, "Lamp", 90),
        new(107, 4, "Headset", 150),
        new(108, 1, "Webcam", 100),
    ];
}
