using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrackState : MonoBehaviour
{
    public bool isSetting;
    [SerializeField] private OVRHand hand;
    [SerializeField] private GameObject[] poseList;
    [SerializeField] private bool isLeftHand;
    private bool isTracking = false;
    private int end;
    private PresentationModeSwitch presentationModeSwitch;
    // Start is called before the first frame update
    void Start()
    {
        presentationModeSwitch = GameObject.Find("Switches").GetComponent<PresentationModeSwitch>();
        stopTracking();
        isSetting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!hand.IsTracked && isTracking) // hand start lose tracking
        {
            stopTracking();
            //Debug.Log(hand.gameObject.name + "stop tracking");
        }
        else if(hand.IsTracked && !isTracking) // hand start tracking
        {
            startTracking();
        }
    }
    private void stopTracking()
    {
        for (int i = 0; i < poseList.Length; i++)
        {
            poseList[i].SetActive(false);
        }
        isTracking = false;
    }
    public void startTracking()
    {
        if (isSetting) end = poseList.Length;
        else if (isLeftHand) end = 1;
        else end = 0;
        for (int i = 0; i < end; i++)
        {
            poseList[i].SetActive(true);
        }
        if (isLeftHand) presentationModeSwitch.applySlideMode(isSetting);
        isTracking = true;
    }
    public void applyTracking(bool s)
    {
        stopTracking();
        if (s) end = poseList.Length;
        else if (isLeftHand) end = 1;
        else end = 0;
        for (int i = 0; i < end; i++)
        {
            poseList[i].SetActive(true);
        }
        if (isLeftHand) presentationModeSwitch.applySlideMode(s);
        isTracking = true;
    }
}
