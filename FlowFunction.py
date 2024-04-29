# -*- coding: utf-8 -*-
"""
Created on Tue Apr  2 20:14:41 2024

@author: SIMS007
"""

import numpy as np
import matplotlib.pylab as plt

def step_function(x) : 
    return np.array(x > 0, dtype = np.int32)

x = np.arange(-5.0, 5.0, 0.1)
y = step_function(x)
plt.plot(x, y)
plt.ylim(-0.1, 1.1)
plt.show()

