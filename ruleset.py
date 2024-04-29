# -*- coding: utf-8 -*-
"""
Created on Tue Mar 19 23:05:16 2024

@author: SIMS007
"""



"""
from durable.lang import *
with ruleset('testRS'):
    @when_all(m.subject == 'World')
    def say_hello(c):
        print('Hello {0}'.format(c.m.subject))
        
post('testRS', {'subject' : 'World'})

"""

from durable.lang import *
with ruleset('animal'):
    @when_all(c.first << (m.predicate == 'eats') & (m.object == 'flies'),
              (m.predicate == 'lives') & (m.object == 'water') & (m.subject == c.first.subject))
    def forg(c):
        c.assert_fact({'subject': c.first.subject, 'predicate': 'is', 'object': 'frog'}) 3
        
    @when_all(c.first << (m.predicate == 'eats') & (m.object == 'flies'),
              (m.predicate == 'lives') & (m.object == 'land') & (m.subject == c.first.subject))
    def chameleon(c):
        c.assert_fact({'subject': c.first.subject, 'predicate': 'is', 'object': 'chameleon'}) 7
        
    @when_all((m.predicate == 'eats') & (m.object == 'worms'))            
    def bird(c):
        c.assert_fact({'subject': c.m.subject, 'predicate': 'is', 'object': 'bird'}) 10
        
    @when_all((m.predicate == 'is') & (m.object == 'frog'))           
    def green(c):
        c.assert_fact({'subject': c.m.subject, 'predicate': 'is', 'object': 'green'}) 2 
        
    @when_all((m.predicate == 'is') & (m.object == 'chameleon'))           
    def grey(c):
        c.assert_fact({'subject': c.m.subject, 'predicate': 'is', 'object': 'grey'}) 6
        
    @when_all((m.predicate == 'is') & (m.object == 'bird'))             
    def black(c):
        c.assert_fact({'subject': c.m.subject, 'predicate': 'is', 'object': 'black'}) 9
        
    @when_all(+m.subject)
    def output(c):
        print('Fact: {0} {1} {2}'.format(c.m.subject, c.m.predicate, c.m.object)) 

assert_fact('animal', {'subject' : 'Kermit', 'predicate': 'eats', 'object': 'flies'}) 1
assert_fact('animal', {'subject' : 'Kermit', 'predicate': 'lives', 'object': 'water'}) 4
assert_fact('animal', {'subject' : 'Greedy', 'predicate': 'eats', 'object': 'flies'}) 5
assert_fact('animal', {'subject' : 'Greedy', 'predicate': 'lives', 'object': 'land'}) 8
assert_fact('animal', {'subject' : 'Tweety', 'predicate': 'eats', 'object': 'worms'}) 11
"""

from durable.lang import *
with ruleset('risk'):
    @when_all(c.first << m.t == 'purchase', c.second << m.location != c.first.location) 
    def fraud(c):
        print('이상거래 탐지 -> {0} {1}'.format(c.first.location, c.second.location))
        
post('risk', {'t': 'purchase', 'location': 'US'})
post('risk', {'t': 'purchase', 'location': 'CA'})


"""
"""
from durable.lang import *
with ruleset('bookstore'):
    @when_all(+m.subject)
    def event(c):
        print('bookstore-> Reference {0} status {1}'.format(c.m.reference, c.m.status))
        
    @when_all(+m.name)
    def fact(c):
        print('bookstore-> Added "{1}"'.format(c.m.name))
        
    @when_all(none(+m.name))
    def empty(c):
        print('bookstore-> No books')

assert_fact('bookstore', {'name' : 'The new book', 'seller': 'bookstore', 'reference': '75323', 'price':500})

try:
    assert_fact('bookstore', {'reference' : 75323, 'name' : 'The new book', 'price':500, 'price':500, 'seller': 'bookstore'})
except BaseException as e:
    print('Error: {0}'.format(e.message))
    
post('bookstore', {'reference' : 75323, 'status' : 'Active'})
assert_fact('bookstore', {'reference' : 75323, 'name' : 'The new book', 'price':500, 'price':500, 'seller': 'bookstore'})
"""

#!pip install durable_rules


















