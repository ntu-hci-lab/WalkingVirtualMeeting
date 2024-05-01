import matplotlib.pyplot as plt
import numpy as np
import csv

# application[task][position][env]
application = [[[[] for x in range(2)] for y in range(5)] for z in range(3)]

taskIndex = {
    "facebook" : 0,
    "medium" : 1,
    "youtube" : 2
}

# 0: top, 1: middle, 2: bottom, 3: side, 4: bottom-side
positionGroup = {
    1 : 0,
    2 : 0,
    3 : 0,
    4 : 3,
    5 : 1,
    6 : 3,
    7 : 4,
    8 : 2,
    9 : 4
}

envIndex = {
    "Campus" : 0,
    "SideWalk" : 1
}

with open('user_fav_label.csv', mode ='r')as file:
    reader = csv.DictReader(open('user_fav_label.csv'))
    for row in reader:
        if row["UserID"] != "":
            relPosition = row["relPosition"][1:-1].split(',')
            z = float(relPosition[2].strip())
            application[taskIndex[row["task"]]][positionGroup[int(row["label(81~99)"])]][envIndex[row["environment"]]].append(z);

allEnvPlot = plt.figure(1)
allEnvGroups = []
allEnvXTicks = ["Facebook,\nlow-traffic", "Medium,\nlow-traffic", "YouTube,\nlow-traffic", "Facebook,\nhigh-traffic", "Medium,\nhigh-traffic",  "YouTube,\nhigh-traffic"];
for i in range(2):
    for j in range(3):
        envGroups = []
        for k in range(5):
            envGroups += application[j][k][i]
        allEnvGroups.append(envGroups)
        allEnvXTicks[(j * 2) + i] += " (" + str(len(envGroups)) + ")"
bp = plt.boxplot(allEnvGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue'), positions=[0, 1, 2, 4, 5, 6], labels = allEnvXTicks) 
plt.title("Boxplot of depth (By application and traffic)")
plt.ylabel("Depth")
plt.ylim(bottom = 0);
plt.legend([bp['medians'][0], bp['means'][0]], ['median', 'mean'])
allEnvPlot.show()

plt.show()