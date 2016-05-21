
s = lambda x: '' if x == 1 else 's'
count = 1
print("{0} file{1} has been processed.".format(count, s(count)))

count = 2
print("{0} file{1} has been processed.".format(count, s(count)))

elements = [(2,12, "Mg"), (1,11,"Na"), (1,3,"Li"), (2,4,"Bet")]
elements.sort()
print(elements)
elements.sort(key=lambda e: (e[1], e[2]))
print(elements)
print(elements[1:3])    # slice
elements.sort(key=lambda e: (e[1:3]), reverse=True)
print(elements)
elements.sort(key=lambda e: (e[2].lower(), e[1]), reverse=False)
print(elements)


