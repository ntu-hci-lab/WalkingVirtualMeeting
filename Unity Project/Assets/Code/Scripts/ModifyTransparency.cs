using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyTransparency : MonoBehaviour
{
    [SerializeField]
    private Transform fingerTipLeft;
    [SerializeField]
    private Transform fingerTipRight;
    [SerializeField]
    private Transform upperBound;
    [SerializeField]
    private Transform lowerBound;
    [SerializeField]
    private int mode; // 0: material, 1: texture
    [SerializeField]
    private Material mat;
    [SerializeField]
    private RawImage[] tex;

    private bool modifyOn = false;
    private float wholeLength;    
    private string refHand;
    void Start()
    {
        
    }

    void Update()
    {
        if (modifyOn)
        {
             
            if(refHand == "left" && upperBound.position.y > fingerTipLeft.position.y && lowerBound.position.y < fingerTipLeft.position.y)
            {
                float trans = (Mathf.Min(upperBound.position.y, fingerTipLeft.position.y) - lowerBound.position.y) / wholeLength;
                if (mode == 0) mat.color = new Vector4(1f, 1f, 1f, trans);
                else if (mode == 1)
                {
                    for (int i = 0; i < tex.Length; i++)
                    {
                        tex[i].color = new Vector4(1f, 1f, 1f, trans);
                    }
                }
            }
            else if (refHand == "right" && upperBound.position.y > fingerTipRight.position.y && lowerBound.position.y < fingerTipRight.position.y)
            {
                float trans = (Mathf.Min(upperBound.position.y, fingerTipRight.position.y) - lowerBound.position.y) / wholeLength;
                if (mode == 0) mat.color = new Vector4(1f, 1f, 1f, trans);
                else if (mode == 1)
                {
                    for (int i = 0; i < tex.Length; i++)
                    {
                        tex[i].color = new Vector4(1f, 1f, 1f, trans);
                    }
                }
            }
            print(mat.color.a);
        }        
    }
    public void modifyTransparentOn(string h)
    {
        wholeLength = Mathf.Abs(upperBound.position.y - lowerBound.position.y);
        modifyOn = true;
        refHand = h;
        print("modify on");
    }
    public void modifyTransparentOff()
    {
        modifyOn = false;
        print("modify off");
    }

}
