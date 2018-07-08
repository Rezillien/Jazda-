
import matplotlib.pyplot as plt
import numpy as np


# PARAMETER = 'interval'
# MIN = 1
# MAX = 15

PARAMETER = 'angleEnd'
MIN = 10
MAX = 94

values = np.linspace(MIN, MAX, 15)

plt.plot(values, values**2)

plt.show()
