using UnityEngine;
using UnityEngine.UI;

public class MirrorBrowser : MonoBehaviour
{
    [SerializeField] private Vuplex.WebView.Internal.CanvasViewportMaterialView webViewImage;
    private RawImage image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (webViewImage != null && image != null)
        {
            image.texture = webViewImage.Texture;
            image.material = webViewImage.Material;
            image.uvRect = new Rect(0f, 0f, 1f, 1f);
        }
    }
}
