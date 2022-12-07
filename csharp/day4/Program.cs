public static class Program
{
    public record Interval(int start, int end);

    public record Pair(Interval interval1, Interval interval2);

    public static bool IsIn(this Interval interval1, Interval interval2)
    {
        return interval1.start >= interval2.start && interval1.end <= interval2.end;
    }

    // 14-17 18-40
    public static bool HasOverlap(this Interval interval1, Interval interval2)
    {
        return (interval1.start >= interval2.start && interval1.start <= interval2.end) ||
               (interval1.end <= interval2.end && interval1.start >= interval2.end);
    }

    public static void Main()
    {
        var lines = File.ReadAllText("./input.txt").Split("\n");

        var pairs = lines
            .Where(l => !String.IsNullOrWhiteSpace(l))
            .Select(l => l.Split(",").Select(p => p.Split("-").ToArray()).ToArray())
            .Select(p => new Pair(
                        new Interval(int.Parse(p[0][0]), int.Parse(p[0][1])),
                        new Interval(int.Parse(p[1][0]), int.Parse(p[1][1]))
                        )
                    );


        //part 1
        var pairWithContainedIntervals =
            pairs
                .Where(p => p.interval1.IsIn(p.interval2) || p.interval2.IsIn(p.interval1))
                .Count();

        Console.WriteLine("Part1 - O numero de pares com intervalos contidos em outro é {0}", pairWithContainedIntervals);

        //part 2
        var overlapedPairs =
            pairs
                .Where(p => p.interval1.HasOverlap(p.interval2) || p.interval2.HasOverlap(p.interval1))
                .Count();

        Console.WriteLine("Part2 - O numero de pares com intervalos com overlap é {0}", overlapedPairs);
    }
}
