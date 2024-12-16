from itertools import product


def open_file(filename):
    file_content = open(filename, "r").read()
    file_content = file_content.replace("\r", "")
    return file_content.split("\n")[:-1]


def main():
    lines = open_file("../input.txt")

    result = 0

    for line in lines:
        left_number, right_numbers = line.split(": ")

        left_number = int(left_number)
        right_numbers = right_numbers.split(" ")

        if possible(left_number, right_numbers):
            result += left_number

    print(result)


def compute(equation):
    result = 0

    mode = "+"
    for i in range(len(equation)):
        term = equation[i]
        if term == "*":
            mode = "*"
        elif term == "+":
            mode = "+"
        else:
            if mode == "+":
                result += int(term)
            elif mode == "*":
                result *= int(term)
            else:
                raise Exception("Unexpected term")

    return result


def possible(target_num, list_of_numbers, operators=["+", "*"]):
    operator_combinations = list(product(operators, repeat=len(list_of_numbers) - 1))

    possible_equations = []

    for operator_combination in operator_combinations:
        possible_equation = []

        for i in range(len(list_of_numbers)):
            possible_equation.append(list_of_numbers[i])

            if i < len(operator_combination):
                possible_equation.append(operator_combination[i])

        possible_equations.append(possible_equation)

    for equation in possible_equations:
        if compute(equation) == target_num:
            return True

    return False


if __name__ == "__main__":
    main()
