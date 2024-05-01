import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import json

all = [[] for x in range(2)]
lowTraffic = [[] for x in range(2)]
highTraffic = [[] for x in range(2)]

f = open("./AllWindowData.json")
data = json.load(f)
for i in data["windowDatas"]:
    all[i["type"]].append(i["transparency"]);

    # Environment
    if i["caseNum"] == 1 or i["caseNum"] == 2:
        lowTraffic[i["type"]].append(i["transparency"]);
    else:
        highTraffic[i["type"]].append(i["transparency"]);
  
# Overall
df = pd.DataFrame({'presentation': all[0], 'participant': all[1]})
plt.title("Probability density of opacity (Overall)")

# Environment
# df = pd.DataFrame({'highTraffic': highTraffic[0], 'lowTraffic': lowTraffic[0]})
# plt.title("Probability density of opacity (Presentation, Environment)")

sns.kdeplot(df, bw_adjust=0.5)

plt.xlabel('Opacity')
plt.ylabel('Probability Density')

# plt.show()
plt.savefig('Results/opacity_kde.png')