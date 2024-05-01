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
            application[taskIndex[row["task"]]][positionGroup[int(row["label(81~99)"])]][envIndex[row["environment"]]].append((float)(row["width_angle"]) * (float)(row["height_angle"]));

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
plt.title("Boxplot of size (By application and traffic)")
plt.ylabel("Visual angles multiplied")
plt.ylim(bottom = 0);
plt.legend([bp['medians'][0], bp['means'][0]], ['median', 'mean'])
allEnvPlot.show()

plt.show()

# ARCHIVE
# import matplotlib.pyplot as plt
# from scipy.spatial.transform import Rotation as R
# import numpy as np
# import csv

# # application[task][position][env]
# application = [[[[] for x in range(2)] for y in range(5)] for z in range(3)]

# taskIndex = {
#     "facebook" : 0,
#     "medium" : 1,
#     "youtube" : 2
# }

# # 0: top, 1: middle, 2: bottom, 3: side, 4: bottom-side
# positionGroup = {
#     1 : 0,
#     2 : 0,
#     3 : 0,
#     4 : 3,
#     5 : 1,
#     6 : 3,
#     7 : 4,
#     8 : 2,
#     9 : 4
# }

# envIndex = {
#     "Campus" : 0,
#     "SideWalk" : 1
# }

# def rotateVector(original, eulerAngles):
#     r = R.from_euler('xyz', [eulerAngles], degrees=True);
#     return r.apply(original)[0]

# with open('user_fav_label.csv', mode ='r')as file:
#     reader = csv.DictReader(open('user_fav_label.csv'))
#     for row in reader:
#         if row["UserID"] != "":
#             application[taskIndex[row["task"]]][positionGroup[int(row["label(81~99)"])]][envIndex[row["environment"]]].append((float)(row["width_angle"]) * (float)(row["height_angle"]));


# # # Facebook
# # fbPlot = plt.figure(1)
# # fbGroups = []
# # fbXTicks = ["Middle,\nlow-traffic", "Middle,\nhigh-traffic", "Bottom,\nlow-traffic", "Bottom,\nhigh-traffic", "Side,\nlow-traffic", "Side,\nhigh-traffic", "Bottom Side,\nlow-traffic", "Bottom Side,\nhigh-traffic"];
# # for i in range(4):
# #     for j in range(2):
# #         fbGroups.append(application[0][i + 1][j])
# #         fbXTicks[(i * 2) + j] += " (" + str(len(application[0][i + 1][j])) + ")"
# # plt.boxplot(fbGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue')) 
# # plt.title("Boxplot of size (Facebook)")
# # plt.xticks([1, 2, 3, 4, 5, 6, 7, 8], fbXTicks)
# # plt.ylabel("Visual angles multiplied")
# # plt.ylim(0, 1.15)
# # fbPlot.show()

# # # Medium
# # mdPlot = plt.figure(2)
# # mdGroups = []
# # mdXTicks = ["Middle,\nlow-traffic", "Middle,\nhigh-traffic", "Bottom,\nlow-traffic", "Bottom,\nhigh-traffic", "Side,\nlow-traffic", "Side,\nhigh-traffic", "Bottom Side,\nlow-traffic", "Bottom Side,\nhigh-traffic"];
# # for i in range(4):
# #     for j in range(2):
# #         mdGroups.append(application[1][i + 1][j])
# #         mdXTicks[(i * 2) + j] += " (" + str(len(application[1][i + 1][j])) + ")"
# # plt.boxplot(mdGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue')) 
# # plt.title("Boxplot of size (Medium)")
# # plt.xticks([1, 2, 3, 4, 5, 6, 7, 8], mdXTicks)
# # plt.ylabel("Visual angles multiplied")
# # plt.ylim(0, 1.15)
# # mdPlot.show()

# # # YouTube
# # ytPlot = plt.figure(3)
# # ytGroups = []
# # ytXTicks = ["Top,\nlow-traffic", "Top,\nhigh-traffic", "Middle,\nlow-traffic", "Middle,\nhigh-traffic", "Bottom,\nlow-traffic", "Bottom,\nhigh-traffic", "Side,\nlow-traffic", "Side,\nhigh-traffic", "Bottom Side,\nlow-traffic", "Bottom Side,\nhigh-traffic"];
# # for i in range(5):
# #     for j in range(2):
# #         ytGroups.append(application[2][i][j])
# #         ytXTicks[(i * 2) + j] += " (" + str(len(application[2][i][j])) + ")"
# # plt.boxplot(ytGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue')) 
# # plt.title("Boxplot of size (YouTube)")
# # plt.xticks([1, 2, 3, 4, 5, 6, 7, 8, 9, 10], ytXTicks)
# # plt.ylabel("Visual angles multiplied")
# # plt.ylim(0, 1.15)
# # ytPlot.show()

# appPlot = plt.figure(1)
# appGroups = []
# appXTicks = ["Top,\nlow-traffic", "Top,\nhigh-traffic", "Middle,\nlow-traffic", "Middle,\nhigh-traffic", "Bottom,\nlow-traffic", "Bottom,\nhigh-traffic", "Side,\nlow-traffic", "Side,\nhigh-traffic", "Bottom Side,\nlow-traffic", "Bottom Side,\nhigh-traffic"];
# for i in range(5):
#     for j in range(2):
#         appGroups.append(application[2][i][j] + application[1][i][j] + application[0][i][j])
#         appXTicks[(i * 2) + j] += "\n(" + str(len(application[2][i][j] + application[1][i][j] + application[0][i][j])) + ")"
# bp = plt.boxplot(appGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue')) 
# plt.title("Boxplot of size (By position and traffic)")
# plt.xticks([1, 2, 3, 4, 5, 6, 7, 8, 9, 10], appXTicks)
# plt.ylabel("Visual angles multiplied (rad^2)")
# plt.ylim(0, 1.15)
# plt.legend([bp['medians'][0], bp['means'][0]], ['median', 'mean'])
# appPlot.show()

# # fbCombinedPlot = plt.figure(5)
# # fbCombinedGroups = []
# # fbCombinedXTicks = ["Middle", "Bottom", "Side", "Bottom Side"];
# # for i in range(4):
# #     fbCombinedGroups.append(application[0][i + 1][0] + application[0][i + 1][1])
# #     fbCombinedXTicks[i] += " (" + str(len(application[0][i + 1][0] + application[0][i + 1][1])) + ")"
# # plt.boxplot(fbCombinedGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue')) 
# # plt.title("Boxplot of size (Facebook)")
# # plt.xticks([1, 2, 3, 4], fbCombinedXTicks)
# # plt.ylabel("Visual angles multiplied")
# # plt.ylim(0, 1.15)
# # fbCombinedPlot.show()

# # mdCombinedPlot = plt.figure(6)
# # mdCombinedGroups = []
# # mdCombinedXTicks = ["Middle", "Bottom", "Side", "Bottom Side"];
# # for i in range(4):
# #     mdCombinedGroups.append(application[1][i + 1][0] + application[1][i + 1][1])
# #     mdCombinedXTicks[i] += " (" + str(len(application[1][i + 1][0] + application[1][i + 1][1])) + ")"
# # plt.boxplot(mdCombinedGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue')) 
# # plt.title("Boxplot of size (Medium)")
# # plt.xticks([1, 2, 3, 4], mdCombinedXTicks)
# # plt.ylabel("Visual angles multiplied")
# # plt.ylim(0, 1.15)
# # mdCombinedPlot.show()

# # ytCombinedPlot = plt.figure(7)
# # ytCombinedGroups = []
# # ytCombinedXTicks = ["Top", "Middle", "Bottom", "Side", "Bottom Side"];
# # for i in range(5):
# #     ytCombinedGroups.append(application[2][i][0] + application[2][i][1])
# #     ytCombinedXTicks[i] += " (" + str(len(application[2][i][0] + application[2][i][1])) + ")"
# # plt.boxplot(ytCombinedGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue')) 
# # plt.title("Boxplot of size (YouTube)")
# # plt.xticks([1, 2, 3, 4, 5], ytCombinedXTicks)
# # plt.ylabel("Visual angles multiplied")
# # plt.ylim(0, 1.15)
# # ytCombinedPlot.show()

# # fbEnvPlot = plt.figure(8)
# # fbEnvGroups = []
# # fbEnvXTicks = ["Campus", "Sidewalk"];
# # for i in range(2):
# #     envGroups = []
# #     for j in range(5):
# #         envGroups += application[0][j][i]
# #     fbEnvGroups.append(envGroups)
# #     fbEnvXTicks[i] += " (" + str(len(envGroups)) + ")"
# # plt.boxplot(fbEnvGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue')) 
# # plt.title("Boxplot of size (Facebook)")
# # plt.xticks([1, 2], fbEnvXTicks)
# # plt.ylabel("Visual angles multiplied")
# # plt.ylim(0, 1.15)
# # fbEnvPlot.show()

# # mdEnvPlot = plt.figure(9)
# # mdEnvGroups = []
# # mdEnvXTicks = ["Campus", "Sidewalk"];
# # for i in range(2):
# #     envGroups = []
# #     for j in range(5):
# #         envGroups += application[1][j][i]
# #     mdEnvGroups.append(envGroups)
# #     mdEnvXTicks[i] += " (" + str(len(envGroups)) + ")"
# # plt.boxplot(mdEnvGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue')) 
# # plt.title("Boxplot of size (Medium)")
# # plt.xticks([1, 2], mdEnvXTicks)
# # plt.ylabel("Visual angles multiplied")
# # plt.ylim(0, 1.15)
# # mdEnvPlot.show()

# # ytEnvPlot = plt.figure(10)
# # ytEnvGroups = []
# # ytEnvXTicks = ["Campus", "Sidewalk"];
# # for i in range(2):
# #     envGroups = []
# #     for j in range(5):
# #         envGroups += application[2][j][i]
# #     ytEnvGroups.append(envGroups)
# #     ytEnvXTicks[i] += " (" + str(len(envGroups)) + ")"
# # plt.boxplot(ytEnvGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue')) 
# # plt.title("Boxplot of size (YouTube)")
# # plt.xticks([1, 2], ytEnvXTicks)
# # plt.ylabel("Visual angles multiplied")
# # plt.ylim(0, 1.15)
# # ytEnvPlot.show()

# # allEnvPlot = plt.figure(1)
# # allEnvGroups = []
# # allEnvXTicks = ["Facebook,\nlow-traffic", "Facebook,\nhigh-traffic", "Medium,\nlow-traffic", "Medium,\nhigh-traffic", "YouTube,\nlow-traffic", "YouTube,\nhigh-traffic"];
# # for i in range(3):
# #     for j in range(2):
# #         envGroups = []
# #         for k in range(5):
# #             envGroups += application[i][k][j]
# #         allEnvGroups.append(envGroups)
# #         allEnvXTicks[(i * 2) + j] += " (" + str(len(envGroups)) + ")"
# # bp = plt.boxplot(allEnvGroups, medianprops=dict(color='orangered'), showmeans=True, meanline=True, meanprops=dict(color='royalblue'), positions=[0, 1, 3, 4, 6, 7], labels = allEnvXTicks) 
# # plt.title("Boxplot of size (By application and traffic)")
# # plt.ylabel("Visual angles multiplied (rad^2)")
# # plt.ylim(0, 1.15)
# # plt.legend([bp['medians'][0], bp['means'][0]], ['median', 'mean'])
# # allEnvPlot.show()

# plt.show()