using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] private GameObject presentation;
    [SerializeField] private GameObject participant;
    private GameObject curGameObject;
    [SerializeField] private GameObject manager;
    private ManagerPosition managerPosition;
    private ManagerRotation managerRotation;
    private ManagerResize managerResize;
    private ManagerTransparency managerTransparency;
    public bool isSlide; // now selected

    private void Start()
    {
        managerPosition = manager.GetComponent<ManagerPosition>();
        managerResize = manager.GetComponent<ManagerResize>();
        managerRotation = manager.GetComponent<ManagerRotation>();
        managerTransparency = manager.GetComponent<ManagerTransparency>();
        curGameObject = presentation;
    }
    
    public void SelectObject()
    {
        if (isSlide) curGameObject = presentation;
        else curGameObject = participant;
        // show white border
        curGameObject.transform.FindChildRecursive("Background").gameObject.GetComponent<MeshRenderer>().enabled = true;
        
        managerPosition.target = curGameObject;
        managerRotation.target = curGameObject;
        managerResize.target = curGameObject;
        managerTransparency.target = curGameObject.transform.FindChildRecursive("RawImage").GetComponent<UnityEngine.UI.RawImage>();
        manager.SetActive(true);
    }
    public void UnselectObject()
    {
        curGameObject.transform.FindChildRecursive("Background").gameObject.GetComponent<MeshRenderer>().enabled = false;
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
