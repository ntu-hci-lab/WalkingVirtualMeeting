using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class LayoutSetting // 整體位置
{
    // info
    public int posType; // 1 ~ 5
    public bool anchorMode; // 0: path, 1: head
    // parameter
    public float[] position;
    public float[] rotation;
    public float scale;
}

[System.Serializable]
public class MeetingSetting // 會議
{
    // info
    public int meetingType; // 1 ~ 4
    public bool environmentType;  // 0: low, 1: high

    // parametet
    public bool presentationShow;
    public float[] presentationPosition;
    public float presentationScale;
    public float presentationOpacity;

    public bool participantShow;
    public float[] participantPosition;
    public float participantScale;
    public float partcipantOpacity;
}

[System.Serializable]
public class AllSetting
{
    public LayoutSetting[] layouts;
    public MeetingSetting[] meetings;
}

public class LayoutSettingManager : MonoBehaviour
{
    //[SerializeField] private string layoutPath;
    //[SerializeField] private string meetingPath;
    //private LayoutSetting layoutSetting;
    //private MeetingSetting meetingSetting;
    [SerializeField] private string allPath;
    [SerializeField] private AllSetting allSetting;

    [SerializeField] private Transform interactable;
    [SerializeField] private Transform centerEye;
    [SerializeField] private Transform layoutGroup;
    private Transform presentation;
    private Transform participant;

    // current layout info
    private int info_layout = 1; // 1 ~ 5
    private bool info_anchor = false;
    private bool info_environment = false;
    private int info_meeting = 1; // 1 ~ 4

    // Start is called before the first frame update
    void Start()
    {
        //layoutSetting = LoadDataFromJSON<LayoutSetting>(layoutPath);
        //meetingSetting = LoadDataFromJSON<MeetingSetting>(meetingPath);
        //allSetting = LoadDataFromJSON<AllSetting>(allPath);
        presentation = layoutGroup.GetChild(0);
        participant = layoutGroup.GetChild(1);
        updateAll();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            exportJSON<AllSetting>(allPath);
        }
    }

    private T LoadDataFromJSON<T>(string filePath)
    {
        string jsonString = File.ReadAllText(filePath); // read text
        T data = JsonUtility.FromJson<T>(jsonString); // turn into structure
        return data;
    }
    private void exportJSON<T>(string filePath)
    {
        string json = JsonUtility.ToJson(allSetting);
        System.IO.File.WriteAllText(filePath, json);
    }
    private LayoutSetting FindLayout(int posType, bool anchorMode, bool environment)
    {
        for(int i = 0; i < allSetting.layouts.Length; i++)
        {
            LayoutSetting l = allSetting.layouts[i];
            if (l.posType == posType && l.anchorMode == anchorMode) return l;
        }
        return null;
    }
    private void applyLayout(LayoutSetting l)
    {
        if (!l.anchorMode) // path
        {
            layoutGroup.parent = interactable;
        }
        else // head
        {
            layoutGroup.parent = centerEye;
        }
        layoutGroup.localPosition = new Vector3(l.position[0], l.position[1], l.position[2]);
        layoutGroup.localEulerAngles = new Vector3(l.rotation[0], l.rotation[1], l.rotation[2]);
        layoutGroup.localScale = new Vector3(l.scale, l.scale, l.scale);
        //if (l.posType == 1 && l.anchorMode == false && info_meeting == 1)
        //{
        //    participant.localPosition = new Vector3(0f, -0.317f, 0f);
        //}
        //layoutGroup.forward = Vector3.Normalize(layoutGroup.transform.position - centerEye.transform.position);
        //presentation.forward = Vector3.Normalize(presentation.transform.position - centerEye.transform.position);
        //participant.forward = Vector3.Normalize(participant.transform.position - centerEye.transform.position);
    }
    private void updateLayout()
    {
        
        applyLayout(FindLayout(info_layout, info_anchor, info_environment));
    }
    private MeetingSetting FindMeeting(int meeting, bool environment)
    {
        for (int i = 0; i < allSetting.meetings.Length; i++)
        {
            MeetingSetting m = allSetting.meetings[i];
            if (m.meetingType == meeting && m.environmentType == environment) return m;
        }
        return null;
    }
    private void updateMeeting()
    {
        MeetingSetting m = FindMeeting(info_meeting, info_environment);

        //presentation.localPosition = new Vector3(m.presentationPosition[0], m.presentationPosition[1], m.presentationPosition[1]);
        presentation.localScale = new Vector3(m.presentationScale, m.presentationScale, 1f);
        //presentation.forward = Vector3.Normalize(presentation.transform.position - centerEye.transform.position);
        presentation.gameObject.SetActive(m.presentationShow);
        presentation.FindChildRecursive("RawImage").GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(1f, 1f, 1f, m.presentationOpacity);

        participant.localPosition = new Vector3(m.participantPosition[0], m.participantPosition[1], m.participantPosition[2]);
        participant.localScale = new Vector3(m.participantScale, m.participantScale, 1f);
        //participant.forward = Vector3.Normalize(participant.transform.position - centerEye.transform.position);
        participant.gameObject.SetActive(m.participantShow);
        participant.FindChildRecursive("RawImage").GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(1f, 1f, 1f, m.partcipantOpacity);

        
    }
    private void updateAll()
    {
        updateMeeting();
        updateLayout();
        if (info_layout == 1)
        {
            if (info_meeting == 1 && info_environment == false) participant.localPosition = new Vector3(0f, -0.317f, 0f);
            else if (info_meeting == 1 && info_environment == true) participant.localPosition = new Vector3(0f, -0.274f, 0f);
            else if (info_meeting == 2 && info_environment == false) participant.localPosition = new Vector3(0f, -0.29f, 0f);
            else if (info_meeting == 2 && info_environment == true) participant.localPosition = new Vector3(0f, -0.265f, 0f);
        }
    }
    private Vector3 float2vector(float[] f)
    {
        return new Vector3(f[0], f[1], f[2]);
    }
    //private void updateEnvironment()
    //{
    //    MeetingSetting m = FindMeeting(info_meeting, info_environment);
    //    presentation.FindChildRecursive("RawImage").GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(1f, 1f, 1f, m.presentationOpacity);
    //    participant.FindChildRecursive("RawImage").GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(1f, 1f, 1f, m.partcipantOpacity);
    //}

    // update info
    public void changeLayout(int i)
    {
        info_layout = i;
        updateAll();
    }
    public void changeAnchor()
    {
        info_anchor = !info_anchor;
        updateAll();
    }
    public void changeEnvironment()
    {
        info_environment = !info_environment;
        updateAll();
    }
    public void changeMeeting(int i)
    {
        info_meeting = i;
        updateAll();
    }
}
