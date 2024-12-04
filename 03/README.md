# Day 3 of Advent of Code

## Language:
Go

## Explanations

### Part 1
1. Read file
2. Find all matching substrings using regex (find `mul([integer],[integer])`)
3. Convert some groups in matched regex to integers
4. Multiply the integers and add to final result
5. Display final result

### Part 2
1. Read file
2. Find all matching substrings using a new regex (`mul([integer],[integer])`, `do()` OR `don't()`) 
3. Declare a boolean variable `addProduct` as true
4. Do a for loop over matched substrings
5. If substring is "don't()", change `addProduct` to `false`
6. If substring is "do()", change `addProduct` to `true`
7. Otherwise, substring is `mul([integer],integer)`. Only add the product of the integers if `addProduct` is `true`
8. Display final sum of products

## Thoughts:
Continue practicing the Go language. I used [regex101](https://regex101.com/) to test regex.

For part 2, I initially thought about eliminating everything between "don't()" and "do()" until someone pointed out some problems including "what if there is no do() after don't()" and suggested to use or (`|`) in regex instead.

The suggestion has helped me learn better approaches with regex.