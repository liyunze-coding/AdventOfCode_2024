file = open("../input.txt", "r").read().split("\n")

left_list = []
right_list = []

for line in file:
    l, r = line.split("   ")

    left_list.append(int(l))
    right_list.append(int(r))

score = 0

for left in left_list:
    score += right_list.count(left) * left

print(score)
