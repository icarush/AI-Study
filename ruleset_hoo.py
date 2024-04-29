# -*- coding: utf-8 -*-
"""
Created on Wed Mar 20 01:59:56 2024

@author: SIMS007
"""

from durable.lang import *
with ruleset('hoo'):
    @when_all(c.first << (m.predicate == 'develop') & (m.object == 'system'),
              (m.predicate == 'make') & (m.object == 'service') & (m.subject == c.first.subject))
    def forg(c):
        c.assert_fact({'subject': c.first.subject, 'predicate': 'wear', 'object': 'checked shirt'}) 
        
    @when_all(c.first << (m.predicate == 'make') & (m.object == 'service'),
              (m.predicate == 'paint') & (m.object == 'texture') & (m.subject == c.first.subject))
    def chameleon(c):
        c.assert_fact({'subject': c.first.subject, 'predicate': 'use', 'object': 'Photoshop'}) 
        
    @when_all((m.predicate == 'use') & (m.object == 'Photoshop'))            
    def bird(c):
        c.assert_fact({'subject': c.m.subject, 'predicate': 'is', 'object': 'fashion leader'}) 
        
    @when_all((m.predicate == 'wear') & (m.object == 'checked shirt'))           
    def green(c):
        c.assert_fact({'subject': c.m.subject, 'predicate': 'is', 'object': 'green'}) 
        
    @when_all((m.predicate == 'is') & (m.object == 'chameleon'))           
    def grey(c):
        c.assert_fact({'subject': c.m.subject, 'predicate': 'is', 'object': 'grey'}) 
        
    @when_all((m.predicate == 'is') & (m.object == 'bird'))             
    def black(c):
        c.assert_fact({'subject': c.m.subject, 'predicate': 'is', 'object': 'black'}) 
        
    @when_all(+m.subject)
    def output(c):
        print('Fact: {0} {1} {2}'.format(c.m.subject, c.m.predicate, c.m.object)) 

assert_fact('hoo', {'subject' : 'developer', 'predicate': 'develop', 'object': 'system'}) 
assert_fact('hoo', {'subject' : 'developer', 'predicate': 'make', 'object': 'service'}) 
assert_fact('hoo', {'subject' : 'designer', 'predicate': 'make', 'object': 'service'}) 
assert_fact('hoo', {'subject' : 'designer', 'predicate': 'paint', 'object': 'texture'}) 
assert_fact('hoo', {'subject' : 'modeler', 'predicate': 'make', 'object': '3D character'}) 