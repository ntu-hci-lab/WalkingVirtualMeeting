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
    9 : 4,
}

envIndex = {
    "Campus" : 0,
    "SideWalk" : 1
}

with open('Analysis/walkAR/user_fav_label.csv', mode ='r')as file:
    reader = csv.DictReader(open('Analysis/walkAR/user_fav_label.csv'))
    for row in reader:
        if row["UserID"] != "":
            relPosition = row["relPosition"][1:-1].split(',')
            x = float(relPosition[0].strip())
            y = float(relPosition[1].strip())
            z = float(relPosition[2].strip())
            distance = np.linalg.norm(np.array([x, y, z]))
            application[taskIndex[row["task"]]][positionGroup[int(row["label(81~99)"])]][envIndex[row["environment"]]].append(distance);

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
plt.title("Boxplot of distance (By application and traffic)")
plt.ylabel("Distance")
plt.ylim(bottom = 0);
plt.legend([bp['medians'][0], bp['means'][0]], ['median', 'mean'])
allEnvPlot.show()

plt.show()

# ARCHIVE
# import matplotlib.pyplot as plt
# import numpy as np
# import csv

# application = [[] for x in range(3)] # 0: Facebook, 1: Medium, 2: YouTube
# environment = [[] for x in range(2)] # 0: Comfort/Usability, 1: Safety

# with open('user_fav_label.csv', mode ='r')as file:
#     reader = csv.DictReader(open('user_fav_label.csv'))
#     for row in reader:
#         if row["UserID"] != "":
#             relPosition = row["relPosition"][1:-1].split(',')
#             x = float(relPosition[0].strip())
#             y = float(relPosition[1].strip())
#             z = float(relPosition[2].strip())
#             distance = np.linalg.norm(np.array([x, y, z]))

#             if row["task"] == "facebook":
#                 application[0].append(distance)
#             elif row["task"] == "medium":
#                 application[1].append(distance)
#             else:
#                 application[2].append(distance)

#             if row["purpose"] == "comfort" or row["purpose"] == "usability":
#                 environment[0].append(distance)
#             else:
#                 environment[1].append(distance)

# # Application
# applicationPlot = plt.figure(1)
# plt.boxplot([application[0], application[1], application[2]], medianprops=dict(color='orangered')) 
# plt.title("Boxplot of distance (Application)")
# plt.xticks([1, 2, 3], ["Facebook", "Medium", "YouTube"])
# plt.ylabel("Distance")
# plt.ylim(bottom=0)
# applicationPlot.show()

# # Environment
# environmentPlot = plt.figure(2)
# plt.boxplot([environment[0], environment[1]], medianprops=dict(color='orangered')) 
# plt.title("Boxplot of distance (Priority)")
# plt.xticks([1, 2], ["Comfort / Usability", "Safety"])
# plt.ylabel("Distance")
# plt.ylim(bottom=0)
# environmentPlot.show()

# plt.show()