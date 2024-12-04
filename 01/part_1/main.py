file = open("../input.txt", "r").read().split("\n")

left_list = []
right_list = []

for line in file:
    l, r = line.split("   ")

    left_list.append(int(l))
    right_list.append(int(r))

left_list.sort()
right_list.sort()

# measure the distanes
final_distance = 0

for i in range(len(left_list)):
    distance = abs(left_list[i] - right_list[i])

    final_distance += distance

print(final_distance)
