package main

import (
	"fmt"
	"os"
	"regexp"
	"strconv"
)

func readfile(filename string) string {
	data, err := os.ReadFile(filename)

	if err != nil {
		panic(err)
	}

	return string(data)
}

func main() {
	data := readfile("../input.txt")

	r := regexp.MustCompile(`mul\((\d+),(\d+)\)|don't\(\)|do\(\)`)

	matches := r.FindAllStringSubmatch(data, -1)

	res := 0
	addProduct := true

	// convert [1] [2] to int, multiply
	for _, slice := range matches {
		if slice[0] == "don't()" {
			addProduct = false
		} else if slice[0] == "do()" {
			addProduct = true
		} else if addProduct {
			num1, err := strconv.Atoi(slice[1])

			if err != nil {
				panic(err)
			}

			num2, err := strconv.Atoi(slice[2])

			if err != nil {
				panic(err)
			}

			res += num1 * num2
		}
	}

	// convert slices string -> int
	fmt.Printf("\n%d", res)
}
