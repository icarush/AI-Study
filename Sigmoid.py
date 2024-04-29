# -*- coding: utf-8 -*-
"""
Created on Tue Apr  2 20:19:09 2024

@author: SIMS007
"""
import numpy as np
import matplotlib.pylab as plt

def sigmoid(x):
    return 1 / (1 + np.exp(-x))
"""
>>> x = np.array([-1.0, 1.0, 2.0])
>>> sigmoid(x)
array([0.26894142, 0.73105858, 0.88079708])

>>> x = np.array([1.0, 1.0, 2.0])
>>> sigmoid(x)
array([0.26894142, 0.73105858, 0.88079708])
"""

x = np.arange(-5.0, 5.0, 0.1)
y = sigmoid(x)
plt.plot(x, y)
plt.ylim(-0.1, 1.1)
plt.show()