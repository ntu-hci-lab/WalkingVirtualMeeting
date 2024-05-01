using UnityEngine;
using Unity.Services.CloudSave;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.IO;

[System.Serializable]
public class TimeData
{
    public string playerId;
    public List<string> time;
    public List<bool> show;
    public TimeData()
    {
        playerId = System.DateTime.Now.ToString("MMdd_HHmmss");
        time = new List<string>();
        show = new List<bool>();
    }
    public void append(string t, bool v)
    {
        time.Add(t);
        show.Add(v);
    }
}

public class CloudDanger : MonoBehaviour
{
    [SerializeField] private AuthenticationManager authenticationManager;
    public TimeData timeData;
    private void Awake()
    {
        timeData = new TimeData();
    }
    public async void uploadData()
    {
        if (!authenticationManager.isLogin) return;
        var data = new Dictionary<string, object> { { $"User_{timeData.playerId}", timeData } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        Debug.Log("CloudSave Manager Upload Data");
    }
    public void toggle(bool b)
    {
        string str = (System.DateTime.Now.Hour < 10 ? "0" : "") + System.DateTime.Now.Hour + ":" + (System.DateTime.Now.Minute < 10 ? "0" : "") + System.DateTime.Now.Minute + ":" + (System.DateTime.Now.Second < 10 ? "0" : "") + System.DateTime.Now.Second;
        timeData.append(str, b);
        uploadData();
    }
    //public void updateData(string name)
    //{
    //    int i = findDataIndex(name);
    //    playerData.presentation[i].update(windowSettingManager.getPresentationWindow(""));
    //    playerData.participant[i].update(windowSettingManager.getParticipantWindow(""));
    //}
    //public WindowSetting findData(bool isSlide, string title)
    //{
    //    for (int i = 0; i < playerData.presentation.Count; i++)
    //    {
    //        if (isSlide && playerData.presentation[i].time == title) return playerData.presentation[i];
    //        else if (!isSlide && playerData.participant[i].time == title) return playerData.participant[i];
    //    }
    //    return null;
    //}
    //public int findDataIndex(string time)
    //{
    //    for (int i = 0; i < playerData.presentation.Count; i++)
    //    {
    //        if (playerData.presentation[i].time == time) return i;
    //    }
    //    return -1;
    //}
}

