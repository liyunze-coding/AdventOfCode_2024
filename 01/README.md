# Day 1 of Advent of Code

## Language:
Python version 3.13

## Explanations

### Part 1
1. Split each row by 3 spaces `"   "`
2. Convert the remaining characters to integers
3. Push the integers into `left_list` and `right_list`
4. Sort the lists in ascending order
5. Subtract and get the absolute distance (always positive), add it to total distance
6. Display total distance

### Part 2
1. Use the same `left_list` and `right_list` variables from part 1
2. Add the frequency of the left integer on `right_list` to final sum
3. Display final sum

## Thoughts:
I only did it in Python because I am still very familiar with its syntax and have not done any programming puzzles in a long time. I did not take advantage of any Python v13 features.

