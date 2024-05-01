traffic = "high"  # low or high
factors="angle" #可以改 angle or xyz
best = "inertia" #可以改 Silhouette or inertia
#########################
import json
import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
from sklearn.cluster import KMeans
from sklearn.metrics import silhouette_score
from matplotlib import cm
import math
#########################
def cartesian_to_spherical(x, y, z):
    rho = math.sqrt(x**2 + y**2 + z**2)
    theta = math.atan2(y, x) # 經度
    phi = math.acos(z / rho) # 緯度
    return theta, phi
def find_best_k(k_values, inertias):
    deltas = np.diff(inertias)
    deltas_diff = np.diff(deltas)
    best_k_index = deltas_diff.argmax() + 1  
    return k_values[best_k_index]
#############data preparation#############
file_path = 'AllWindowData.json'
with open(file_path, 'r') as json_file:
    data = json.load(json_file)

low_positions=[]
high_positions=[]

for window_data in data["windowDatas"]:
    position = window_data["position"]
    window_type = window_data["type"]
    caseNum=window_data["caseNum"]
    if window_type == 0: # for presentation
        if caseNum == 1 or caseNum == 2:
            low_positions.append(position)
        else:
            high_positions.append(position)

if traffic =="low":
    list1 = np.array(low_positions)[:, 0]
    list2 = np.array(low_positions)[:, 1]
    list3 = np.array(low_positions)[:, 2]
elif traffic =="high":
    list1 = np.array(high_positions)[:, 0]
    list2 = np.array(high_positions)[:, 1]
    list3 = np.array(high_positions)[:, 2]
theta_list = []
phi_list = []
if factors=="xyz":
    data=np.column_stack((list1,list3, list2)) #x z y
elif factors =="angle":
    for x, y, z in zip(list1, list3, list2):#x z y
        theta, phi = cartesian_to_spherical(x, y, z) 
        theta_list.append(theta)
        phi_list.append(phi)
    data= np.column_stack((theta_list,phi_list))
###############Kmeans-find best k###########
silhouette_scores = []
inertias = []  
k_values = range(2, 11)
for k in k_values:
    kmeans = KMeans(n_clusters=k)
    kmeans.fit(data)
    # Calculate the silhouette score for this value of k
    silhouette_avg = silhouette_score(data, kmeans.labels_)
    silhouette_scores.append(silhouette_avg)
    # Get the inertia for this value of k
    inertia = kmeans.inertia_
    inertias.append(inertia)
plt.figure()
plt.plot(k_values, silhouette_scores, marker='o', label='Silhouette Score')
plt.plot(k_values, inertias, marker='o', label='Inertia')
best_k_silhouette = k_values[np.argmax(silhouette_scores)]
best_k_inertia = find_best_k(k_values, inertias)
plt.scatter(best_k_silhouette, max(silhouette_scores), color='green', label='Best k (Silhouette)', s=100, zorder=5)
plt.scatter(best_k_inertia, inertias[best_k_inertia-2], color='red', label='Best k (Inertia)', s=100, zorder=5)

plt.xlabel('Number of Clusters (k)')
plt.ylabel('Score')
title=traffic+" using "+factors+" "+': Silhouette Score and Inertia vs Number of Clusters'
plt.title(title)
plt.legend()
file=traffic+" using " +factors
filename = f"{file}.jpg"
plt.savefig(filename)
###############Kmeans-cluster###########
if best == "Silhouette":
    k=best_k_silhouette
elif best == "inertia":
    k=best_k_inertia
if factors =="xyz":
    kmeans = KMeans(n_clusters=k)
    kmeans.fit(data)
    labels = kmeans.labels_
    fig = plt.figure()
    ax = fig.add_subplot(111, projection='3d')
    for i in range(k):
        group_indices= np.where(labels == i)
        print(group_indices)
        ax.scatter(data[group_indices][:, 0], data[group_indices][:, 1], data[group_indices][:, 2], marker='o', label=f'Group {i+1}')
        print("group"+str(i+1)+" x average:"+str(np.mean(np.array(list1)[group_indices])))
        print("group"+str(i+1)+" z average:"+str(np.mean(np.array(list3)[group_indices])))
        print("group"+str(i+1)+" y average:"+str(np.mean(np.array(list2)[group_indices])))
    ax.set_xlabel('X')
    ax.set_ylabel('Z')
    ax.set_zlabel('Y')
    title=traffic+" using "+factors+" to cluster by "+best+" method"
    ax.set_title(title)
    ax.view_init(elev=0,azim=0)
    ax.legend()
    plt.savefig(title)
elif factors =="angle":
    kmeans = KMeans(n_clusters=k)
    kmeans.fit(data)
    labels = kmeans.labels_
    #############plot a blue hemisphere###########
    hemisphere_radius = 2.0  # 半球體半徑
    theta = np.linspace(0, np.pi * 2, 100)
    phi = np.linspace(0, np.pi / 2, 50)
    theta, phi = np.meshgrid(theta, phi)
    cmap = cm.get_cmap('tab10')
    x = hemisphere_radius * np.sin(phi) * np.cos(theta)
    z = hemisphere_radius * np.sin(phi) * np.sin(theta)
    y = hemisphere_radius * np.cos(phi)   
    fig = plt.figure(figsize=(10, 8))  
    ax = fig.add_subplot(111, projection='3d')
    ax.plot_surface(x, y, z, color='b', alpha=0.1)
    #############plot center position###########
    for i in range(k):
        group_indices = np.where(labels == i)
        print(group_indices)
        x_group = hemisphere_radius * np.sin(np.array(phi_list)[group_indices]) * np.cos(np.array(theta_list)[group_indices])
        z_group = hemisphere_radius * np.sin(np.array(phi_list)[group_indices]) * np.sin(np.array(theta_list)[group_indices])
        y_group = hemisphere_radius * np.cos(np.array(phi_list)[group_indices])
        color = cmap(i / k)  # Get a color from the colormap based on the group index
        ax.scatter(x_group, z_group, y_group, color=color, alpha=0.5, s=50, label=f'Group {i+1}')  # Increase the 's' parameter to make points bigger
        print("group"+str(i+1)+"theta average:"+str(np.mean(np.array(theta_list)[group_indices])))
        print("group"+str(i+1)+"phi average:"+str(np.mean(np.array(phi_list)[group_indices])))
    title=traffic+" using "+factors+" to cluster by "+best+" method"
    ax.set_xlabel('X')
    ax.set_ylabel('Z')
    ax.set_zlabel('Y')
    ax.axis('equal')
    ax.set_title(title)
    ax.view_init(elev=0, azim=90)
    plt.savefig(title)
