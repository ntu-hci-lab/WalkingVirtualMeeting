using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColorManager : MonoBehaviour
{
    public bool isSettingMode;
    [SerializeField] private SkinnedMeshRenderer hand;
    [SerializeField] private Material original;
    [SerializeField] private Material setting;
    [SerializeField] private Material pinch;
    [SerializeField] private Material rock;
    [SerializeField] private Material point;
    [SerializeField] private Material thumbUp;
    [SerializeField] private Material palmUp;
    [SerializeField] private Material pinky;
    [SerializeField] private Material ya;
    [SerializeField] private Material danger;
    public void Activate(string pose)
    {
        if (pose == "pinch") hand.material = pinch;
        else if (pose == "rock") hand.material = rock;
        else if (pose == "point") hand.material = point;
        else if (pose == "thumb") hand.material = thumbUp;
        else if (pose == "palm") hand.material = palmUp;
        else if (pose == "ya") hand.material = ya;
        else if (pose == "pinky") hand.material = pinky;
        else if (pose == "danger") hand.material = danger;
    }
    public void inActivate()
    {
        if (isSettingMode) hand.material = setting;
        else hand.material = original;
    }
    public void switchSetting()
    {
        isSettingMode = !isSettingMode;
        //inActivate();
    }
}
