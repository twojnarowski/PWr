# Python-AI-Algorithms
## Problem:
Two sets of points are generated in two groups. Positive which should be above a polynomial and negative which should be under the same polynomial. The task is to find a polynomial which will fit between these two sets of points.
## Individual:
An individual in my genetic algorithm is a polynomial represented in a list with the following values:
#poly[0] = a
#poly[1] = b
#poly[2] = c
#poly[3] = id
Where a, b and c are the coefficients of a polynomial in the following formula: a*x^2 + b*x + c and id is the identification used for sorting in the algorithm.
## Fitness function:
The fitness function in my genetic algorithm firstly checks the polynomial against all given points and calculates how many of the points are in the right place, that being the positive points above the polynomial and negative under the polynomial. The score is represented as the percentage of points that are at the right place.
## Crossover operator:
The crossover in my genetic algorithm is done in the following way: Two breeders, taken from the beginning and middle of the population sorted  by the fitness function produce a given number of children, with children having randomly assigned values of a, b and c from two possible choices that are the values their breeders had. Next the second individual from the beginning of the population and the next one after the middle of the population are chosen and so on.
## Mutation Operator:
In mutation in my genetic algorithm, some individuals are selected with a given probability, and these individuals have one of the values (a, b or c) mutated by changing one of the binary bits to its opposite.
## Selection operator:
In my genetic algorithm the selection is separated into two parts, firstly the given number of best resulting individuals are selected into the new population from the beginning of the current sorted population.  Later a given number of other individuals are randomly selected from the remaining population.
## Implementation:
The algorithm has been implemented in python:
## Results for 2nd degree:
 ![alt text](https://i.imgur.com/z5qrtGk.png)

Average fitness score from 10 independent runs: 93,31%
## Results for 3rd degree:
 ![alt text](https://i.imgur.com/O8X2CAb.png)

## Results for 4th degree (with mixed points):
![alt text](https://i.imgur.com/rLvqYVZ.png)
  
## Influence of changes in probability of mutation, crossover, selection operator and population size and generations number:
In my genetic algorithm change in the probability of mutation, the number of children made during crossover, and the number of selected individuals from population do not change the workings of the algorithm as it reaches the same high score each time.
A higher number of population means only that there is a higher chance of a high score in the first population. 
The change in number of populations only slightly improve the outcome.
## Observations and findings:
This genetic algorithm has no problem in finding the best fitting polynomial while the points are properly distributed. Sometimes the algorithm is capable of reaching 100% fitness score and usually the result is somewhere around 80%-90%. The cause of that is most probably the location of points generated on the graph. 


