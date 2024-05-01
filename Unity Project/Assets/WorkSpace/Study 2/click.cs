using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;
using UnityEngine.UIElements;

public class click : MonoBehaviour
{
    public LayoutSettingManager settingManager;
    public TMP_Text anchor; //head or path
    public TMP_Text env; // low or high
    public TMP_Text type; //
    // Start is called before the first frame update
    string nowAnchor = "Path";
    string nowEnv = "1";
    string nowType = "Listen";
    public GameObject indexFinger;
    bool isSelect_index=false;
    bool isSelect_little=false;
    public GameObject middleFinger;
    public GameObject ringFinger;
    public GameObject littleFinger;
    public GameObject position6botton;
    public GameObject type4botton;
    public GameObject outside;
    public InteractableColorVisual anchor_color;
    public InteractableColorVisual env_color;
    Color color_1;
    Color color_2;
    
    public void indexButton()
    {
        if(isSelect_index==true)
        {
            unselectIndex();
            isSelect_index = false;
        }
        else
        {
            middleFinger.SetActive(false);
            ringFinger.SetActive(false);
            littleFinger.SetActive(false);
            position6botton.SetActive(true);
            outside.SetActive(true);
            if (nowAnchor == "Head")
            {
                outside.SetActive(false);
            }
            isSelect_index = true;
        }
        
    }
    public void unselectIndex()
    {
        indexFinger.SetActive(true);
        middleFinger.SetActive(true);
        ringFinger.SetActive(true);
        littleFinger.SetActive(true);
        position6botton.SetActive(false);
        
    }
    public void unselectLittle()
    {
        indexFinger.SetActive(true);
        middleFinger.SetActive(true);
        ringFinger.SetActive(true);
        type4botton.SetActive(false);
        littleFinger.SetActive(true);
    }
    public void middleButton() // o for path, 1 for head
    {
        
        if (nowAnchor == "Head")
        {
            nowAnchor = "Path";
            anchor.text = nowAnchor;
            settingManager.changeAnchor();
            anchor_color._hoverColorState = anchor_color._normalColorState = new InteractableColorVisual.ColorState() { Color = color_1 };
        }
        else
        {
            nowAnchor = "Head";
            anchor.text = nowAnchor;
            settingManager.changeAnchor();
            anchor_color._hoverColorState = anchor_color._normalColorState = new InteractableColorVisual.ColorState() { Color = color_2 };
        } 
    }
    public void ringButton()
    {
        if (nowEnv == "1")
        {
            nowEnv = "2";
            env.text = nowEnv;
            env_color._hoverColorState = env_color._normalColorState = new InteractableColorVisual.ColorState() { Color = color_2 };
            settingManager.changeEnvironment();
        }
        else
        {
            nowEnv = "1";
            env.text = nowEnv;
            env_color._hoverColorState = env_color._normalColorState = new InteractableColorVisual.ColorState() { Color = color_1 };
            settingManager.changeEnvironment();
        }
    }
    public void littleButton()
    {
        if(isSelect_little==true)
        {

            unselectLittle();
            isSelect_little = false;
        }else
        {
            indexFinger.SetActive(false);
            middleFinger.SetActive(false);
            ringFinger.SetActive(false);
            type4botton.SetActive(true);
            isSelect_little = true;
        }
        
    }

    public void position6Button(int position_n) // 1 for left, 2 for middle, 3 for right, 4 for bottomLeft, 5 for bottom, 6 for outside
    {
        middleFinger.SetActive(true);
        ringFinger.SetActive(true);
        littleFinger.SetActive(true);
        position6botton.SetActive(false);
        settingManager.changeLayout(position_n);
        indexFinger.SetActive(true);
 
    }
    public void type4Button(int type_n) // 1 for slide, 2 for head, 3 for slide only, 4 for head only
    {
        indexFinger.SetActive(true);
        middleFinger.SetActive(true);
        ringFinger.SetActive(true);
        type4botton.SetActive(false);
        settingManager.changeMeeting(type_n);
        littleFinger.SetActive(true);


    }
    void Start()
    {
        
        ColorUtility.TryParseHtmlString("#BFE6F2", out color_1);
        ColorUtility.TryParseHtmlString("#FECBC8", out color_2);
        anchor.text = nowAnchor;
        anchor_color._normalColorState = new InteractableColorVisual.ColorState() { Color = color_1 };
        env.text = nowEnv;
        env_color._normalColorState = new InteractableColorVisual.ColorState() { Color = color_1 };
        position6botton.SetActive(false);
        type4botton.SetActive(false);
        //need initialized all the setting?
        //settingManager.changeLayout(1);//initial left
        //settingManager.changeMeeting(1);//initial slide
        //rotation
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
