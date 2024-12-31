# Day 5 of Advent of Code

## Language:
Rust

## Explanations

### Part 1
1. Read each line of the file, if it contains `|` it's a rule; if it contains `,` it's an update.
2. Record the rules down into a hashmap, after recording the rules it should be

```
{
    pre: [posts...]
}
```

Where "posts" cannot precede "pre" values in updates.

3. Check each update, iterate through each row and make sure that the integers that precedes does not contain any of its "posts" value. If it does, then "violate" is set to true.
4. If it doesn't violate, then add the middle number (each update is always an odd number length long).
5. Print sum of middle number of each non violating update

### Part 2
1. Repeat Part 1 steps 1-3
2. If it violates, then bubble the current integer to precede the violating integer. Then, restart the index at 0 to check for violations again.
3. If it iterates through the lists without anymore violation, it's a valid update.
4. Print sum of middle number of each fixed update

## Thoughts:

Rust is difficult. Took me a while to realize my error at the "rotate_right" function. In the end, I strengthen my knowledge and skill in Rust.