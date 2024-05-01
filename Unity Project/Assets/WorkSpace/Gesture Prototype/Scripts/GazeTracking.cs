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

        // �q���骺��m�A�u�۪��骺�e��g�X�g�u
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
            

            // �p�G�g�u�R������A���L�R�����骺�W��
            //Debug.Log("�g�u�R���F�G" + hit.collider.transform.root.name);
            if (!hitting)
            {
                //Debug.Log("case 1");
                currentHitObject = findSecondAncestor(hit);
                Debug.Log("�g�u�R���F�G" + currentHitObject.name);
                RayCastEnter();
                previousHit = currentHitObject.name;
                hitting = true;
            }
            else if (previousHit != findSecondAncestor(hit).name)
            {
                //Debug.Log("case 2");
                RayCastExit();
                currentHitObject = findSecondAncestor(hit);
                Debug.Log("�g�u�R���F�G" + currentHitObject.name);
                RayCastEnter();
                previousHit = currentHitObject.name;
                hitting = true;
            }

            // �b�o�̥i�H����R������᪺�����ʧ@
            // �Ҧp�GĲ�o�ƥ�B�ܴ������ݩʵ���
        }
        else
        {
            if (hitting)
            {
                hitting = false;
                RayCastExit();
            }
        }
        // ø�s�g�u (VR �ݤ���u)
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
