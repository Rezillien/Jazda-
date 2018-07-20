import csv
import os
import matplotlib.pyplot as plt
import numpy as np

plt.style.use('dark_background')

def get_statistics(data):
    car_creators_duplicated = data[:, 0]
    car_creators = list(set(car_creators_duplicated))
    statistics = {}

    for car_creator in car_creators:
        statistics[car_creator] = []

    for row in data:
        value = float(row[1])
        statistics[row[0]].append(value)

    for car_creator in statistics.keys():
        values = statistics[car_creator]
        average = np.average(values)
        statistics[car_creator] = average
    return statistics

def read_data(data):
    return [row for row in data if row and row[0] != 'log type']
    
def filter_data_by_log_type(raw_data, type):
    filtered_data = [row[1:] for row in raw_data if row[0] == type]
    return np.array(filtered_data)
    
def get_data(filename):
    with open(filename, 'r') as outfile:
        data = read_data(csv.reader(outfile, delimiter=';'))
        time_data = filter_data_by_log_type(data, 'time')
        crash_data = filter_data_by_log_type(data, 'crash')
    return time_data, crash_data

def get_car_keys(filename):
    with open(filename, 'r') as outfile:
        data = read_data(csv.reader(outfile, delimiter=';'))
        statistics = get_statistics(filter_data_by_log_type(data, 'time'))
    return statistics.keys()

class PlotValues:
    def __init__(self):
        self.values = {}
        for car_creator in car_creators:
            self.values[car_creator] = []
    
    def append(self, stat):
        for car_creator in car_creators:
            self.values[car_creator].append(stat[car_creator])

def plot(values):
    plot_sum = []
    for single_plot in values.values():
        try:
            plot_sum += np.array(single_plot)
        except:
            plot_sum = np.array(single_plot)
        plt.plot(parameter_values, single_plot)
    plt.plot(parameter_values, plot_sum / len(values), linewidth=6)
    plt.show()    

# path = 'intervalPartial'
#path = 'intervalAll'
path = 'lookangle'
datafiles = [float(filename) for filename in os.listdir(path)]
datafiles.sort()
parameter_values = []
car_creators = get_car_keys(path + '/' + str(datafiles[0]))
time_values = PlotValues()
crash_values = PlotValues()

for filename in datafiles:
    parameter_values.append(filename)
    time_data, crash_data = get_data(path + '/' + str(filename))
    time_stat = get_statistics(time_data)
    crash_stat = get_statistics(crash_data)
    time_values.append(time_stat)
    crash_values.append(crash_stat)

print(time_values.values)
print(crash_values.values)

plot(time_values.values)
plot(crash_values.values)
