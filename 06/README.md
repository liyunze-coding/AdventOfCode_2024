# Day 6 of Advent of Code

## Language:
C#

## Explanations

### Part 1
1. Read file, split by each line
2. Treat the map like a matrix form
3. Set current direction up
4. While the guard is in the map, check if there's obstacles ahead or guard went off the map
5. If obstacle ahead, update direction (up -> right -> down -> left -> up...)
6. If guard went off the map, end while loop
7. Count number of "X"s on the map
8. Print output 


### Part 2
1. Steps 1-3 of Part 1
2. Journal the steps (coordinates) the guard took
3. Iterate through the steps and place an obstacle there
4. Save the coordinates and direction every time the guard takes a step after placing an obstacle
5. If the guard walked through the same coordinate facing the direction as before, it is in a loop
6. If the guard walks out the map, it is not in a loop
7. Record number of times the guard ends up in a loop
8. Print output


## Thoughts:

I did not need to draw a map. At least I still remember how to code in C# from my OOP class from just months ago.