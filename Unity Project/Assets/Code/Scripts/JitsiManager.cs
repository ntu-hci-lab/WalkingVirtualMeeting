using System.Collections;
using UnityEngine;
using Vuplex.WebView;
using UnityEngine.UI;

public class JitsiManager : MonoBehaviour
{
    [Tooltip("Point of reference for scrolling; should be hand position in VR")]
    [SerializeField] private GameObject scrollPose;
    [SerializeField] private GameObject zoomPose;
    [SerializeField] private RectTransform mainImage;
    [SerializeField] private RectTransform mainImage2;
    [SerializeField] private RectTransform mirrorImage;
    [SerializeField] private Transform thumbTip;
    [SerializeField] private Transform indexTip;
    [SerializeField] private Transform plane;
    [SerializeField] private float zoomSensitivity;
    [SerializeField] private float zoomMax;
    [SerializeField] private float scrollSensitivity;
    [SerializeField] private TMPro.TMP_Text size1;
    [SerializeField] private TMPro.TMP_Text size2;
    private CanvasWebViewPrefab webView;
    private bool loginLoaded = false;
    private RawImage image;
    private bool browserSelectionLoaded = false, meetingRoomLoaded = false;
    private float currentZoom = 1.0f;
    [SerializeField] private float oriScrollSensitivity;

    // scroll
    private bool isScrolling = false;
    private Vector3 prevScrollPos = Vector3.zero;
    private Vector2 previousAnchorPos;
    private float borderX;
    private float borderY;
    // zoom
    private bool isZooming = false;
    [SerializeField] private Vector2 originalResolution;
    private float previousDistance;
    private Vector2 previousResolution;
    private float previousDimension;
    private float lastDimension;



    // For testing purposes: uncomment to delete cache on each interation
    //private void Awake()
    //{
    //    Web.ClearAllData();
    //}

    private void Start()
    {
        webView = this.GetComponent<CanvasWebViewPrefab>();
        image = this.GetComponentInChildren<CanvasPointerInputDetector>().GetComponent<RawImage>();
        previousResolution = originalResolution;
        mainImage.sizeDelta = originalResolution;
        mirrorImage.sizeDelta = originalResolution;
        oriScrollSensitivity = scrollSensitivity;
        Login();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) ToggleMute();
        if (Input.GetKeyDown(KeyCode.P)) TogglePinParticipant();


        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    currentZoom += (zoomSpeed * Time.deltaTime);
        //    Zoom(currentZoom);
        //}

        //if (Input.GetKey(KeyCode.DownArrow) && currentZoom > 1.0f) // clamp zoom level to >= 1.0f
        //{
        //    currentZoom = Mathf.Max(1.0f, currentZoom - (zoomSpeed * Time.deltaTime));
        //    Zoom(currentZoom);
        //}

        if (Input.GetKey(KeyCode.Space)) // replace condition with raycast trigger
        {
            isScrolling = !isScrolling;
            prevScrollPos = indexTip.position;
        }
        if (isScrolling) Scroll();
        if (isZooming) Zoom();
    }
    private async void Login()
    {
        await webView.WaitUntilInitialized();
        await webView.WebView.WaitForNextPageLoadToFinish();
        StartCoroutine(nameof(SelectBrowser));
        loginLoaded = false;
        StartCoroutine(nameof(StartMeeting));
    }

    private IEnumerator SelectBrowser()
    {
        yield return new WaitUntil(() => webView.WebView.Url.Contains("jit.si"));
        yield return new WaitUntil(() => IsLoaded());

        string js = "document.getElementsByClassName(\"css-1l3cxcu-linkLabel\")[1].click();";

        webView.WebView.ExecuteJavaScript(js);
    }

    private IEnumerator StartMeeting()
    {
        yield return new WaitUntil(() => IsLoaded());

        string js = "document.getElementsByClassName(\"primary\")[0].click();" +
            "document.getElementsByClassName(\"chrome-extension-banner__close-container\")[0].click();";

        webView.WebView.ExecuteJavaScript(js);
    }

    private bool IsLoaded()
    {
        CheckIsLoaded();
        return loginLoaded;
    }

    private async void CheckIsLoaded()
    {
        var resultBrowser = await webView.WebView.ExecuteJavaScript("document.getElementsByClassName(\"css-1l3cxcu-linkLabel\")[1];");
        var resultStart = await webView.WebView.ExecuteJavaScript("document.getElementsByClassName(\"primary\")[0];");
        loginLoaded = (resultBrowser != "undefined" || resultStart != "undefined");
    }

    /// <summary>
    /// Toggles whether the user is muted or not
    /// </summary>
    public void ToggleMute()
    {
        string js = "document.getElementsByClassName(\"toolbox-button\")[0].click();";
        webView.WebView.ExecuteJavaScript(js);
    }

    /// <summary>
    /// When a screen is being shared, swaps the position of the screen and the speaker by pinning the speaker
    /// </summary>
    public void TogglePinParticipant()
    {
        string js = "document.getElementsByClassName(\"remote-videos\")[0].children[0].firstChild.click()";
        webView.WebView.ExecuteJavaScript(js);
    }

    /// <summary>
    /// Zooms the screen to the desired level (where 1.0 is the original zoom level)
    /// </summary>
    /// <param name="zoomLevel">Fraction (> 1.0f) of the original level to zoom to; setting this to less than 1.0f results in weird texture issues</param>
    private void Zoom()
    {
        float dimensions = Vector3.Distance(thumbTip.position, indexTip.position) / previousDistance * zoomSensitivity;
        
        //print(dimensions);
        //print(previousResol * dimensions);
        float sizeX = Mathf.Round(Mathf.Min(Mathf.Max(previousResolution.x * dimensions, originalResolution.x), originalResolution.x * zoomMax));
        float sizeY = Mathf.Round(Mathf.Min(Mathf.Max(previousResolution.y * dimensions, originalResolution.y), originalResolution.y * zoomMax));
        mirrorImage.sizeDelta = new Vector2(sizeX, sizeY);
        size1.text = sizeX.ToString();
        size2.text = sizeY.ToString();
        borderX = (sizeX - originalResolution.x) / 2;
        borderY = (sizeY - originalResolution.y) / 2;
        mirrorImage.anchoredPosition = new Vector2(Mathf.Max(Mathf.Min(mirrorImage.anchoredPosition.x, borderX), -borderX),
                                                   Mathf.Max(Mathf.Min(mirrorImage.anchoredPosition.y, borderY), -borderY));
        lastDimension = dimensions;
        //dimensions = Mathf.Max(Mathf.Min(previousDimension * dimensions, 100f), 0.01f);
//#if UNITY_ANDROID && !UNITY_EDITOR
//            var androidWebView = webView.WebView as AndroidWebView;
//            androidWebView.ZoomBy(dimensions);
//#endif
        //lastDimension = dimensions;
    }
    public void Zoom1(float dimensions)
    {
        //#if UNITY_ANDROID && !UNITY_EDITOR
        //            var androidWebView = webView.WebView as AndroidWebView;
        //            androidWebView.ZoomBy(dimensions);
        //#endif
        float sizeX = Mathf.Min(Mathf.Max(previousResolution.x * dimensions, originalResolution.x), originalResolution.x * zoomMax);
        float sizeY = Mathf.Min(Mathf.Max(previousResolution.y * dimensions, originalResolution.y), originalResolution.y * zoomMax);
        //webView.Resolution = Mathf.Min(Mathf.Max(previousResol * dimensions, 10f), 1300f);
        previousResolution = mainImage.sizeDelta = new Vector2(sizeX, sizeY);
        mirrorImage.sizeDelta = new Vector2(sizeX, sizeY);

        borderX = (sizeX - originalResolution.x) / 2;
        borderY = (sizeY - originalResolution.y) / 2;
        mirrorImage.anchoredPosition = new Vector2(Mathf.Max(Mathf.Min(mirrorImage.anchoredPosition.x, borderX), -borderX),
                                                   Mathf.Max(Mathf.Min(mirrorImage.anchoredPosition.y, borderY), -borderY));
    }

    /// <summary>
    /// Scrolls the screen based on the relative position to a reference object; visible only if zoom level > 1.0f
    /// </summary>
    public void Scroll()
    {
        float imageX = previousAnchorPos.x;
        float imageY = previousAnchorPos.y;

        Vector3 displacement = indexTip.position - prevScrollPos;
        float projectedX = Project2Vector(displacement, plane.right);
        float projectedY = Project2Vector(displacement, plane.up);

        imageX = Mathf.Max(Mathf.Min(imageX + ((projectedX) * scrollSensitivity), borderX), -borderX);
        imageY = Mathf.Max(Mathf.Min(imageY + ((projectedY) * scrollSensitivity), borderY), -borderY);

        mirrorImage.anchoredPosition = new Vector2(imageX, imageY);
    }
    private float Project2Vector(Vector3 vectorA, Vector3 vectorB)
    {
        float dotProduct = Vector3.Dot(vectorA, vectorB);

        // Calculate the length of vector B
        float lengthB = vectorB.magnitude;

        // Calculate the projection multiplier (C is how many times B)
        return dotProduct / (lengthB * lengthB);
    }

    // hand interaction
    public void startScroll()
    {
        borderX = (previousResolution.x - originalResolution.x) / 2;
        borderY = (previousResolution.y - originalResolution.y) / 2;
        prevScrollPos = indexTip.position;
        previousAnchorPos = mirrorImage.anchoredPosition;
        zoomPose.SetActive(false);
        isScrolling = true;
    }
    public void stopScroll()
    {
        isScrolling = false;
        zoomPose.SetActive(true);
    }
    public void startZoom()
    {
        previousDistance = Vector3.Distance(thumbTip.position, indexTip.position);
        //previousResolution = mirrorImage.sizeDelta;
        //previousResol = webView.Resolution;
        scrollPose.SetActive(false);
        isZooming = true;
    }
    public void stopZoom()
    {
        isZooming = false;
        scrollSensitivity = mirrorImage.sizeDelta.x / originalResolution.x * oriScrollSensitivity;
        previousResolution = mirrorImage.sizeDelta;
        
        scrollPose.SetActive(true);
    }
}
