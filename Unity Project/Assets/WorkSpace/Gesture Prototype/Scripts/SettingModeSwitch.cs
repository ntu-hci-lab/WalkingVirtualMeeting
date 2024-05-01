using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingModeSwitch : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject basicManager;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private BodyMovement bodyMovement;
    [SerializeField] private CloudSaveManager cloudSaveManager;
    [SerializeField] private WindowSettingManager windowSettingManager;
    [SerializeField] private HandTrackState handLeft;
    [SerializeField] private HandTrackState handRight;
    [SerializeField] private ItemSelector itemSelector;
    [SerializeField] private FollowPos3D followPos3D;
    [SerializeField] private CanvasOrder canvasOrder;
    [SerializeField] private GameObject presentation;

    [SerializeField] private Transform interactable;
    [SerializeField] private Transform eyeAnchor;
    [SerializeField] private Transform bodyAnchor;
    public bool windowMode; // 0: path, 1: head
    private bool isOpened = false;
    private WindowSetting windowSetting1;
    private WindowSetting windowSetting2;
    private void Start()
    {
        text.SetActive(false);
        basicManager.SetActive(false);
        bodyMovement.startTracking();
        handLeft.isSetting = false;
        handRight.isSetting = false;
        settingPanel.SetActive(false);
        windowSetting1 = windowSettingManager.getPresentationWindow("0");
        windowSetting2 = windowSettingManager.getParticipantWindow("0");
        canvasOrder.enabled = false;
    }
    private void settingStart()
    {
        text.SetActive(true);
        basicManager.SetActive(true);
        bodyMovement.stopTracking();
        handLeft.isSetting = true;
        handRight.isSetting = true;
        handLeft.applyTracking(true);
        handRight.applyTracking(true);
        settingPanel.SetActive(true);
        itemSelector.SelectObject();
        canvasOrder.enabled = true;
        presentation.SetActive(true); // glanceable mode
    }
    private void settingStop()
    {
        text.SetActive(false);
        basicManager.SetActive(false);        
        handLeft.isSetting = false;
        handRight.isSetting = false;
        handLeft.applyTracking(false);
        handRight.applyTracking(false);
        settingPanel.SetActive(false);
        itemSelector.UnselectObject();
        canvasOrder.enabled = false;
        bodyMovement.startTracking();
    }
    public void settingSwitch()
    {
        if (!isOpened) settingStart();
        else settingStop();
        isOpened = !isOpened;
    }
    public void applyWindowBehavior()
    {
        windowSetting1 = windowSettingManager.getPresentationWindow("0");
        windowSetting2 = windowSettingManager.getParticipantWindow("0");
        if (!windowMode) // path anchor
        {
            interactable.parent = bodyAnchor;
            followPos3D.heightStart();
        }
        else // head anchor
        {
            interactable.parent = eyeAnchor;
            followPos3D.heightStop();
        }
        interactable.localEulerAngles = Vector3.zero;
        windowSettingManager.applyWindow(windowSetting1, windowSetting2);
    }
}
