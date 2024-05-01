using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovingPos2D : MonoBehaviour
{
    [SerializeField]
    private float movingThreshold;
    [SerializeField]
    private float movingThreshold2;
    [SerializeField]
    private Transform src;
    [SerializeField]
    private Transform dst;
    [SerializeField]
    private Vector3 pos;
    private Vector3 curPos = new Vector3(0f, 0f, 0f);
    private Vector3 prePos = new Vector3(0f, 0f, 0f);
    private Vector3 movingDir;
    private Vector3 forward = new Vector3(0f, 0f, 0f);
    private Vector3 preProcessedPos = new Vector3(0f, 0f, 0f);
    private Vector3 processDir;

    //public float threshold; // 移动的阈值
    //public float timeWindow; // 時間窗口的長度
    //private Vector3[] positionRecord;
    //private int currentPositionIndex = 0;
    //private Vector3 displacement;

    private void Start()
    {
        prePos = src.position;
        //int arraySize = Mathf.CeilToInt(timeWindow / Time.fixedDeltaTime);
        //positionRecord = new Vector3[arraySize];
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        print(src.forward + " " + dst.forward);
        // 有移動
        //curPos = src.position;
        //if ((curPos - prePos).magnitude >= movingThreshold)
        //{
        //    updateMovingDirection();
        //}
        //dst.position = new Vector3(src.position.x, 0f, src.position.z) + processDir;
        //prePos = curPos;
        dst.position = new Vector3(src.position.x, 0f, src.position.z) + pos;
        dst.rotation = src.rotation;
    }
    private Vector3 calculateByForward(Vector3 dir, Vector3 v)
    {
        Vector3 right = Vector3.Cross(Vector3.up, dir);
        return dir * v.z + right * v.x + Vector3.up * v.y;
    }
    private void updateMovingDirection()
    {
        movingDir = (curPos - prePos);
        movingDir.Set(movingDir.x, 0f, movingDir.z);
        movingDir = movingDir.normalized;

        Vector3 right = Vector3.Cross(Vector3.up, movingDir);
        preProcessedPos = new Vector3(src.position.x, 0f, src.position.z) + movingDir * pos.z + right * pos.x + Vector3.up * pos.y;
        if ((preProcessedPos - dst.position).magnitude > movingThreshold2)
        {
            processDir = movingDir * pos.z + right * pos.x + Vector3.up * pos.y;
        }
        Debug.Log((preProcessedPos - dst.position).magnitude + " " + movingDir + " " + (curPos - prePos).magnitude + " " + dst.position);
    }
    //private void RecordPosition(Vector3 position)
    //{
    //    positionRecord[currentPositionIndex] = position;
    //    currentPositionIndex = (currentPositionIndex + 1) % positionRecord.Length;
    //}

    //private void CalculateDisplacement(Vector3 position)
    //{
    //    displacement = position - positionRecord[(currentPositionIndex + 1) % positionRecord.Length];
    //    displacement.Set(displacement.x, 0f, displacement.z);

    //    //print("位移量：" + displacement + displacement.magnitude);
    //}
}
