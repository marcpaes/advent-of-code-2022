use std::fs;
use std::str::FromStr;

fn main() {
    let lines = fs::read_to_string("input.txt").expect("Error reading de file");

    let mut elf_inventories: Vec<u32> = lines
        .split("\n\n")
        .map(|i| {
            i.lines()
                .map(|s| u32::from_str(s).expect("Error parsing int"))
                .sum()
        })
        .collect();

    //Part 1
    let sum = elf_inventories.iter().max().unwrap();
    println!("A soma total é: {}", sum);

    //Part 2
    elf_inventories.sort_by(|a, b| b.cmp(a));

    let top_three_sum: u32 = elf_inventories.iter().take(3).sum();

    println!("A soma dos 3 elfos com mais calorias é: {}", top_three_sum);
}
