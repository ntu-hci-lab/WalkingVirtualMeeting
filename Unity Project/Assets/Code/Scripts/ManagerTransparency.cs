using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerTransparency : MonoBehaviour
{
    [SerializeField] private Transform fingerTipLeft;
    [SerializeField] private Transform fingerTipRight;
    public RawImage target;
    //public Material target;
    [SerializeField] private float scaler;
    [SerializeField] private float minTransparency;
    [SerializeField] private TMPro.TMP_Text tmp;

    private bool modifyOn = false;
    // private float wholeLength = 0.2f;  // 實測出 0.2 的結果
    private Transform refHand;
    private float fingerStartingPoint;
    private float originalTransparency;
    void Start()
    {

    }

    void Update()
    {
        if (!modifyOn) return;
        float displacement = refHand.position.y - fingerStartingPoint;        
        float trans = originalTransparency + displacement * scaler;
        trans = Mathf.Min(1f, Mathf.Max(minTransparency, trans)) ;
        //print(displacement + " " + trans);
        target.color = new Vector4(1f, 1f, 1f, trans);
        tmp.text = $"{Mathf.RoundToInt(trans*100f)}%";

    }
    public void modifyTransparentOn(string h)
    {
        modifyOn = true;
        if (h == "right") refHand = fingerTipRight;
        else if (h == "left") refHand = fingerTipLeft;
        fingerStartingPoint = refHand.position.y;
        originalTransparency = target.color.a;
        tmp.gameObject.SetActive(true);
        //Debug.Log("modify on");
    }
    public void modifyTransparentOff()
    {
        modifyOn = false;
        tmp.gameObject.SetActive(false);
        //print("modify off");
    }

}
