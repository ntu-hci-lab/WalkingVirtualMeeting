using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyHorizontalPosition : MonoBehaviour
{
    [SerializeField]
    private Transform fingerTipLeft;
    [SerializeField]
    private Transform fingerTipRight;
    [SerializeField]
    private Transform leftBound;
    [SerializeField]
    private Transform rightBound;
    [SerializeField]
    private float fullLength;
    [SerializeField]
    private float threshold;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject hmd;
    [SerializeField]
    private Transform slider;

    private bool modifyOn = false;
    private float wholeLength;
    private Vector2 firstPos;    
    private float firstTargetPos;
    private Vector2 forward;
    private string refHand;
    void Start()
    {
        
    }

    void Update()
    {
        if (modifyOn)
        {

            //if(refHand == "left" && upperBound.position.y > fingerTipLeft.position.y && lowerBound.position.y < fingerTipLeft.position.y)
            //{
            //    float trans = (Mathf.Min(upperBound.position.y, fingerTipLeft.position.y) - lowerBound.position.y) / wholeLength;
            //    if (mode == 0) mat.color = new Vector4(1f, 1f, 1f, trans);
            //    else if (mode == 1)
            //    {
            //        for (int i = 0; i < tex.Length; i++)
            //        {
            //            tex[i].color = new Vector4(1f, 1f, 1f, trans);
            //        }
            //    }
            //}
            if (refHand == "left")
            {
                Vector2 curPos = new Vector2(fingerTipLeft.position.x, fingerTipLeft.position.z);
                float length = Vector2.Dot(curPos - firstPos, forward);
                float dst = 0f;
                if (firstTargetPos + length > fullLength) dst = fullLength;
                else if (firstTargetPos + length < -fullLength) dst = -fullLength;
                else dst = firstTargetPos + length;
                target.transform.localPosition = new Vector3(dst, 0f, 0f);
                slider.localPosition = new Vector3(-0.0278f + (1f - (dst + fullLength) / (2f * fullLength)) * 0.0582f, -0.0479f, 0f);
            }
            else if (refHand == "right")
            {
                Vector2 curPos = new Vector2(fingerTipRight.position.x, fingerTipRight.position.z);
                float length = Vector2.Dot(curPos - firstPos, forward);
                float dst = 0f;
                if (firstTargetPos + length > fullLength) dst = fullLength;
                else if (firstTargetPos + length < -fullLength) dst = -fullLength;
                else dst = firstTargetPos + length;
                target.transform.localPosition = new Vector3(dst, 0f, 0f);
                //float pct = ;
                slider.localPosition = new Vector3(-0.0278f + (1f - (dst + fullLength) / (2f * fullLength)) * 0.0582f, -0.0479f, 0f);

                //Vector2 curPos = new Vector2(fingerTipRight.position.x, fingerTipRight.position.z);
                //float length = Vector2.Dot(curPos - firstPos, forward) / wholeLength;
                //if (length >= 1f) target.transform.localPosition = new Vector3(fullLength, 0f, 0f);
                //else if (length <= 0f) target.transform.localPosition = new Vector3(-fullLength, 0f, 0f);
                //else target.transform.localPosition = new Vector3(-fullLength + length * 2 * fullLength, 0f, 0f);
                print(dst+" "+ (dst + fullLength) / (2f * fullLength));
            }
            //print(mat.color.a);
        }        
    }
    private void bounce2FixIdx()
    {
        float frame = 120f;
        float pos = target.transform.localPosition.x;
        float dst = Mathf.Round(pos / fullLength) * fullLength;
        float a = (dst - pos) / frame;
        for(int i = 0; i < frame; i++) target.transform.localPosition += new Vector3(a, 0f, 0f);
    }
    public void modifyTransparentOn(string h)
    {
        
        
        refHand = h;
        wholeLength = Vector2.Distance(new Vector2(rightBound.position.x, rightBound.position.z), new Vector2(leftBound.position.x, leftBound.position.z));
        if (refHand == "left") firstPos.Set(fingerTipLeft.position.x, fingerTipLeft.position.z);
        else if (refHand == "right") firstPos.Set(fingerTipRight.position.x, fingerTipRight.position.z);
        //firstPos.Set(leftBound.position.x, leftBound.position.z);
        forward.Set(hmd.transform.right.x, hmd.transform.right.z);
        firstTargetPos = target.transform.localPosition.x;
        print("modify on");
        modifyOn = true;
    }
    public void modifyTransparentOff()
    {
        modifyOn = false;
        print("modify off");
        bounce2FixIdx();
    }

}
