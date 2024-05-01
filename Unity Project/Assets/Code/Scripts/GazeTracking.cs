using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTracking : MonoBehaviour
{
    public float raycastDistance = 10e6f;
    private string previousHit = "";
    private Transform currentHitObject;
    private Transform hit;
    private bool hitting = false;
    private GameObject curGameObject;
    [SerializeField] private GameObject manager;
    private ManagerPosition managerPosition;
    private ManagerRotation managerRotation;
    private ManagerResize managerResize;
    private ManagerTransparency managerTransparency;

    private void Start()
    {
        managerPosition = manager.GetComponent<ManagerPosition>();
        managerResize = manager.GetComponent<ManagerResize>();
        managerRotation = manager.GetComponent<ManagerRotation>();
        managerTransparency = manager.GetComponent<ManagerTransparency>();
    }
    void Update()
    {
        //Transform hit;
        RaycastHit[] hits;

        // 從物體的位置，沿著物體的前方射出射線
        hits = Physics.RaycastAll(transform.position, transform.forward, raycastDistance);
        hit = null;
        for(int i = 0; i < hits.Length; i++)
        {
            //print(hits[i].collider.gameObject.name);
            if (hits[i].collider.transform.CompareTag("RayCastTarget"))
            {
                hit = hits[i].collider.transform;
                break;
            }
        }
        if (hit != null && hits.Length > 0)
        {
            

            // 如果射線命中物體，打印命中物體的名稱
            //Debug.Log("射線命中了：" + hit.collider.transform.root.name);
            if (!hitting)
            {
                //Debug.Log("case 1");
                currentHitObject = findSecondAncestor(hit);
                Debug.Log("射線命中了：" + currentHitObject.name);
                RayCastEnter();
                previousHit = currentHitObject.name;
                hitting = true;
            }
            else if (previousHit != findSecondAncestor(hit).name)
            {
                //Debug.Log("case 2");
                RayCastExit();
                currentHitObject = findSecondAncestor(hit);
                Debug.Log("射線命中了：" + currentHitObject.name);
                RayCastEnter();
                previousHit = currentHitObject.name;
                hitting = true;
            }

            // 在這裡可以執行命中物體後的相應動作
            // 例如：觸發事件、變換物體屬性等等
        }
        else
        {
            if (hitting)
            {
                hitting = false;
                RayCastExit();
            }
        }
        // 繪製射線 (VR 看不到線)
        //Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red);
        //Debug.DrawLine(transform.position, transform.forward * raycastDistance, Color.red);

    }
    private void RayCastEnter()
    {
        currentHitObject.transform.FindChildRecursive("Background").gameObject.GetComponent<MeshRenderer>().enabled = true;
        
        managerPosition.target = currentHitObject.gameObject;
        managerRotation.target = currentHitObject.gameObject;
        managerResize.target = currentHitObject.gameObject;
        managerTransparency.target = currentHitObject.FindChildRecursive("RawImage").GetComponent<UnityEngine.UI.RawImage>();
        manager.SetActive(true);
        print("RayCastEnter");
    }
    public void RayCastExit()
    {
        print("RayCastExit");
        if(currentHitObject != null) currentHitObject.transform.FindChildRecursive("Background").gameObject.GetComponent<MeshRenderer>().enabled = false;
        //hit = null;
        manager.SetActive(false);
    }
    private void RayCastStop()
    {
        curGameObject.transform.FindChildRecursive("Background").gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private Transform findSecondAncestor(Transform t)
    {
        // 取得物件的父級
        Transform parent = t.parent;

        if (parent != null)
        {
            // 如果父級物件存在，再次取得其父級物件
            Transform secondAncestor = parent.parent;

            if (secondAncestor.transform.CompareTag("RayCastTarget"))
            {
                // 如果第二個祖先物件存在，可以在此進行相應的操作
                //Debug.Log("第二個祖先物件的名稱為：" + secondAncestor.gameObject.name);
                return secondAncestor.transform;
            }
            else
            {
                //Debug.Log("物件沒有第二個祖先物件");
            }
        }
        else
        {
            //Debug.Log("物件沒有父級物件");
        }
        return null;
    }
}
