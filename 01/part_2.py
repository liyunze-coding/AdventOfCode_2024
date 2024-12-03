file = open("./input.txt", "r").read().split("\n")

# print(file[0].split("   "))

left_list = []
right_list = []

for line in file:
    l, r = line.split("   ")

    l = int(l)
    r = int(r)

    left_list.append(l)
    right_list.append(r)

score = 0

for left in left_list:
    score += right_list.count(left) * left

print(score)