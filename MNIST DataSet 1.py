# -*- coding: utf-8 -*-
"""
Created on Tue Apr 16 20:27:02 2024

@author: SIMS007
"""

import sys, os
sys.path.append(os.pardir)
# from keras.mnist import load_mnist

from keras.datasets import mnist
import numpy as np

(x_train, t_train), (x_test, t_test) = \
mnist.load_data()
#load_mnist(flatten = True, normalize = False)



print(x_train.shape)
print(t_train.shape)
print(x_test.shape)
print(t_test.shape)

