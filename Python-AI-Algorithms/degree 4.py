import random
import operator
import matplotlib
import matplotlib.pyplot as plt
import numpy as np
from random import randrange

#punkt - koordynaty x, y, oraz grupa 0 lub 41
# point[0] = x
# point[1] = y
# point[2] = grupa
#linia - 2nd degree, wiec wartosci a, b i c we wzorze y = ax^3 + bx^2  + cx + d
#poly[0] = a
#poly[1] = b
#poly[2] = c
#poly[3] = d
#poly[4] = e
#poly[5] = id
#


def fitness(points, poly):
    score1 = 0
    score2 = 0
    total = 0
    for point in points:
        if point[2]==0:
            if point[1] > poly[0] * point[0] ** 4 + poly[1] * point[0]**3 + poly[2]*point[0]**2+poly[3]*point[0]+poly[4]:
                score1 += 1
            total += 1
        elif point[2]==1:
            if  point[1] < poly[0] * point[0] ** 4 + poly[1] * point[0]**3 + poly[2]*point[0]**2+poly[3]*point[0]+poly[4]:
                score2 += 1
            total += 1
    return (score1+score2)*100/total

def generatePolynomial(id):
        i = 0
        poly = [0,0,0, 0,0, id]
        while i < 5:
            poly[i] = int(10 * random.random()+1)
            i += 1
        return poly

def generateFirstPopulation(sizepopulation):
        population = []
        i = 0
        while i < sizepopulation:
            population.append(generatePolynomial(i))
            i += 1
        return population


def computePerfPopulation(population, points):
    populationperf = {}
    for individual in population:
        populationperf[individual[5]]=  (fitness(points, individual))
    return sorted(populationperf.items(), key = operator.itemgetter(1), reverse=True)

def selectFromPopulation(population, populationSorted, best_sample, lucky_few):
    nextGeneration = []
    if best_sample > len(populationSorted)-1:
        best_sample = len(populationSorted)-1
    else:
        best_sample = best_sample - 1
    for i in range(0, best_sample):
        nextGeneration.append(population[populationSorted[i][0]])
    if lucky_few > len(populationSorted)-1:
        lucky_few = len(populationSorted)-1
    else:
        lucky_few = lucky_few - 1
    for j in range(0, lucky_few):
        dlug = len(populationSorted) - 1
        outcome = random.randint(0,dlug)
        nextGeneration.append(population[populationSorted[outcome][0]])
    random.shuffle(nextGeneration)
    return nextGeneration

def createChild(poly1, poly2, id):
    child = []
    for i in range(0,5):
        if int(100 * random.random()) < 50 :
            child.append(poly1[i])
        else:
            child.append(poly2[i])
    child.append(id)
    return child

def createChildren(breeders, number_of_child):
    id = 30
    for i in range(int(len(breeders) / 2)):
        for j in range(number_of_child):
            breeders.append(createChild(breeders[i], breeders[int(len(breeders) / 2) + i], id + 5 * i - j))
    return breeders

def mutatePoly(poly):
    poly[int(random.random() * 5)] = randrange(1,10)
    return poly


def mutatePopulation(population, chance_of_mutation):
    #mutowanie populacji
    for i in range(len(population)):
        if random.random() * 100 < chance_of_mutation:
            population[i] = mutatePoly(population[i])
    return population


gen = 0
points = []
rand1 = 0
rand2 = 0
i=0
for j in range(0, 30):
    {
        points.append([int(random.random()*10), int(random.random()*10000) , 1])
    }
for j in range(0, 30):
    {
        points.append([int(random.random()*10), int(random.random() * 10000), 0])
    }


pop = generateFirstPopulation(100)
popsort = computePerfPopulation(pop, points)
while fitness(points, pop[popsort[0][0]])<100 and gen < 100:
    gen+=1
    print(gen)
    pop = selectFromPopulation(pop, popsort, 20, 10)
    pop = createChildren(pop, 5)
    pop = mutatePopulation(pop, 10)
    popsort = computePerfPopulation(pop, points)
print (pop[popsort[0][0]])
print (popsort[0])
print ("gen: " + str(gen))
result = pop[popsort[0][0]]


# Data for plotting
t = np.arange(0, 10, 1)
s = result[0]*(t**4) + result[1]*(t**3) + result[2]*(t**2)+result[3]*t + result[4]

fig, ax = plt.subplots()
ax.plot(t, s)
for point in points:
    if point[2] == 1:
        plt.plot([point[0]],[point[1]],'ro')
    else:
        plt.plot([point[0]],[point[1]], 'bs')
ax.set(xlabel='x', ylabel='y',
       title='ga')
ax.grid()

fig.savefig("test.png")
plt.show()
