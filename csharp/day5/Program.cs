using System.Text.RegularExpressions;

public static class Program
{
    public static Stack<string>[] CreateInitialStacks(string[] stackString)
    {
        var cratesText = stackString.Reverse();

        var stacks = new Stack<string>[9];
        for (int i = 0; i < 9; i++)
            stacks[i] = new Stack<string>();


        var pattern = "[a-zA-Z]";
        var rgFindLetter = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled);

        foreach (var line in cratesText)
        {
            var matches = rgFindLetter.Matches(line);
            foreach (Match match in matches)
            {
                var stackIndex = (match.Index - 1) / 4;
                stacks[stackIndex].Push(match.Value);
            }
        }

        return stacks;
    }

    public static void Main()
    {
        Console.WriteLine("Day5!");

        var file = File.ReadAllLines("./input.txt");

        var movements = file[10..];

        //Part 1
        var stacks = CreateInitialStacks(file[..8]);

        foreach (var movString in movements)
        {
            var digitPattern = @"\d+";
            var rgFindDigit = new Regex(digitPattern, RegexOptions.Singleline | RegexOptions.Compiled);

            var matches = rgFindDigit.Matches(movString).ToArray();

            var qtyToMove = int.Parse(matches[0].Value);
            var startStack = int.Parse(matches[1].Value) - 1;
            var endStack = int.Parse(matches[2].Value) - 1;

            for (int i = 0; i < qtyToMove; i++)
            {
                var item = stacks[startStack].Pop();
                stacks[endStack].Push(item);
            }
        }

        var resultPart1 = "";
        foreach (var stack in stacks)
        {
            resultPart1 += stack.Peek();
        }

        Console.WriteLine("Part1: O topo de cada pilha é: {0}", resultPart1);

        // Part 2

        var newstacks = CreateInitialStacks(file[..8]);

        foreach (var movString in movements)
        {
            var digitPattern = @"\d+";
            var rgFindDigit = new Regex(digitPattern, RegexOptions.Singleline | RegexOptions.Compiled);

            var matches = rgFindDigit.Matches(movString).ToArray();

            var qtyToMove = int.Parse(matches[0].Value);
            var startStack = int.Parse(matches[1].Value) - 1;
            var endStack = int.Parse(matches[2].Value) - 1;

            var batch = new List<string>();
            for (int i = 0; i < qtyToMove; i++)
            {
                var item = newstacks[startStack].Pop();
                batch.Add(item);
            }

            batch.Reverse();
            foreach (var item in batch)
            {
                newstacks[endStack].Push(item);
            }
        }

        var resultPart2 = "";
        foreach (var stack in newstacks)
        {
            resultPart2 += stack.Peek();
        }

        Console.WriteLine("Part2: O topo de cada pilha é: {0}", resultPart2);


    }
}
