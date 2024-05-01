using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.Services.Core;
using Unity.Services.CloudSave;

public class RecoveryData : MonoBehaviour
{
    public bool export;
    [SerializeField] private GameObject presentation;
    [SerializeField] private GameObject participant;
    [SerializeField] private GameObject title;
    [SerializeField] private Transform interactable;
    [SerializeField] private ExportData exportData;
    [SerializeField] private string[] filePath;
    [SerializeField] private string folder;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            print(i);
            PlayerData playerData = LoadPlayerDataFromJSON(folder + "/" + filePath[i]);
            recovery(playerData);
        }
        //exportData.finish();
    }

    public async void LoadSomeData(string user)
    {
        Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { user });
        Debug.Log("Done: " + savedData[user]);
    }
    private PlayerData LoadPlayerDataFromJSON(string filePath)
    {
        string jsonString = File.ReadAllText(filePath); // read text
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonString); // turn into structure
        return playerData;
    }
    private void recovery(PlayerData playerData)
    {
        //Debug.Log(playerData.playerId + " pres: " + playerData.presentation.Count + " part: "+ playerData.participant.Count);
        for (int i = 0; i < playerData.presentation.Count; i++)
        {

            // parent object
            GameObject p = new GameObject($"{playerData.playerId} {playerData.presentation[i].time}"); // create empty
            p.transform.parent = interactable;
            p.transform.localPosition = Vector3.zero;
           // participant
           //GameObject p1 = GameObject.Instantiate(participant, p.transform);
           // applyData(playerData.participant[i], p1);
           // p1.name = "participant";
            // presentation
            GameObject p2 = GameObject.Instantiate(presentation, p.transform);
            applyData(playerData.presentation[i], p2);
            p2.name = "presnetation";
            

            // title
            //GameObject t = GameObject.Instantiate(title, p.transform);
            //t.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = ConvertString($"{playerData.playerId} {playerData.presentation[i].time}");


            //if (!export) p2.transform.forward = Vector3.Normalize(new Vector3(p2.transform.localPosition.x, p2.transform.localPosition.y, p2.transform.localPosition.z));
            p.SetActive(false);
            // participant
            GameObject p1 = GameObject.Instantiate(participant, p.transform);
            applyData(playerData.participant[i], p1);
            p1.name = "participant";

            if (!export)
            {
                
                //WindowData w1 = new WindowData(playerData.participant[i].position, playerData.participant[i].rotation, p1.transform.forward, p1.transform.GetChild(0).position, p1.transform.GetChild(1).position, p1.transform.GetChild(2).position, p1.transform.GetChild(3).position, playerData.participant[i].scale[0]);
                //w1.type = 1;
                //w1.transparency = playerData.participant[i].transparency;
                //exportData.addWindowData(p.name, w1);
                //WindowData w2 = new WindowData(playerData.presentation[i].position, playerData.presentation[i].rotation, p2.transform.forward, p2.transform.GetChild(0).position, p2.transform.GetChild(1).position, p2.transform.GetChild(2).position, p2.transform.GetChild(3).position, playerData.presentation[i].scale[0]);
                //w2.type = 0;
                //w2.transparency = playerData.presentation[i].transparency;
                //exportData.addWindowData(p.name, w2);

                // title
                //GameObject t = GameObject.Instantiate(title, p.transform);
                //t.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = ConvertString($"{playerData.playerId} {playerData.presentation[i].time}");
            }
        }
    }
    private void applyData(WindowSetting w, GameObject g)
    {
        g.transform.localPosition = float2vector(w.position);
        g.transform.localEulerAngles = float2vector(w.rotation);
        g.transform.localScale = float2vector(w.scale);
        g.transform.FindChildRecursive("RawImage").GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(1f, 1f, 1f, w.transparency);
    }
    private Vector3 float2vector(float[] f)
    {
        return new Vector3(f[0], f[1], f[2]);
    }
    string ConvertString(string input)
    {
        // 将输入字符串按空格分割成单词数组
        string[] words = input.Split(' ');

        // 提取数字部分和最后一个字母
        string userNumber = words[1];
        string caseNumber = words[3];
        string text = words[4].Substring(0, 1).ToUpper();

        // 组合成新的格式
        string convertedString = userNumber + " " + caseNumber + " " + text;

        return convertedString;
    }
}

