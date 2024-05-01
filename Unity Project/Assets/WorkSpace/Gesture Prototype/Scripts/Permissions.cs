using UnityEngine;
using Vuplex.WebView;
using UnityEngine.Android;

public class Permissions : MonoBehaviour
{
    void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        Web.SetCameraAndMicrophoneEnabled(true);
    }
}