
import re
import numpy as np


PARAMETER = 'Speed'
MIN = 1
MAX = 10

values = np.linspace(MIN, MAX, 15)

with open('TrueAutoPilot.cs', 'r') as f:
    autopilot = f.read()

for value in values:
    changed_autopilot = re.sub(r'{} =[^f]*'.format(PARAMETER),
                               '{} = {}'.format(PARAMETER, value),
                               autopilot)

    filename = '{}_tests/{}.cs'.format(PARAMETER, value)
    with open(filename, 'w') as outfile:
        outfile.write(changed_autopilot)
