
import csv
import os
import matplotlib.pyplot as plt
import numpy as np

plt.style.use('dark_background')


def get_time_statistics(filename):
    with open(filename, 'r') as outfile:
        data = csv.reader(outfile, delimiter=';')
        time_data = [row[1:] for row in data
                     if row and row[0] == 'time']
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


path = 'intervalPartial'
# path = 'intervalAll'
# path = 'lookangle'
datafiles = [float(filename) for filename in os.listdir(path)]
datafiles.sort()
parameter_values = []
time_values = {}
car_creators = get_time_statistics(path + '/' + str(datafiles[0])).keys()
for car_creator in car_creators:
    time_values[car_creator] = []

for filename in datafiles:
    parameter_values.append(filename)
    stat = get_time_statistics(path + '/' + str(filename))
    for car_creator in time_values.keys():
        time_values[car_creator].append(stat[car_creator])

plot_sum = []
for single_plot in time_values.values():
    try:
        plot_sum += np.array(single_plot)
    except:
        plot_sum = np.array(single_plot)
    plt.plot(parameter_values, single_plot)
plt.plot(parameter_values, plot_sum / len(time_values), linewidth=6)
plt.show()
