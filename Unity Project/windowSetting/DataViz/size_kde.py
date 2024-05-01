import pandas as pd
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
import json

eyeYPos = 1.2

all = [[] for x in range(2)]

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
  
# Overall
df = pd.DataFrame({'presentation': all[0], 'participant': all[1]})
plt.title("Probability density of visual angle (Overall)")

sns.kdeplot(df, bw_adjust=0.5)

plt.xlabel('Visual Angle')
plt.ylabel('Probability Density')

# plt.show()
plt.savefig('Results/size_kde.png')