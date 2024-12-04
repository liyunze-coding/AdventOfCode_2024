# Day 2 of Advent of Code

## Language:
Go

## Explanations

### Part 1
1. Read file
2. Declare initial score (`x`) as 0
3. Iterate through each line, processing the numbers
4. If the line fulfills conditions (report is safe), increment `x` by 1.

### Part 2
1. Repeat steps 1-4
2. If report is not safe, run a for loop that removes 1 element from the array at a time
3. If the for-loop finds any safe reports after removing 1 element, the report is considered safe and `x` is incremented by 1. Otherwise x will not be affected.

## Thoughts:
I completed day 2 of AoC using Go to practice the language. Initially I made it so the function returns which index of the slice caused the report to not be safe, and I would try to remove 1 of the elements between `n-1`,`n` and `n+1` where `n` is the returned index, to check if the report would be safe. 

I did not obtain the correct answer, and switched to using a for-loop instead, which resulted in the correct answer.