using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPos3D : MonoBehaviour
{
    [SerializeField] private GameObject target;//eye
    [SerializeField] private GameObject interactable;
    [SerializeField] private GameObject interactable2;
    [SerializeField] private float threshold;
    [SerializeField] private float movingSpeed;
    public bool isUpdatingHeight;
    [System.Serializable]
    public struct obj
    {
        public Transform trans;
        public Vector3 pos;
        public Vector3 rot;
    }
    public List<obj> list = new List<obj>();
    private Vector3 pos;
    //private float targetHeadY;
    //private float y;
    private float previuosHeady = 0;
    private float playerHeight = 0;
    private void Start()
    {
        //previuosHeady = target.transform.position.y;
        Debug.Log(previuosHeady + " " + playerHeight);
        Debug.Log("heyheyehy");
    }
    void Update()
    {
        // 第一次取得高度
        if (previuosHeady == 0)
        {
            previuosHeady = target.transform.position.y;
            interactable.transform.position = new Vector3(0f, playerHeight, 0f);
            interactable2.transform.position = new Vector3(0f, playerHeight, 0f);
        }
        // 如果高度差距超過 threshold
        pos = target.transform.position;
        if (Mathf.Abs(pos.y - previuosHeady) > threshold)
        {
            previuosHeady = pos.y;
            Debug.Log(previuosHeady + " " + pos.y);
        }
        smooth();

        // follow 水平座標
        for (int i = 0; i < list.Count; i++)
        {
            list[i].trans.position = new Vector3(pos.x + list[i].pos.x, 0f, pos.z + list[i].pos.z);
        }
        
    }
    private void smooth()
    {
        float y;
        // 使用线性插值（Lerp）来平滑移动物体
        if (isUpdatingHeight)
        {
            y = Mathf.Lerp(interactable.transform.localPosition.y, previuosHeady, Time.deltaTime * movingSpeed);
            interactable.transform.localPosition = new Vector3(0f, y, 0f);
        }
        y = Mathf.Lerp(interactable2.transform.localPosition.y, previuosHeady, Time.deltaTime * movingSpeed);
        interactable2.transform.localPosition = new Vector3(0f, y, 0f);
        //Debug.Log("smooth " + y);
        
        // 当物体接近目标位置时，重置时间参数和目标位置，以实现循环移动
        //if (t >= 1f)
        //{
        //    t = 0f;
        //    Vector3 temp = targetPosition;
        //    targetPosition = objectToMove.position;
        //    objectToMove.position = temp;
        //}
    }
    public void heightStop()
    {
        interactable.transform.localPosition = Vector3.zero;
        isUpdatingHeight = false;
    }
    public void heightStart()
    {
        // interactable.transform.localPosition = ;
        isUpdatingHeight = true;
    }
}
