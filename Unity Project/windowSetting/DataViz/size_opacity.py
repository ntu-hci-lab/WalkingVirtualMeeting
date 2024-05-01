# Calculations based on https://courses.washington.edu/psy333/lecture_pdfs/chapter8_Depthvisual angle.pdf

import matplotlib.pyplot as plt
import numpy as np
import json

eyeYPos = 1.2

# arr[case][type][userId]
sizeP = [[[] for y in range(2)] for z in range(4)]
transparencyP = [[[] for y in range(2)] for z in range(4)]
sizeH = [[[] for y in range(2)] for z in range(4)]
transparencyH = [[[] for y in range(2)] for z in range(4)]

f = open("./AllWindowData.json")
data = json.load(f)
for i in data['windowDatas']:
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
       
       if i["windowMode"] == 0:
              sizeP[i["caseNum"] - 1][i["type"]].append(angle)
              transparencyP[i["caseNum"] - 1][i["type"]].append(i["transparency"])
       else:
              sizeH[i["caseNum"] - 1][i["type"]].append(angle)
              transparencyH[i["caseNum"] - 1][i["type"]].append(i["transparency"])

f.close()

# Cases Path
path = plt.figure(1)
plt.plot(transparencyP[0][0], sizeP[0][0], 'red', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyP[0][1], sizeP[0][1], 'salmon', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyP[1][0], sizeP[1][0], 'orange', marker='.', linestyle='None', markersize = 10.0)
plt.plot(transparencyP[1][1], sizeP[1][1], 'gold', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyP[2][0], sizeP[2][0], 'seagreen', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyP[2][1], sizeP[2][1], 'turquoise', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyP[3][0], sizeP[3][0], 'blue', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyP[3][1], sizeP[3][1], 'dodgerblue', marker='.', linestyle='None', markersize = 10.0) 
plt.legend(["Case 1 Presentation", "Case 1 Participant", "Case 2 Presentation", "Case 2 Participant", "Case 3 Presentation", "Case 3 Participant", "Case 4 Presentation", "Case 4 Participant"], loc ="upper left")
plt.title("Graph of visual angle against opacity (Path Anchor, Both)")
plt.xlabel('Opacity')
plt.ylabel('Visual Angle')
path.show()

# Cases Head
head = plt.figure(2)
plt.plot(transparencyH[0][0], sizeH[0][0], 'red', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyH[0][1], sizeH[0][1], 'salmon', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyH[1][0], sizeH[1][0], 'orange', marker='.', linestyle='None', markersize = 10.0)
plt.plot(transparencyH[1][1], sizeH[1][1], 'gold', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyH[2][0], sizeH[2][0], 'seagreen', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyH[2][1], sizeH[2][1], 'turquoise', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyH[3][0], sizeH[3][0], 'blue', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyH[3][1], sizeH[3][1], 'dodgerblue', marker='.', linestyle='None', markersize = 10.0) 
plt.legend(["Case 1 Presentation", "Case 1 Participant", "Case 2 Presentation", "Case 2 Participant", "Case 3 Presentation", "Case 3 Participant", "Case 4 Presentation", "Case 4 Participant"], loc ="upper left")
plt.title("Graph of visual angle against opacity (Head Anchor, Both)")
plt.xlabel('Opacity')
plt.ylabel('Visual Angle')
head.show()

# Cases Path Presentation
pathPr = plt.figure(3)
plt.plot(transparencyP[0][0], sizeP[0][0], 'red', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyP[1][0], sizeP[1][0], 'orange', marker='.', linestyle='None', markersize = 10.0)
plt.plot(transparencyP[2][0], sizeP[2][0], 'seagreen', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyP[3][0], sizeP[3][0], 'blue', marker='.', linestyle='None', markersize = 10.0) 
plt.legend(["Case 1 Presentation", "Case 2 Presentation", "Case 3 Presentation", "Case 4 Presentation"], loc ="upper left")
plt.title("Graph of visual angle against opacity (Path Anchor, Presentation)")
plt.xlabel('Opacity')
plt.ylabel('Visual Angle')
pathPr.show()

# Cases Path Participant
pathPa = plt.figure(4)
plt.plot(transparencyP[0][1], sizeP[0][1], 'red', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyP[1][1], sizeP[1][1], 'orange', marker='.', linestyle='None', markersize = 10.0)
plt.plot(transparencyP[2][1], sizeP[2][1], 'seagreen', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyP[3][1], sizeP[3][1], 'blue', marker='.', linestyle='None', markersize = 10.0) 
plt.legend(["Case 1 Participant", "Case 2 Participant", "Case 3 Participant", "Case 4 Participant"], loc ="upper left")
plt.title("Graph of visual angle against opacity (Path Anchor, Participant)")
plt.xlabel('Opacity')
plt.ylabel('Visual Angle')
pathPa.show()

# Cases Head Presentation
headPr = plt.figure(5)
plt.plot(transparencyH[0][0], sizeH[0][0], 'red', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyH[1][0], sizeH[1][0], 'orange', marker='.', linestyle='None', markersize = 10.0)
plt.plot(transparencyH[2][0], sizeH[2][0], 'seagreen', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyH[3][0], sizeH[3][0], 'blue', marker='.', linestyle='None', markersize = 10.0) 
plt.legend(["Case 1 Presentation", "Case 2 Presentation", "Case 3 Presentation", "Case 4 Presentation"], loc ="upper left")
plt.title("Graph of visual angle against opacity (Head Anchor, Presentation)")
plt.xlabel('Opacity')
plt.ylabel('Visual Angle')
headPr.show()

# Cases Head Participant
headPa = plt.figure(6)
plt.plot(transparencyH[0][1], sizeH[0][1], 'red', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyH[1][1], sizeH[1][1], 'orange', marker='.', linestyle='None', markersize = 10.0)
plt.plot(transparencyH[2][1], sizeH[2][1], 'seagreen', marker='.', linestyle='None', markersize = 10.0) 
plt.plot(transparencyH[3][1], sizeH[3][1], 'blue', marker='.', linestyle='None', markersize = 10.0) 
plt.legend(["Case 1 Participant", "Case 2 Participant", "Case 3 Participant", "Case 4 Participant"], loc ="upper left")
plt.title("Graph of visual angle against opacity (Head Anchor, Participant)")
plt.xlabel('Opacity')
plt.ylabel('Visual Angle')
headPa.show()

# Get user arrays
tpUsers = [[] for x in range(9)]
spUsers = [[] for x in range(9)]
thUsers = [[] for x in range(9)]
shUsers = [[] for x in range(9)]

for i in range(4):
       for j in range(len(sizeP[i])):
              for k in range(len(sizeP[i][j])):
                     spUsers[k].append(sizeP[i][j][k])

for i in range(4):
       for j in range(len(transparencyP[i])):
              for k in range(len(transparencyP[i][j])):
                     tpUsers[k].append(transparencyP[i][j][k])

for i in range(4):
       for j in range(len(sizeH[i])):
              for k in range(min(len(sizeH[i][j]), 9)):
                     shUsers[k].append(sizeH[i][j][k])

for i in range(4):
       for j in range(len(transparencyH[i])):
              for k in range(min(len(transparencyH[i][j]), 9)):
                     thUsers[k].append(transparencyH[i][j][k])

# for i in range(9):
#        tpUsers[i] = [transparencyP[0][0][i], transparencyP[0][1][i], transparencyP[1][0][i], transparencyP[1][1][i], transparencyP[2][0][i], transparencyP[2][1][i], transparencyP[3][0][i], transparencyP[3][1][i]]
#        spUsers[i] = [sizeP[0][0][i], sizeP[0][1][i], sizeP[1][0][i], sizeP[1][1][i], sizeP[2][0][i], sizeP[2][1][i], sizeP[3][0][i], sizeP[3][1][i]]
#        thUsers[i] = [transparencyH[0][0][i], transparencyH[0][1][i], transparencyH[1][0][i], transparencyH[1][1][i], transparencyH[2][0][i], transparencyH[2][1][i], transparencyH[3][0][i], transparencyH[3][1][i]]
#        shUsers[i] = [sizeH[0][0][i], sizeH[0][1][i], sizeH[1][0][i], sizeH[1][1][i], sizeH[2][0][i], sizeH[2][1][i], sizeH[3][0][i], sizeH[3][1][i]]

# Users Path
userPath = plt.figure(7)
plt.plot(tpUsers[0], spUsers[0], 'tomato', marker='.', linestyle='None', markersize = 10.0)
plt.plot(tpUsers[1], spUsers[1], 'orange', marker='.', linestyle='None', markersize = 10.0)
plt.plot(tpUsers[2], spUsers[2], 'mediumseagreen', marker='.', linestyle='None', markersize = 10.0)
plt.plot(tpUsers[3], spUsers[3], 'steelblue', marker='.', linestyle='None', markersize = 10.0)
plt.plot(tpUsers[4], spUsers[4], 'darkorchid', marker='.', linestyle='None', markersize = 10.0)
plt.plot(tpUsers[5], spUsers[5], 'hotpink', marker='.', linestyle='None', markersize = 10.0)
plt.plot(tpUsers[6], spUsers[6], 'sienna', marker='.', linestyle='None', markersize = 10.0)
plt.plot(tpUsers[7], spUsers[7], 'olivedrab', marker='.', linestyle='None', markersize = 10.0)
plt.plot(tpUsers[8], spUsers[8], 'navy', marker='.', linestyle='None', markersize = 10.0)
plt.legend(["User 1", "User 2", "User 3", "User 4", "User 5", "User 6", "User 7", "User 8", "User 9"], loc ="upper left")
plt.title("Graph of visual angle against opacity (Path Anchor, Users)")
plt.xlabel('Opacity')
plt.ylabel('Visual Angle')
userPath.show()

# Users Head
userHead = plt.figure(8)
plt.plot(thUsers[0], shUsers[0], 'tomato', marker='.', linestyle='None', markersize = 10.0)
plt.plot(thUsers[1], shUsers[1], 'orange', marker='.', linestyle='None', markersize = 10.0)
plt.plot(thUsers[2], shUsers[2], 'mediumseagreen', marker='.', linestyle='None', markersize = 10.0)
plt.plot(thUsers[3], shUsers[3], 'steelblue', marker='.', linestyle='None', markersize = 10.0)
plt.plot(thUsers[4], shUsers[4], 'darkorchid', marker='.', linestyle='None', markersize = 10.0)
plt.plot(thUsers[5], shUsers[5], 'hotpink', marker='.', linestyle='None', markersize = 10.0)
plt.plot(thUsers[6], shUsers[6], 'sienna', marker='.', linestyle='None', markersize = 10.0)
plt.plot(thUsers[7], shUsers[7], 'olivedrab', marker='.', linestyle='None', markersize = 10.0)
plt.plot(thUsers[8], shUsers[8], 'navy', marker='.', linestyle='None', markersize = 10.0)
plt.legend(["User 1", "User 2", "User 3", "User 4", "User 5", "User 6", "User 7", "User 8", "User 9"], loc ="upper left")
plt.title("Graph of visual angle against opacity (Head Anchor, Users)")
plt.xlabel('Opacity')
plt.ylabel('Visual Angle')
userHead.show()

# plt.show()
plt.savefig('Results/size_opacity.png')