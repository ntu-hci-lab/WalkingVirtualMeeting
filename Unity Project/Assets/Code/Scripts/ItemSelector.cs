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
        // ���o���󪺤���
        Transform parent = t.parent;

        if (parent != null)
        {
            // �p�G���Ū���s�b�A�A�����o����Ū���
            Transform secondAncestor = parent.parent;

            if (secondAncestor.transform.CompareTag("RayCastTarget"))
            {
                // �p�G�ĤG�ӯ�������s�b�A�i�H�b���i��������ާ@
                //Debug.Log("�ĤG�ӯ������󪺦W�٬��G" + secondAncestor.gameObject.name);
                return secondAncestor.transform;
            }
            else
            {
                //Debug.Log("����S���ĤG�ӯ�������");
            }
        }
        else
        {
            //Debug.Log("����S�����Ū���");
        }
        return null;
    }
}
