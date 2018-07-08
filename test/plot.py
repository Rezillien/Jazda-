
import csv
import matplotlib.pyplot as plt
import numpy as np


# PARAMETER = 'interval'
# MIN = 1
# MAX = 15

PARAMETER = 'angleEnd'
MIN = 10
MAX = 94

# values = np.linspace(MIN, MAX, 15)


def get_time_statistics(filename):
    with open(filename, 'r') as outfile:
        data = csv.reader(outfile, delimiter=';')
        time_data = [row[1:] for row in data if row[0] == 'time']
        time_data = np.array(time_data)

    car_creators_duplicated = time_data[:, 0]
    car_creators = list(set(car_creators_duplicated))
    statistics = {}

    for car_creator in car_creators:
        statistics[car_creator] = []

    for row in time_data:
        time_value = float(row[1])
        statistics[row[0]].append(time_value)

    for car_creator in statistics.keys():
        values = statistics[car_creator]
        average = np.average(values)
        statistics[car_creator] = average

    return statistics


print(get_time_statistics('../Assets/Statistics/log'))


# plt.plot(values, values**2)
# plt.show()
