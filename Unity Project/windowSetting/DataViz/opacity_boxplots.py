import matplotlib.pyplot as plt
import numpy as np
import json

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
    all[i["type"]].append(i["transparency"]);

    # Environment
    if i["caseNum"] == 1 or i["caseNum"] == 2:
        lowTraffic[i["type"]].append(i["transparency"]);
    else:
        highTraffic[i["type"]].append(i["transparency"]);

    # Meeting Type
    if i["caseNum"] == 1 or i["caseNum"] == 3:
        listening[i["type"]].append(i["transparency"]);
    else:
        discussion[i["type"]].append(i["transparency"]);

    # Window Mode
    if i["windowMode"] == 0:
        pathAnchor[i["type"]].append(i["transparency"]);
    else:
        headAnchor[i["type"]].append(i["transparency"]);

f.close()

# Overall
overall = plt.figure(1)
plt.boxplot([all[0], all[1]], medianprops=dict(color='orangered')) 
plt.title("Boxplot of opacity (Overall)")
plt.xticks([1, 2], ["Presentation", "Participant"])
plt.ylabel("Opacity")
overall.show()

# Environment
environment = plt.figure(2)
plt.boxplot([lowTraffic[0], highTraffic[0], lowTraffic[1], highTraffic[1], lowTraffic[0] + lowTraffic[1], highTraffic[0] + highTraffic[1]], medianprops=dict(color='orangered')) 
plt.title("Boxplot of opacity (Environment)")
plt.xticks([1, 2, 3, 4, 5, 6], ["Presentation,\nLow Traffic", "Presentation,\nHigh Traffic", "Participant,\nLow Traffic", "Participant,\nHigh Traffic", "Both,\nLow Traffic", "Both,\nHigh Traffic"])
plt.ylabel("Opacity")
environment.show()

# Meeting Type
meetingType = plt.figure(3)
plt.boxplot([listening[0], discussion[0], listening[1], discussion[1], listening[0] + listening[1], discussion[0] + discussion[1]], medianprops=dict(color='orangered')) 
plt.title("Boxplot of opacity (Meeting type)")
plt.xticks([1, 2, 3, 4, 5, 6], ["Presentation,\nListening", "Presentation,\nDiscussion", "Participant,\nListening", "Participant,\nDiscussion", "Both,\nListening", "Both,\nDiscussion"])
plt.ylabel("Opacity")
meetingType.show()

# Window Mode
windowMode = plt.figure(4)
plt.boxplot([pathAnchor[0], headAnchor[0], pathAnchor[1], headAnchor[1], pathAnchor[0] + pathAnchor[1], headAnchor[0] + headAnchor[1]], medianprops=dict(color='orangered')) 
plt.title("Boxplot of opacity (Window mode)")
plt.xticks([1, 2, 3, 4, 5, 6], ["Presentation,\nPath Anchor", "Presentation,\nHead Anchor", "Participant,\nPath Anchor", "Participant,\nHead Anchor", "Both,\nPath Anchor", "Both,\nHead Anchor"])
plt.ylabel("Opacity")
windowMode.show()

# plt.show()
plt.savefig('Results/opacity_boxplots.png')