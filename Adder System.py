# -*- coding: utf-8 -*-
"""
Created on Tue Mar 19 19:28:47 2024

@author: SIMS007
"""

# -*- coding: utf-8 -*-
"""
Created on Tue Mar 19 19:28:47 2024

@author: SIMS007
"""

"""
import numpy as np
def AND1(x1, x2):
    w1, w2, theta = 0.5, 0.5, 0.7
    tmp = x1*w1 + x2*w2
    if tmp <= theta:
        return 0
    elif tmp > theta:
        return w1

def AND2(x1, x2):
    x = np.array([x1, x2])
    w = np.array([0.5, 0.5])
    b = -0.7
    tmp = np.sum(w*x) + b
    if tmp <= 0:
        return 0
    else:
        return 1

def NAND(x1, x2):
    x = np.array([x1, x2])
    w = np.array([-0.5, -0.5])
    b = 0.7
    tmp = np.sum(w*x) + b
    if tmp <= 0:
        return 0
    else:
        return 1
def OR(x1, x2):
    x = np.array([x1, x2])
    w = np.array([0.5, 0.5])
    b = -0.2
    tmp = np.sum(w*x) + b
    if tmp <= 0:
        return 0
    else:
        return 1
    
def XOR(x1, x2):
    s1 = NAND(x1, x2)
    s2 = OR(x1, x2)
    y = AND2(s1, s2)
    return y

def halfAdder(x1, x2):
    s = XOR(x1, x2)
    c = AND2(x1, x2)
    return s, c
  
print(halfAdder(0, 0))
print(halfAdder(1, 0))
print(halfAdder(0, 1))
print(halfAdder(1, 1))
"""



import numpy as np
class Perceptron:
    def AND1(self, x1, x2):
        w1, w2, theta = 0.5, 0.5, 0.7
        tmp = x1*w1 + x2*w2
        if tmp <= theta:
            return 0
        elif tmp > theta:
            return w1     
        
    def AND2(self, x1, x2):
        x = np.array([x1, x2])
        w = np.array([0.5, 0.5])
        b = -0.7
        tmp = np.sum(w*x) + b
        if tmp <= 0:
            return 0
        else: 
            return 1
        
    def NAND(self, x1, x2):
        x = np.array([x1, x2])
        w = np.array([-0.5, -0.5])
        b = 0.7
        tmp = np.sum(w*x) + b
        if tmp <= 0:
            return 0
        else:
            return 1
        
    def OR(self, x1, x2):
        x = np.array([x1, x2])
        w = np.array([0.5, 0.5])
        b = -0.2
        tmp = np.sum(w*x) + b
        if tmp <= 0:
            return 0
        else:
            return 1
    
    def XOR(self, x1, x2):
        s1 = self.NAND(x1, x2)
        s2 = self.OR(x1, x2)
        y = self.AND2(s1, s2)
        return y
    
    def halfAdder(self, x1, x2):
        s = self.XOR(x1, x2)
        c = self.AND2(x1, x2)
        return s, c
    
    def fullAdder(self, x1, x2, cin):
        s = self.XOR(x1, x2)
        ss = self.XOR(s, cin)
        aa = self.AND2(cin, s) 
        c = self.AND2(x1, x2)
        cc = self.OR(aa, c) 
        
        return cc, ss

pt  = Perceptron()

print(pt.fullAdder(0, 0, 0))
print(pt.fullAdder(0, 0, 1))
print(pt.fullAdder(0, 1, 0))
print(pt.fullAdder(0, 1, 1))
print(pt.fullAdder(1, 0, 0))
print(pt.fullAdder(1, 0, 1))
print(pt.fullAdder(1, 1, 0))
print(pt.fullAdder(1, 1, 1))
"""
print(pt.halfAdder(0, 0))
print(pt.halfAdder(1, 0))
print(pt.halfAdder(0, 1))
print(pt.halfAdder(1, 1))
"""
