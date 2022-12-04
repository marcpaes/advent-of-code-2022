// PART 1
var data = File.ReadAllText("./input.txt");

var elfs = data
    .Split("\n\n")
    .Select((x, ix) =>
    {
        return
            new Elf(x
                    .Split("\n")
                    .Sum(item => string.IsNullOrWhiteSpace(item) ?
                        0 :
                        int.Parse(item.Trim())), ix);
    });

var maxCalories = elfs.Max(x => x.calories);

Console.WriteLine("O maximo de calorias de um elfo é {0}", maxCalories);

//Part 2
var sum_of_top_three = elfs
    .OrderByDescending(x => x.calories)
    .Take(3)
    .Sum(x => x.calories);

Console.WriteLine("A soma dos tres elfos com mais calorias é {0}", sum_of_top_three);

record Elf(int calories, int id);
