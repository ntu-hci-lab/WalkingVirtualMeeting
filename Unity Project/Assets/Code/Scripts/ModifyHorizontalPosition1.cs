using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyHorizontalPosition1 : MonoBehaviour
{
    [SerializeField]
    private Transform fingerTipLeft;
    [SerializeField]
    private Transform fingerTipRight;
    //[SerializeField]
    //private Transform leftBound;
    //[SerializeField]
    //private Transform rightBound;
    [SerializeField]
    private float threshold;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject hmd;

    private bool modifyOn = false;
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
                if (target.transform.localPosition.x < 0.09675f) target.transform.localPosition += new Vector3(0.09675f, 0f, 0f);
                modifyOn = false;
            }
            if (refHand == "right")
            {
                if(target.transform.localPosition.x > -0.09675f) target.transform.localPosition -= new Vector3(0.09675f, 0f, 0f);
                modifyOn = false;
            }
            //print(mat.color.a);
        }        
    }
    public void modifyTransparentOn(string h)
    {
        
        modifyOn = true;
        refHand = h;
        //if (refHand == "left") firstPos.Set(fingerTipLeft.position.x, fingerTipLeft.position.z);
        //else if (refHand == "right") firstPos.Set(fingerTipRight.position.x, fingerTipRight.position.z);
        firstTargetPos = target.transform.localPosition.x;
        print("modify on");
    }
    public void modifyTransparentOff()
    {
        modifyOn = false;
        print("modify off");
    }

}
