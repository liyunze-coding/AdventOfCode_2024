package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

func readfile(filename string) []string {
	data, err := os.ReadFile(filename)

	if err != nil {
		panic(err)
	}

	lines := strings.Split(string(data), "\n")

	return lines
}

func convertStringArrayToIntArray(stringArray []string) []int {
	var t2 = []int{}

	for _, i := range stringArray {
		num, err := strconv.Atoi(i)
		if err != nil {
			panic(err)
		}

		t2 = append(t2, num)
	}

	return t2
}

func reportSafe(report []int) bool {
	increasing := false
	decreasing := false
	valid := true

	for i := 0; i < len(report)-1; i++ {
		if report[i] == report[i+1] {
			valid = false
			break
		}

		if report[i] > report[i+1] {
			decreasing = true

			if report[i]-report[i+1] > 3 || increasing {
				valid = false
				break
			}
		}

		if report[i] < report[i+1] {
			increasing = true

			if report[i+1]-report[i] > 3 || decreasing {
				valid = false
				break
			}
		}
	}

	return valid
}

func main() {
	// returns slice of file content by each line
	lines := readfile("../input.txt")

	// x = final answer
	x := 0

	for i := 0; i < len(lines)-1; i++ {
		// replace all \r with ""
		line := strings.Replace(lines[i], "\r", "", -1)

		// each line, split by " "
		line_array := strings.Split(line, " ")

		// convert each char into integer
		numArray := convertStringArrayToIntArray(line_array)

		if reportSafe(numArray) {
			x++
		}
	}

	fmt.Printf("%d", x)
}
