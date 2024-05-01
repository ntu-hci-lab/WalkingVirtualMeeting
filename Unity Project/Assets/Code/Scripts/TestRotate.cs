using UnityEngine;

public class TestRotate : MonoBehaviour
{
    [SerializeField] private GameObject webViewContainer;
    [SerializeField] private GameObject hand;
    [SerializeField] private bool isRotating = false;
    private Quaternion localRotation;

    private Vector3 srcOriRotation, webOriRotation;
    private bool rotationSaved = false;

    private void Update()
    {
        if (isRotating)
        {
            if (!rotationSaved)
            {
                srcOriRotation = hand.transform.rotation.eulerAngles;
                webOriRotation = webViewContainer.transform.rotation.eulerAngles;
                localRotation =  Quaternion.Inverse(hand.transform.rotation) * webViewContainer.transform.rotation;
                rotationSaved = true;
            }


            webViewContainer.transform.rotation = hand.transform.rotation * localRotation;
            //Vector3 angle = (hand.transform.rotation.eulerAngles - srcOriRotation);
            //webViewContainer.transform.eulerAngles = webOriRotation + new Vector3(-angle.z, angle.y, angle.x);
        } else
        {
            rotationSaved = false;
        }
    }
    public void startRotating()
    {
        isRotating = true;
    }
    public void stopRotating()
    {
        isRotating = false;
    }
}
