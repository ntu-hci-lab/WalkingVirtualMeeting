using Oculus.Interaction;
using UnityEngine;
using Vuplex.WebView;

public class TestInput : MonoBehaviour
{
    [SerializeField] private CanvasWebViewPrefab webView;
    /// <summary>
    /// The number of pixels scrolled for each unit movement in Unity worldspace or for each button press
    /// </summary>
    [SerializeField] private float scrollSensitivity = 100.0f;
    /// <summary>
    /// The dimensions in *pixels* after resizing
    /// </summary>
    [SerializeField] private Vector2 resizeDimensions;
    private Vector3[] browserCorners;

    [SerializeField] private RayInteractor rayInteractor;
    [SerializeField] private IndexPinchSelector indexPinchSelector;
    bool pinchDetected = false;

    private void Start()
    {
        browserCorners = new Vector3[4];
    }

    private void Update()
    {
        if (rayInteractor == null || rayInteractor.CollisionInfo == null || rayInteractor.CollisionInfo.Value.Point == null) return;

        Vector3 cursorPosition = rayInteractor.CollisionInfo.Value.Point;
        if (cursorPosition == null) return;

        if(indexPinchSelector._isIndexFingerPinching)
        {
            if (!pinchDetected)
            {
                pinchDetected = true;
                Vector2 mousePos = GetMousePositionByRay(cursorPosition);
                Debug.Log("clicked by rayInteractor" + mousePos);
                webView.WebView.Click((int)mousePos.x, (int)mousePos.y);
            }
        } else
        {
            pinchDetected = false;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // mouse click
        {
            if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit))
            {
                Vector2 mousePos = GetMousePositionByRay(cursorPosition);
                Debug.Log("clicked" + mousePos);
                webView.WebView.Click((int)mousePos.x, (int)mousePos.y);
            }
        } else if (Input.GetKeyDown(KeyCode.S)) // scroll
        {
            if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit))
            {
                Vector2 mousePos = GetMousePositionByRay(cursorPosition);
                webView.WebView.Scroll(webView.WebView.PointToNormalized(0, (int)scrollSensitivity), webView.WebView.PointToNormalized((int)mousePos.x, (int)mousePos.y));

                // Unable to test in Editor due to need to use both Game and Scene View at the same time
                /*
                Vector2 prevPos = mousePos;

                while (Input.GetKey(KeyCode.S))
                {
                    if (prevPos != this.transform.position) {
                        int scrollDeltaY = (int)(scrollSensitivity * (this.transform.position.y - prevPos.y));
                        webView.WebView.Scroll(webView.WebView.PointToNormalized(0, scrollDeltaY), webView.WebView.PointToNormalized((int)mousePos.x, (int)mousePos.y));
                        prevPos = this.transform.position;
                    }
                }
                */
            }
        } else if (Input.GetKeyDown(KeyCode.W)) // scroll up
        {
            if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit))
            {
                Vector2 mousePos = GetMousePosition(hit);
                webView.WebView.Scroll(webView.WebView.PointToNormalized(0, (int)scrollSensitivity * -1), webView.WebView.PointToNormalized((int)mousePos.x, (int)mousePos.y));
            }
        } else if (Input.GetKeyDown(KeyCode.R)) // resize
        {

            Debug.Log("Resize: " + webView.GetComponentInParent<Canvas>().name);
            (GetComponent<Canvas>().transform as RectTransform).sizeDelta = resizeDimensions / webView.Resolution; // divided by resolution
            webView.GetComponent<BoxCollider>().center = (resizeDimensions / webView.Resolution) * 0.5f;
            webView.GetComponent<BoxCollider>().size = resizeDimensions / webView.Resolution;
        }
    }

    private Vector2 GetMousePositionByRay(Vector3 hit)
    {
        Vector3 deltaV = hit - webView.transform.position; // vector from top left corner of browser to the point that the raycast hit the browser
        (webView.transform as RectTransform).GetWorldCorners(browserCorners);
        Vector3 xV = browserCorners[2] - browserCorners[1]; // vector from top left corner to top right corner
        Vector3 yV = browserCorners[1] - browserCorners[0]; // vector from top left corner to bottom left corner
        Vector2 mousePos = new(Vector3.Project(deltaV, xV).magnitude, Vector3.Project(deltaV, yV).magnitude);

        //// Angle corrections
        //mousePos.x /= (Mathf.Cos(Mathf.Deg2Rad * webView.transform.eulerAngles.y) * Mathf.Cos(Mathf.Deg2Rad * webView.transform.eulerAngles.z));
        //mousePos.y /= (Mathf.Cos(Mathf.Deg2Rad * webView.transform.eulerAngles.x) * Mathf.Cos(Mathf.Deg2Rad * webView.transform.eulerAngles.z));

        // Default corrections
        mousePos.y = -mousePos.y + (webView.transform as RectTransform).rect.height;
        mousePos *= webView.Resolution;

        return mousePos;
    }

    private Vector2 GetMousePosition(RaycastHit hit)
    {
        Vector3 deltaV = hit.point - webView.transform.position; // vector from top left corner of browser to the point that the raycast hit the browser
        (webView.transform as RectTransform).GetWorldCorners(browserCorners);
        Vector3 xV = browserCorners[2] - browserCorners[1]; // vector from top left corner to top right corner
        Vector3 yV = browserCorners[1] - browserCorners[0]; // vector from top left corner to bottom left corner
        Vector2 mousePos = new(Vector3.Project(deltaV, xV).magnitude, Vector3.Project(deltaV, yV).magnitude);

        // Angle corrections
        // mousePos.x /= (Mathf.Cos(Mathf.Deg2Rad * webView.transform.eulerAngles.y) * Mathf.Cos(Mathf.Deg2Rad * webView.transform.eulerAngles.z));
        // mousePos.y /= (Mathf.Cos(Mathf.Deg2Rad * webView.transform.eulerAngles.x) * Mathf.Cos(Mathf.Deg2Rad * webView.transform.eulerAngles.z));

        // Default corrections
        mousePos.y = -mousePos.y + (webView.transform as RectTransform).rect.height;
        mousePos *= webView.Resolution;

        return mousePos;
    }
}
