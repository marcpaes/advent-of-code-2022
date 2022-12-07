public class Program
{
    public static int GetPriority(char letter)
    {
        return letter >= 97 ? letter - 96 : letter - 65 + 27;
    }
    public static void Main(string[] args)
    {
        var lines = File
            .ReadAllText("./input.txt")
            .Split("\n");

        // part 1
        var items = lines
            .Select(x => new
            {
                itens = x,
                half1 = x[0..(x.Length / 2)],
                half2 = x[(x.Length / 2)..],
            })
            .Select(x => new
            {
                repeatedItem =
                    x.half1
                        .ToCharArray()
                        .Where(h1 =>
                            x.half2
                                .ToCharArray()
                                .Contains(h1))
                        .Distinct()
            })
            .SelectMany(x => x.repeatedItem);

        var prioritySum = items.Sum(x => GetPriority(x));

        Console.WriteLine(prioritySum);

        // part 2
        var items2 = lines
            .Chunk(3)
            .Select(x => new
            {
                Elf1 = x.Length >= 1 ? x[0].ToCharArray() : Array.Empty<char>(),
                Elf2 = x.Length >= 2 ? x[1].ToCharArray() : Array.Empty<char>(),
                Elf3 = x.Length >= 3 ? x[2].ToCharArray() : Array.Empty<char>()
            })
            .Select((x, ix) => new
            {
                Elf = ix,
                repeatedItens = x.Elf1.Where(el => x.Elf2.Contains(el) && x.Elf3.Contains(el)).Distinct()
            })
            .SelectMany(x => x.repeatedItens);

        var prioritySum2 = items2.Sum(x => GetPriority(x));

        Console.WriteLine(prioritySum2);
    }
}
