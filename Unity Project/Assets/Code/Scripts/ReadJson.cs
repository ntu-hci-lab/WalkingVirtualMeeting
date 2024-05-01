using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.Services.Core;
using Unity.Services.CloudSave;

public class ReadJson : MonoBehaviour
{
       
    [SerializeField] private string folder;
    [SerializeField] private string fileName;
    void Start()
    {

    }

    public async void LoadSomeData(string user)
    {
        Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { user });
        Debug.Log("Done: " + savedData[user]);
    }
    public PlayerData LoadPlayerDataFromJSON()
    {
        string jsonString = File.ReadAllText(folder + "/" + fileName);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonString);
        return playerData;
    }
    //private void recovery(PlayerData playerData)
    //{
    //    for (int i = 0; i < playerData.presentation.Count; i++)
    //    {
    //        GameObject p1 =  GameObject.Instantiate(participant, interactable);
    //        applyData(playerData.participant[i], p1);
    //        p1.name = $"participant_{i}";
    //        GameObject p2 = GameObject.Instantiate(presentation, interactable);
    //        applyData(playerData.presentation[i], p2);
    //        p2.name = $"presnetation_{i}";
    //    }
    //}
    //private void applyData(WindowSetting w, GameObject g)
    //{
    //    g.transform.localPosition = float2vector(w.position);
    //    g.transform.localEulerAngles = float2vector(w.rotation);
    //    g.transform.localScale = float2vector(w.scale);
    //    g.transform.FindChildRecursive("RawImage").GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(1f, 1f, 1f, w.transparency);
    //}
    //private Vector3 float2vector(float[] f)
    //{
    //    return new Vector3(f[0], f[1], f[2]);
    //}
}

