public static class Program
{

    public static (string, int) FindSequence(string input, int consecutiveDifferentNumbers)
    {
        for (var i = consecutiveDifferentNumbers; i < input.Length; i++)
        {
            var sequence = input[(i - consecutiveDifferentNumbers)..i];
            if (sequence.Distinct().Count() == consecutiveDifferentNumbers)
            {
                return (sequence, i);
            }
        }

        return ("", -1);
    }

    public static void Main()
    {
        var input = File.ReadAllText("./input.txt");

        // Part 1
        var (sequence, position) = FindSequence(input, 4);
        Console.WriteLine("Found {0} at {1}", sequence, position);

        // Part 2
        var (message, position2) = FindSequence(input, 14);
        Console.WriteLine("Found {0} at {1}", message, position2);
    }
}
