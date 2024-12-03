file = open("./input.txt", "r").read().split("\n")

# print(file[0].split("   "))

left_list = []
right_list = []

for line in file:
    l, r = line.split("   ")
    print(l,r)
    l = int(l)
    r = int(r)

    left_list.append(l)
    right_list.append(r)

left_list.sort()
right_list.sort()

# measure the distanes
distances = []

for i in range(len(left_list)):
    distance = left_list[i] - right_list[i]

    if distance < 0:
        distance = distance * -1
    distances.append(distance)

print(sum(distances))