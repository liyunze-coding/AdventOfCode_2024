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

func reportSafe(report []int) (bool, int) {
	increasing := false
	decreasing := false
	valid := true
	errorNum := -1

	for i := 0; i < len(report)-1; i++ {
		if report[i] == report[i+1] {
			errorNum = i
			valid = false
			break
		}

		if report[i] > report[i+1] {
			decreasing = true

			if report[i]-report[i+1] > 3 || increasing {
				valid = false
				errorNum = i
				break
			}
		}

		if report[i] < report[i+1] {
			increasing = true

			if report[i+1]-report[i] > 3 || decreasing {
				valid = false
				errorNum = i
				break
			}
		}
	}

	return valid, errorNum
}

func remove(slice []int, s int) []int {
	var newSlice []int

	// for loop, append only if the index is not specified index 's'
	for i, num := range slice {
		if i != s {
			newSlice = append(newSlice, num)
		}
	}

	return newSlice
}

func main() {
	lines := readfile("../input.txt")

	// final answer
	x := 0

	for i := 0; i < len(lines)-1; i++ {
		line := strings.Replace(lines[i], "\r", "", -1)

		line_array := strings.Split(line, " ")

		// convert each char into integer
		numArray := convertStringArrayToIntArray(line_array)

		result, _ := reportSafe(numArray)

		if result {
			x++
		} else {
			remove1Valid := false
			for k := 0; k < len(numArray); k++ {
				res, _ := reportSafe(remove(numArray, k))

				if res {
					remove1Valid = true
					break
				}
			}

			if remove1Valid {
				x++
			}
		}
	}

	fmt.Printf("%d", x)
}
