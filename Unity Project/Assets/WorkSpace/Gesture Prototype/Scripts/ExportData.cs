using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class WindowCorner
{
    public float[] position;
    public WindowCorner(Vector3 v)
    {
        position = new float[3] { v.x, v.y, v.z };
    }
    public WindowCorner(float f1, float f2, float f3)
    {
        position = new float[3] { f1, f2, f3 };
    }
}

[System.Serializable]
public class WindowData
{
    // Design Profile
    public int type; // 0: presentaion, 1: participant
    public int userId; // user number
    public int caseNum; // which case 1, 2, 3, 4
    public int windowMode; // 0: path, 1: head
    // Design Details
    public float transparency;
    public float[] position; // center position
    public float[] rotation; // eular angle
    public float[] normal; // normal vector
    public List<WindowCorner> corner; // 4 corner position: 左上 右上 右下 左下
    public float scale;
    public WindowData(float[] pos, float[] rot, Vector3 n, Vector3 c1, Vector3 c2, Vector3 c3, Vector3 c4, float s)
    {
        position = new float[3] { pos[0], pos[1], pos[2] };
        rotation = new float[3] { rot[0], rot[1], rot[2] };
        normal = new float[3] { n.x, n.y, n.z };
        corner = new List<WindowCorner>();
        corner.Add(new WindowCorner(c1));
        corner.Add(new WindowCorner(c2));
        corner.Add(new WindowCorner(c3));
        corner.Add(new WindowCorner(c4));
        scale = s;
        //corner.Add(new float[3] { c1.x, c1.y, c1.z });
        //corner.Add(new float[3] { c2.x, c2.y, c2.z });
        //corner.Add(new float[3] { c3.x, c3.y, c3.z });
        //corner.Add(new float[3] { c4.x, c4.y, c4.z });
    }
    public WindowData(WindowData w)
    {
        type = w.type;
        userId = w.userId;
        caseNum = w.caseNum;
        transparency = w.transparency;
        position = new float[3] { w.position[0], w.position[1], w.position[2] };
        rotation = new float[3] { w.rotation[0], w.rotation[1], w.rotation[2] };
        normal = new float[3] { w.normal[0], w.normal[1], w.normal[2] };
        corner = new List<WindowCorner>();
        corner.Add(new WindowCorner(w.corner[0].position[0], w.corner[0].position[1], w.corner[0].position[2]));
        corner.Add(new WindowCorner(w.corner[1].position[0], w.corner[1].position[1], w.corner[1].position[2]));
        corner.Add(new WindowCorner(w.corner[2].position[0], w.corner[2].position[1], w.corner[2].position[2]));
        corner.Add(new WindowCorner(w.corner[3].position[0], w.corner[3].position[1], w.corner[3].position[2]));
        scale = w.scale;
    }
}
[System.Serializable]
public class WindowDataAnalysis
{
    public List<WindowData> windowDatas;
    public WindowDataAnalysis()
    {
        windowDatas = new List<WindowData>();
    }
    public void add(WindowData w)
    {
        windowDatas.Add(w);
    }

}
public class ExportData : MonoBehaviour
{
    public WindowDataAnalysis windowDataAnalysis;
    public string filePath;
    // Start is called before the first frame update
    void Start()
    {
        windowDataAnalysis = new WindowDataAnalysis();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void finish()
    {
        string json = JsonUtility.ToJson(windowDataAnalysis);

        // 输出 JSON 字符串
       // Debug.Log("JSON Data: " + json);

        // 将 JSON 数据写入文件
        //string filePath = Application.persistentDataPath + "/playerData.json";
       // string filePath = "windowSetting/AllWindowData_normalized.json";
        File.WriteAllText(filePath, json);

        //Debug.Log("JSON Data written to file: " + filePath);
    }
    public void addWindowData(string inputString, WindowData w)
    {
        // Use string.Format to parse the string and extract the numbers and text
        int userNumber, caseNumber;
        string text;
        if (TryParseFormattedString(inputString, out userNumber, out caseNumber, out text))
        {
            //Debug.Log("User Number: " + userNumber);
            //Debug.Log("Case Number: " + caseNumber);
            //Debug.Log("Text: " + text);
            w.userId = userNumber;
            w.caseNum = caseNumber;

            // Determine the type of the string based on the extracted text
            if (text == "path")
            {
                w.windowMode = 0;
                //Debug.Log("It is type: path");
            }
            else if (text == "head")
            {
                w.windowMode = 1;
                //Debug.Log("It is type: head");
            }
            else if (text == "both")
            {
                w.windowMode = 0;
                windowDataAnalysis.add(w);
                //print(inputString);
                w = new WindowData(w);
                w.windowMode = 1;
                //Debug.Log("It is type: both");
            }
            windowDataAnalysis.add(w);
            //print(inputString);
        }
        else
        {
            Debug.Log("Invalid string format");
        }
    }

    public bool TryParseFormattedString(string input, out int userNumber, out int caseNumber, out string text)
    {
        // Use string.Format to parse the string and extract the numbers and text
        string formattedString = string.Format("User {0} Case {1} {2}", "{0}", "{1}", "{2}");
        if (string.IsNullOrEmpty(input) || input.Length < formattedString.Length)
        {
            userNumber = 0;
            caseNumber = 0;
            text = null;
            //return false;
        }

        string[] split = input.Split(' ');
        if (split.Length < 5 || !split[0].Equals("User") || !split[2].Equals("Case") ||
            !int.TryParse(split[1], out userNumber) || !int.TryParse(split[3], out caseNumber))
        {
            userNumber = 0;
            caseNumber = 0;
            text = null;
            //return false;
        }

        text = split[4].ToLower();
        return true;
    }
}
