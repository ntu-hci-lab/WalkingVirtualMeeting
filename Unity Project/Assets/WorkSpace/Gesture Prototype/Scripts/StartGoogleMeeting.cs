using System.Collections;
using UnityEngine;
using Vuplex.WebView;

public class StartGoogleMeeting : MonoBehaviour
{
    private CanvasWebViewPrefab webView;
    private bool loginLoaded = false;

    // For testing purposes: uncomment to delete cache on each interation
    //private void Awake()
    //{
    //    Web.ClearAllData();
    //}

    private void Start()
    {
        webView = this.GetComponent<CanvasWebViewPrefab>();
        Login();
    }

    private async void Login()
    {
        await webView.WaitUntilInitialized();
        await webView.WebView.WaitForNextPageLoadToFinish();
        StartCoroutine(nameof(SelectBrowser));
        //loginLoaded = false;
        //StartCoroutine(nameof(StartMeeting));
    }

    private IEnumerator SelectBrowser()
    {
        //yield return new WaitUntil(() => webView.WebView.Url.Contains("jit.si"));
        //yield return new WaitUntil(() => IsLoaded());
        yield return new WaitForSeconds(5f);
        string js = "document.getElementsByClassName(\"VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc LQeN7 jEvJdc QJgqC\")[0].click()";

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
        var resultBrowser = await webView.WebView.ExecuteJavaScript("document.getElementsByClassName(\"VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc LQeN7 jEvJdc QJgqC\")[0]");
        //var resultStart = await webView.WebView.ExecuteJavaScript("document.getElementsByClassName(\"primary\")[0];");
        loginLoaded = (resultBrowser != "undefined");
    }
}
