# -*- coding: utf-8 -*-
"""
Created on Tue Apr  2 20:35:49 2024

@author: SIMS007
"""

import numpy as np

A = np.array([[1,2], [3,4]])
print(A.shape)

B = np.array([[5,6], [7,8]])
print(B.shape)

print(np.dot(A,B))

C = np.array([[1,2,3], [4,5,6]])
print(C.shape)

D = np.array([[1,2], [3,4], [5,6]])
print(D.shape)

print(np.dot(C,D))