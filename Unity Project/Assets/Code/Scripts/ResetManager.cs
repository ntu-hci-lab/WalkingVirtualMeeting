using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    [SerializeField] private GameObject presentation;
    [SerializeField] private WindowSetting presentationData;
    [SerializeField] private GameObject participant;
    [SerializeField] private WindowSetting participantData;
    public void resetWindow()
    {
        applyData(presentationData, presentation);
        applyData(participantData, participant);
    }
    public void applyWindow(WindowSetting w1, WindowSetting w2)
    {
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
    private Vector3 float2vector(float[] f)
    {
        return new Vector3(f[0], f[1], f[2]);
    }
}
