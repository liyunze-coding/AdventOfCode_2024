# Day 6 of Advent of Code

## Language:
Python

## Explanations

### Part 1
1. Open file, split by lines
2. Left number separated from right numbers with `:`
3. Compute if it's possible to compute right numbers with `+` and `*` to obtain left number
4. If possible, add to sum
5. Print sum


### Part 2
1. Repeat 1-2 from part 1
2. Add `||` (concatenate) to `+` and `*` operators, then check if it's possible to compute right numbers to obtain left number
3. Repeat 4-5 from part 1


## Thoughts:

Did it in Python because I came back from Japan, missing several days of AoC. Wanted to speedrun, day 8 was too difficult to understand, gave up AoC as a whole.