using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentationModeSwitch : MonoBehaviour
{
    public bool currentMode; // 0: glancable, 1: fixed
    [SerializeField] private GameObject presentation;
    [SerializeField] private GameObject takeUpLeft; // glanceable show / hide
    [SerializeField] private GameObject doubleTapLeft; // change mode
    public void SwitchMode()
    {
        currentMode = !currentMode;
        applySlideMode(false);
    }
    public void applySlideMode(bool isSetting)
    {
        if (isSetting)
        {
            takeUpLeft.SetActive(false);
            doubleTapLeft.SetActive(false);
            print("apply mode setting mode");
        }
        else if (currentMode) // fixed
        {
            print("apply mode fix mode");
            switch2Fixed();
        }
        else // glanceable
        {
            print("apply mode glance mode");
            switch2Glance();
        }
    }
    private void switch2Glance()
    {
        takeUpLeft.SetActive(true);
        if(presentation.activeSelf) presentation.SetActive(true);
        else presentation.SetActive(false);
        doubleTapLeft.SetActive(true);
    }
    private void switch2Fixed()
    {
        takeUpLeft.SetActive(false);
        presentation.SetActive(true);
        doubleTapLeft.SetActive(true);
    }
}
