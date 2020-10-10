from datetime import date
d0 = date(2020, 1, 1)
d1 =  date.today()
delta = d1 - d0
print(delta.days)