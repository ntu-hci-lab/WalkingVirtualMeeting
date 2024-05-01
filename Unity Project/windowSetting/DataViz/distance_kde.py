import pandas as pd
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
import json

all = [[] for x in range(12)]
headAnchor = [[] for x in range(2)]
pathAnchor = [[] for x in range(2)]

f = open("./AllWindowData.json")
data = json.load(f)
for i in data["windowDatas"]:
    all[i["type"]].append(np.linalg.norm(np.array(i["position"])));

    # Window Mode
    if i["windowMode"] == 1:
        headAnchor[i["type"]].append(np.linalg.norm(np.array(i["position"])));
    else:
        pathAnchor[i["type"]].append(np.linalg.norm(np.array(i["position"])));
  
# Overall
# df = pd.DataFrame({'presentation': all[0], 'participant': all[1]})
# plt.title("Probability density of distance (Overall)")

# Window Mode
df = pd.DataFrame({'headAnchor': headAnchor[0], 'pathAnchor': pathAnchor[0]})
plt.title("Probability density of distance (Presentation, Window Mode)")

sns.kdeplot(df, bw_adjust=0.5)

plt.xlabel('Distance')
plt.ylabel('Probability Density')

# plt.show()
plt.savefig('Results/distance_kde.png')