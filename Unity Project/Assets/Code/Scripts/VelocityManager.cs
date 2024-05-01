using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
[System.Serializable]
public class VelocityData
{
    public float time;
    public float velocity;
}
public class VelocityManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject headset;
    public CloudSaveManager cloudSaveManager;
    private float updateTime = 1.0f; //先設定為1秒
    private bool start;
    private float elapsedTime;//>=updateTime 會計算一次速度
    private float deltaTime;//計算instant speed
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    string filePath;
    void Start()
    {
        elapsedTime = 0f;
        previousPosition = Vector3.zero;
        currentPosition = Vector3.zero;
        //start = false;
        startRecording();
    }
    void FixedUpdate()
    {
        //Debug.Log(Time.deltaTime);
        //if (Input.GetKeyDown(KeyCode.E) && start != true)//start record
        //{
        //    start = true;
        //    startRecording();
        //}
        //else if (Input.GetKeyDown(KeyCode.Q))//end record
        //{
        //    start = false;
        //}
        //else if (start == true)
        //{
        //    recording();
        //}
        recording();
        previousPosition = headset.transform.position;//使這一frame的位置，變成下一個frame的比較標準
        //print(elapsedTime);
    }

    private void getVelocity()
    {
        float distance = Vector3.Distance(previousPosition, currentPosition);
        float speed = distance / (deltaTime);
        //Debug.Log("deltaTIme=" + deltaTime + " distance=" + distance + " instant Speed=: " + speed);
        //using (StreamWriter writer = new StreamWriter(filePath, true))
        //{
        //    //writer.WriteLine(DateTime.Now + "," + speed);
        //    writer.WriteLine(Time.time + "," + speed);
        //}

        cloudSaveManager.playerData.append(Time.time, speed);
    }
    private void recording()
    {
        elapsedTime += deltaTime = Time.fixedDeltaTime;
        currentPosition = headset.transform.position;
        if (Mathf.Abs(elapsedTime - updateTime) < 0.01f)
        {
            float distance = Vector3.Distance(previousPosition, currentPosition);
            float speed = distance / (deltaTime);
            cloudSaveManager.playerData.append(Time.time, speed);
            //getVelocity();
            elapsedTime = 0f;
        }
    }
    private void startRecording()
    {
        elapsedTime = 0f;
        previousPosition = headset.transform.position;
        //filePath = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
        //using (StreamWriter writer = new StreamWriter(filePath))
        //{
        //    writer.WriteLine("Time, speed"); //csv標題
        //}
    }
}