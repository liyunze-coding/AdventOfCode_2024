# Day 4 of Advent of Code

## Language:
Typescript (Bun)

`bun run index.ts`

## Explanations

### Part 1
1. Split the file text content into a matrix of characters
2. Find coordinates of "X"
3. Use coordinates of "X" to find "XMAS" in 8 directions
4. For each "XMAS" found, increment `totalCount` by 1
5. Display `totalCount`

### Part 2
1. Find all coordinates of "A"
2. Find if it spells "MAS" or "SAM" diagonally
3. For each X-MAS found, increment `totalCount`
4. Display `totalCount`

## Thoughts:

Initially I wanted to complete today's AoC challenge in Rust, however I had difficulty with the number types "usize", "i32" and checking out of bounds. Switched to typescript and solved the puzzle relatively quickly.

It did help me learn more about Rust, I will try to do Rust for day 5's challenge.