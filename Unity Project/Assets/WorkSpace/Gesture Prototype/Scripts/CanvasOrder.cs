using UnityEngine;

public class CanvasOrder : MonoBehaviour
{
    public Camera mainCamera;
    [SerializeField] private Canvas[] canvas;

    void Update()
    {
        if (Vector3.Distance(mainCamera.transform.position, canvas[0].transform.position) > Vector3.Distance(mainCamera.transform.position, canvas[1].transform.position))
        {
            canvas[0].sortingOrder = -2;
            canvas[1].sortingOrder = -1;
        }
        else
        {
            canvas[0].sortingOrder = -1;
            canvas[1].sortingOrder = -2;
        }
    }
}