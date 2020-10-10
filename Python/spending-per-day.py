from datetime import datetime

today = datetime.today()
if today.month<12 :
    target = datetime(today.year, today.month + 1, 1)
else :
    target = datetime(today.year+1, 1, 1)

val = int(input("Enter your value: "))


print(val/(target -  datetime.today()).days)

    