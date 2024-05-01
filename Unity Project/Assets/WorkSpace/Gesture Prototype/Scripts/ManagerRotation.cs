using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ManagerRotation : MonoBehaviour
{
    private bool modifyOn = false;
    private Quaternion originalRotationHand;
    private GameObject hand;

    [SerializeField] private GameObject OculusHand_R;
    [SerializeField] private GameObject OculusHand_L;
    public bool rotationMode; // 0: pitch, 1: yaw
    public GameObject target;
    private float tmp;

    private void Update()
    {
        if (!modifyOn) return;
        
        
        target.transform.rotation = hand.transform.rotation * originalRotationHand;
        if(rotationMode) target.transform.localEulerAngles = new Vector3(calculate1(target.transform.localEulerAngles.x), tmp, 0f);
        else target.transform.localEulerAngles = new Vector3(tmp, calculate(target.transform.localEulerAngles.y), 0f);
    }
    private float calculate(float num)
    {
        //Debug.Log(num+" "+ (num-360));
        num = num % 360;
        if (num > 180) num -= 360;
        num /= 2f;
        return Mathf.Max(-70f, Mathf.Min(70f, num));
    }
    private float calculate1(float num)
    {
        //Debug.Log(num+" "+ (num-360));
        num = num % 360;
        if (num > 180) num -= 360;
        return Mathf.Max(-90f, Mathf.Min(90f, num));
    }

    public void modifyRotationOn(string h)
    {
        modifyOn = true;
        //Debug.Log("modify rotation on");
        if (h == "right") hand = OculusHand_R;
        else if (h == "left") hand = OculusHand_L;
        originalRotationHand = Quaternion.Inverse(hand.transform.rotation) * target.transform.rotation;
        if (!rotationMode) tmp = target.transform.localEulerAngles.x;
        else tmp = target.transform.localEulerAngles.y;

    }
    public void modifyRotationOff()
    {
        modifyOn = false;
        //print("modify rotation off");
    }
    public void pitchMode()
    {
        rotationMode = true;
    }
    public void yawMode()
    {
        rotationMode = false;
    }
}
