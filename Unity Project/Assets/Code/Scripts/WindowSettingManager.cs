using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WindowSetting
{
    public bool windowType; // 0: presentation, 1: participant
    public float[] position;
    public float[] rotation;
    public float[] scale;
    public float transparency;    
    public string time; // title
    public void set(bool w, Vector3 pos, Vector3 rot, float trans, Vector3 s, string t)
    {
        windowType = w;
        position = new float[3] { pos.x, pos.y, pos.z };
        rotation = new float[3] { rot.x, rot.y, rot.z };
        transparency = trans;
        scale = new float[3] { s.x, s.y, s.z };
        time = t;
    }
    public void update(WindowSetting w)
    {
        position[0] = w.position[0];
        position[1] = w.position[1];
        position[2] = w.position[2];
        rotation[0] = w.rotation[0];
        rotation[1] = w.rotation[1];
        rotation[2] = w.rotation[2];
        transparency = w.transparency;
        scale[0] = w.scale[0];
        scale[1] = w.scale[1];
        scale[2] = w.scale[2];
    }
}

public class WindowSettingManager : MonoBehaviour
{
    [SerializeField] private GameObject presentation;
    [SerializeField] private GameObject participant;
    [SerializeField] private WindowSetting presentationData; // default window setting
    [SerializeField] private WindowSetting participantData; // default window setting

    public WindowSetting getPresentationWindow(string title)
    {
        return getWindowSetting(presentation, title);
    }
    public WindowSetting getParticipantWindow(string title)
    {
        return getWindowSetting(participant, title);
    }
    private WindowSetting getWindowSetting(GameObject target, string title)
    {
        WindowSetting w = new WindowSetting();
        w.set(false, target.transform.localPosition, target.transform.localEulerAngles, target.transform.FindChildRecursive("RawImage").GetComponent<UnityEngine.UI.RawImage>().color.a, target.transform.localScale, title);
        return w;
    }
    public void applyWindow(WindowSetting w1, WindowSetting w2)
    {
        if (w1 == null || w2 == null) return;
        applyData(w1, presentation);
        applyData(w2, participant);
    }
    
    private void applyData(WindowSetting w, GameObject g)
    {
        g.transform.localPosition = float2vector(w.position);
        g.transform.localEulerAngles = float2vector(w.rotation);
        g.transform.localScale = float2vector(w.scale);
        g.transform.FindChildRecursive("RawImage").GetComponent<UnityEngine.UI.RawImage>().color = new Vector4(1f, 1f, 1f, w.transparency);
    }
    public void resetWindow()
    {
        applyWindow(presentationData, participantData);
    }
    private Vector3 float2vector(float[] f)
    {
        return new Vector3(f[0], f[1], f[2]);
    }
}
