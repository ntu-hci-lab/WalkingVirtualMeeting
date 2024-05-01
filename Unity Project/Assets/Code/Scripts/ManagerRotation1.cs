using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerRotation1 : MonoBehaviour
{
    //[SerializeField] private RectTransform canvasRectTransform;
    public GameObject target;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private float CDGain = 5000f;

    // states
    private bool rightOK = false;
    private bool leftOK = false;
    private bool resizing = false;

    private Vector3 previousPositionLeft;
    private Vector3 previousPositionRight;

    private void Update()
    {
        if (resizing)
        {
            Vector3 currentPositionLeft = leftHand.transform.position;
            Vector3 displacementLeft = currentPositionLeft - previousPositionLeft;
            Vector3 currentPositionRight = rightHand.transform.position;
            Vector3 displacementRight = currentPositionRight - previousPositionRight;

            float previousDistance = Vector3.Distance(previousPositionLeft, previousPositionRight);
            float currentDistance = Vector3.Distance(currentPositionLeft, currentPositionRight);



            Vector2 displacementTotal = new Vector2(
                Mathf.Abs(displacementLeft.x - displacementRight.x),
                Mathf.Abs(displacementLeft.y - displacementRight.y)
            );
            //float displacementTotal = Mathf.Abs(displacementLeft.y - displacementRight.y);

            // Shrink
            if (previousDistance > currentDistance)
            {
                displacementTotal *= -1;
            }

            ResizeCanvas(displacementTotal);

            previousPositionLeft = currentPositionLeft;
            previousPositionRight = currentPositionRight;
        }
    }

    private void ResizeCanvas(Vector2 sizeDeltaChange)
    {
        //Debug.Log(sizeDeltaChange);
        //Debug.Log(target.sizeDelta);
        //Debug.Log("##############");
        target.transform.localScale = new Vector3(target.transform.localScale.x + sizeDeltaChange.x * CDGain, target.transform.localScale.y + sizeDeltaChange.x * CDGain, target.transform.localScale.z);
        //target.transform.localScale *= sizeDeltaChange;
    }

    #region Pose Actions
    public void StartRight()
    {
        rightOK = true;
        if (leftOK)
        {
            resizing = true;
            previousPositionLeft = leftHand.transform.position;
            previousPositionRight = rightHand.transform.position;
        }
    }

    public void StopRight()
    {
        rightOK = false;
        resizing = false;
    }

    public void StartLeft()
    {
        leftOK = true;
        if (rightOK)
        {
            resizing = true;
            previousPositionLeft = leftHand.transform.position;
            previousPositionRight = rightHand.transform.position;
        }
    }

    public void StopLeft()
    {
        leftOK = false;
        resizing = false;
    }
    #endregion
}
