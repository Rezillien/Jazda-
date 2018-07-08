
import json

import numpy as np

PARAMETER = 'interval'
MIN = 1
MAX = 15

# PARAMETER = 'angleEnd'
# MIN = 10
# MAX = 94

values = np.linspace(MIN, MAX, 15)

with open('template.json', 'r') as f:
    data = json.load(f)

for value in values:
    for car_creator in data['carCreators']:
        if car_creator['name'] in ['Z', 'X']:
            car_creator[PARAMETER] = value

    filename = '{}_partial_tests/{}.json'.format(PARAMETER, value)
    with open(filename, 'w') as outfile:
        json.dump(data, outfile)
