
import re
import numpy as np

# PARAMETER = 'speedlimit'
# MIN = 3
# MAX = 17

PARAMETER = 'carefullness'
MIN = 0.1
MAX = 1.5

values = np.linspace(MIN, MAX, 15)

with open('Autopilot.cs', 'r') as f:
    autopilot = f.read()

for value in values:
    changed_autopilot = re.sub(r'{} =[^f]*'.format(PARAMETER),
                               '{} = {}'.format(PARAMETER, value),
                               autopilot)

    filename = '{}_tests/{}.cs'.format(PARAMETER, value)
    with open(filename, 'w') as outfile:
        outfile.write(changed_autopilot)
