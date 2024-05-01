import matplotlib.pyplot as plt
import numpy as np
import json

eyeYPos = 1.2

all = [[] for x in range(2)]
lowTraffic = [[] for x in range(2)]
highTraffic = [[] for x in range(2)]
listening = [[] for x in range(2)]
discussion = [[] for x in range(2)]
pathAnchor = [[] for x in range(2)]
headAnchor = [[] for x in range(2)]

f = open("./AllWindowData.json")
data = json.load(f)
for i in data["windowDatas"]:
    # find corners
    corners = i["corner"]
    topArr = np.array([corners[0]["position"], corners[1]["position"]])
    middleTop = topArr.sum(axis=0) / 2
    middleTop[1] -= eyeYPos
    botArr = np.array([corners[2]["position"], corners[3]["position"]])
    middleBot = botArr.sum(axis=0) / 2
    middleBot[1] -= eyeYPos

    # find angle between two vectors
    topUnit = middleTop / np.linalg.norm(middleTop)
    botUnit = middleBot / np.linalg.norm(middleBot)
    angle = np.arccos(np.dot(topUnit, botUnit));

    all[i["type"]].append(angle);

    # Environment
    if i["caseNum"] == 1 or i["caseNum"] == 2:
        lowTraffic[i["type"]].append(angle);
    else:
        highTraffic[i["type"]].append(angle);

    # Meeting Type
    if i["caseNum"] == 1 or i["caseNum"] == 3:
        listening[i["type"]].append(angle);
    else:
        discussion[i["type"]].append(angle);

    # Window Mode
    if i["windowMode"] == 0:
        pathAnchor[i["type"]].append(angle);
    else:
        headAnchor[i["type"]].append(angle);

f.close()

# Overall
overall = plt.figure(1)
plt.boxplot([all[0], all[1]], medianprops=dict(color='orangered')) 
plt.title("Boxplot of visual angle (Overall)")
plt.xticks([1, 2], ["Presentation", "Participant"])
plt.ylabel("Visual Angle")
overall.show()

# Environment
environment = plt.figure(2)
plt.boxplot([lowTraffic[0], highTraffic[0], lowTraffic[1], highTraffic[1], lowTraffic[0] + lowTraffic[1], highTraffic[0] + highTraffic[1]], medianprops=dict(color='orangered')) 
plt.title("Boxplot of visual angle (Environment)")
plt.xticks([1, 2, 3, 4, 5, 6], ["Presentation,\nLow Traffic", "Presentation,\nHigh Traffic", "Participant,\nLow Traffic", "Participant,\nHigh Traffic", "Both,\nLow Traffic", "Both,\nHigh Traffic"])
plt.ylabel("Visual Angle")
environment.show()

# Meeting Type
meetingType = plt.figure(3)
plt.boxplot([listening[0], discussion[0], listening[1], discussion[1], listening[0] + listening[1], discussion[0] + discussion[1]], medianprops=dict(color='orangered')) 
plt.title("Boxplot of visual angle (Meeting type)")
plt.xticks([1, 2, 3, 4, 5, 6], ["Presentation,\nListening", "Presentation,\nDiscussion", "Participant,\nListening", "Participant,\nDiscussion", "Both,\nListening", "Both,\nDiscussion"])
plt.ylabel("Visual Angle")
meetingType.show()

# Window Mode
windowMode = plt.figure(4)
plt.boxplot([pathAnchor[0], headAnchor[0], pathAnchor[1], headAnchor[1], pathAnchor[0] + pathAnchor[1], headAnchor[0] + headAnchor[1]], medianprops=dict(color='orangered')) 
plt.title("Boxplot of visual angle (Window mode)")
plt.xticks([1, 2, 3, 4, 5, 6], ["Presentation,\nPath Anchor", "Presentation,\nHead Anchor", "Participant,\nPath Anchor", "Participant,\nHead Anchor", "Both,\nPath Anchor", "Both,\nHead Anchor"])
plt.ylabel("Visual Angle")
windowMode.show()

# plt.show()
plt.savefig('Results/size_boxplots.png')