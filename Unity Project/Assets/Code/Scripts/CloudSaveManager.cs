using UnityEngine;
using Unity.Services.CloudSave;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public string playerId;
    public List<WindowSetting> presentation;
    public List<WindowSetting> participant;
    public List<float> time;
    public List<float> velocity;
    public PlayerData()
    {
        //playerId = GenerateRandomUserID();
        playerId = System.DateTime.Now.ToString("MMdd_HHmmss");
        presentation = new List<WindowSetting>();
        participant = new List<WindowSetting>();
        time = new List<float>();
        velocity = new List<float>();
    }
    public void append(WindowSetting pres, WindowSetting part)
    {
        presentation.Add(pres);
        participant.Add(part);
    }
    public void append(float t, float v)
    {
        time.Add(t);
        velocity.Add(v);
    }
}

public class CloudSaveManager : MonoBehaviour
{
    [SerializeField] private AuthenticationManager authenticationManager;
    [SerializeField] private WindowSettingManager windowSettingManager;
    public PlayerData playerData;
    private void Awake()
    {
        
        playerData = new PlayerData();
        //await UnityServices.InitializeAsync();
        //Debug.Log(AuthenticationService.Instance.IsSignedIn);
        //AuthenticationService.Instance
        //await AuthenticationService.Instance.SignInAnonymouslyAsync();
        
    }
    public async void uploadData()
    {
        if (!authenticationManager.isLogin) return;
        var data = new Dictionary<string, object> { { $"User_{playerData.playerId}", playerData } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        Debug.Log("CloudSave Manager Upload Data");
    }
    public void saveData(string currentTime)
    {
        playerData.append(windowSettingManager.getPresentationWindow(currentTime), windowSettingManager.getParticipantWindow(currentTime));
        uploadData();
    }
    public void updateData(string name)
    {
        int i = findDataIndex(name);
        playerData.presentation[i].update(windowSettingManager.getPresentationWindow(""));
        playerData.participant[i].update(windowSettingManager.getParticipantWindow(""));
    }
    public WindowSetting findData(bool isSlide, string title)
    {
        for (int i = 0; i < playerData.presentation.Count; i++)
        {
            if (isSlide && playerData.presentation[i].time == title) return playerData.presentation[i];
            else if (!isSlide && playerData.participant[i].time == title) return playerData.participant[i];
        }
        return null;
    }
    public int findDataIndex(string time)
    {
        for (int i = 0; i < playerData.presentation.Count; i++)
        {
            if (playerData.presentation[i].time == time) return i;
        }
        return -1;
    }
}
    
