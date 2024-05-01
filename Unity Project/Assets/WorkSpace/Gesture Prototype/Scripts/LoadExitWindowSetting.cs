using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadExitWindowSetting : MonoBehaviour
{
    [SerializeField] private string folder;
    [SerializeField] private string fileName;
    [SerializeField] private Transform interactable;
    [SerializeField] private CloudSaveManager cloudSaveManager;
    //[SerializeField] private WindowSettingManager windowSetting;
    [SerializeField] private SettingsPanelManager settingsPanelManager;
    //private ReadJson readJson;
    void Start()
    {
        PlayerData p = LoadPlayerDataFromJSON();
        for(int i = 0; i < p.presentation.Count; i++)
        {
            print("add");
            cloudSaveManager.playerData.append(p.presentation[i], p.participant[i]);
            settingsPanelManager.AppendSetting(cloudSaveManager.playerData.presentation[i].time);
        }
        
    }
    public PlayerData LoadPlayerDataFromJSON()
    {
        string jsonString = File.ReadAllText(folder + "/" + fileName);
        //string s = "{\"participant\":[{\"position\":[0.255103052,-0.137742043,0.5463557],\"rotation\":[16.64414,16.0281181,2.22777246E-07],\"scale\":[0.5,0.5,1],\"time\":\"Case 3 Path\",\"transparency\":0.479930162,\"windowType\":false},{\"position\":[0.218495682,-0.215335876,0.7194167],\"rotation\":[16.64414,16.0281181,2.227773E-07],\"scale\":[0.5,0.5,1],\"time\":\"Case 3 Head\",\"transparency\":0.232418776,\"windowType\":false},{\"position\":[0.07817625,-0.0384151936,0.88886714],\"rotation\":[15.2229633,3.63630342,0],\"scale\":[0.5,0.5,1],\"time\":\"Case 4 Both\",\"transparency\":0.540881157,\"windowType\":false}],\"playerId\":\"\",\"presentation\":[{\"position\":[0.009126384,-0.2108972,0.661213],\"rotation\":[18.628746,3.74818087,-5.631109E-08],\"scale\":[0.9980935,0.9980935,1],\"time\":\"Case 3 Path\",\"transparency\":0.505432367,\"windowType\":false},{\"position\":[0.0239914358,-0.214882612,0.6930861],\"rotation\":[11.1267433,6.37627745,0],\"scale\":[0.9980935,0.9980935,1],\"time\":\"Case 3 Head\",\"transparency\":0.254976571,\"windowType\":false},{\"position\":[0.058724314,-0.198039532,0.836708963],\"rotation\":[11.126749,5.0048337,5.43831078E-08],\"scale\":[0.9980935,0.9980935,1],\"time\":\"Case 4 Both\",\"transparency\":0.538237631,\"windowType\":false}]}";
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonString);
        return playerData;
    }
}
