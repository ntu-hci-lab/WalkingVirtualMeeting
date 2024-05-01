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
            application[taskIndex[row["task"]]][positionGroup[int(row["label(81~99)"])]][envIndex[row["environment"]]].append(float(row["Transparency"]));

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
plt.title("Boxplot of opacity (By application and traffic)")
plt.ylabel("Opacity")
plt.ylim(bottom = 0);
plt.legend([bp['medians'][0], bp['means'][0]], ['median', 'mean'])
allEnvPlot.show()

plt.show()

# ARCHIVE
# import matplotlib.pyplot as plt
# import numpy as np
# import csv

# # application[task][position][purpose]
# application = [[[[] for x in range(2)] for y in range(5)] for z in range(3)]

# taskIndex = {
#     "facebook" : 0,
#     "medium" : 1,
#     "youtube" : 2
# }

# # 0: top, 1: middle center, 2: down, 3: side center, 4: side down
# positionGroup = {
#     1 : 3,
#     2 : 0,
#     3 : 3,
#     4 : 3,
#     5 : 1,
#     6 : 3,
#     7 : 4,
#     8 : 2,
#     9 : 4
# }

# purposeIndex = {
#     "safety" : 0,
#     "comfort" : 1,
#     "usability" : 1
# }

# with open('user_fav_label.csv', mode ='r')as file:
#     reader = csv.DictReader(open('user_fav_label.csv'))
#     for row in reader:
#         if row["UserID"] != "":
#             print(row["task"])
#             print(row["label(81~99)"])
#             print(row["purpose"])
#             print("---")
#             application[taskIndex[row["task"]]][positionGroup[int(row["label(81~99)"])]][purposeIndex[row["purpose"]]].append(float(row["Transparency"]));


# # Facebook
# fbPlot = plt.figure(1)
# fbGroups = []
# for i in range(4):
#     for j in range(2):
#         fbGroups.append(application[0][i + 1][j])
# plt.boxplot(fbGroups, medianprops=dict(color='orangered')) 
# plt.title("Boxplot of opacity (Facebook)")
# plt.xticks([1, 2, 3, 4, 5, 6, 7, 8], ["Middle Center,\nSafety", "Middle Center,\nNon-Safety", "Bottom,\nSafety", "Bottom,\nNon-Safety", "Side Center,\nSafety", "Side Center,\nNon-Safety", "Side Bottom,\nSafety", "Side Bottom,\nNon-Safety"])
# plt.ylabel("Opacity")
# plt.ylim(0, 1.1)
# fbPlot.show()

# # Medium
# mdPlot = plt.figure(2)
# mdGroups = []
# for i in range(4):
#     for j in range(2):
#         mdGroups.append(application[1][i + 1][j])
# plt.boxplot(mdGroups, medianprops=dict(color='orangered')) 
# plt.title("Boxplot of opacity (Medium)")
# plt.xticks([1, 2, 3, 4, 5, 6, 7, 8], ["Middle Center,\nSafety", "Middle Center,\nNon-Safety", "Bottom,\nSafety", "Bottom,\nNon-Safety", "Side Center,\nSafety", "Side Center,\nNon-Safety", "Side Bottom,\nSafety", "Side Bottom,\nNon-Safety"])
# plt.ylabel("Opacity")
# plt.ylim(0, 1.1)
# mdPlot.show()

# # YouTube
# ytPlot = plt.figure(3)
# ytGroups = []
# for i in range(5):
#     for j in range(2):
#         ytGroups.append(application[2][i][j])
# plt.boxplot(ytGroups, medianprops=dict(color='orangered')) 
# plt.title("Boxplot of opacity (YouTube)")
# plt.xticks([1, 2, 3, 4, 5, 6, 7, 8, 9, 10], ["Top,\nSafety", "Top,\nNon-Safety", "Middle\nCenter,\nSafety", "Middle\nCenter,\nNon-Safety", "Bottom,\nSafety", "Bottom,\nNon-Safety", "Side Center,\nSafety", "Side Center,\nNon-Safety", "Side Bottom,\nSafety", "Side Bottom,\nNon-Safety"])
# plt.ylabel("Opacity")
# plt.ylim(0, 1.1)
# ytPlot.show()

# print(application[2][0][0])

# plt.show()