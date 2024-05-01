using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPanelManager : MonoBehaviour
{
    // TODO: Check anchor of Settings Panel vertical layout group

    /// <summary>
    /// The "div" that contains all settings
    /// </summary>
    [SerializeField] private GameObject savedSettings;
    [SerializeField] private GameObject settingPrefab;
    [SerializeField] private SettingModeSwitch SettingModeSwitch;
    [SerializeField] private CloudSaveManager cloudSaveManager;
    [SerializeField] private WindowSettingManager windowSettingManager;
    [SerializeField] private ItemSelector itemSelector;
    [SerializeField] private Button applyButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button favoriteButton;
    [SerializeField] private Button uploadButton;
    [SerializeField] private Toggle HeadAnchor;
    [SerializeField] private Toggle PathAnchor;
    [SerializeField] private Toggle presentationButton;
    [SerializeField] private Toggle participantButton;
    //[SerializeField] private Toggle pitchButton;
    //[SerializeField] private Toggle yawButton;
    //[SerializeField] private ManagerRotation managerRotation;
    //[SerializeField] private string favoriteText;

    private int currentSelected = -1;
    public int CurrentSelected
    {
        set
        {
            if (currentSelected != -1) savedSettings.transform.GetChild(currentSelected).GetComponent<Toggle>().isOn = false; // Turn off initial toggle
            currentSelected = value; // Set new currentSelected

            if (currentSelected == -1)
            {
                applyButton.interactable = deleteButton.interactable = false;
            }
            else
            {
                applyButton.interactable = deleteButton.interactable  = true;
            }
        }
    }

    private int currentFavorite = -1;

    public void AddSavedSetting()
    {
        // get current time
        string str = (System.DateTime.Now.Hour < 10 ? "0" : "") + System.DateTime.Now.Hour + ":" + (System.DateTime.Now.Minute < 10 ? "0" : "") + System.DateTime.Now.Minute + ":" + (System.DateTime.Now.Second < 10 ? "0" : "") + System.DateTime.Now.Second;
        AppendSetting(str);
        cloudSaveManager.saveData(str);
    }
    public void AppendSetting(string name)
    {
        //Debug.Log("append");
        GameObject newSetting = Instantiate(settingPrefab, savedSettings.transform);
        newSetting.transform.SetAsLastSibling();
        newSetting.transform.localPosition = Vector3.zero;
        newSetting.transform.localEulerAngles = Vector3.zero;
        newSetting.transform.localScale = Vector3.one;
        newSetting.GetComponentInChildren<TMP_Text>().text = name;
    }
    public void applySetting()
    {
        // get selected title
        string str = savedSettings.transform.GetChild(currentSelected).GetComponentInChildren<TMP_Text>().text;
        // find and apply window settings
        windowSettingManager.applyWindow(cloudSaveManager.findData(true, str), cloudSaveManager.findData(false, str));
    }
    // itemSelector: apply white border
    public void applyObject()
    {
        if (presentationButton.isOn)
        {
            itemSelector.UnselectObject();
            itemSelector.isSlide = true;
            itemSelector.SelectObject();
        }
        else if (participantButton.isOn)
        {
            itemSelector.UnselectObject();
            itemSelector.isSlide = false;
            itemSelector.SelectObject();
        }
    }
    public void updateData()
    {
        string str = savedSettings.transform.GetChild(currentSelected).GetComponentInChildren<TMP_Text>().text;
        cloudSaveManager.updateData(str);
    }
    public void DeleteSavedSetting()
    {
        if (currentFavorite == currentSelected)
        {
            currentFavorite = -1;
        }
        savedSettings.transform.GetChild(currentSelected).gameObject.SetActive(false); // setting cannot be deleted due to loss of index for referral
        CurrentSelected = -1;
    }
    public void UploadSetting()
    {
        cloudSaveManager.uploadData();
    }

    public void ResetWindow()
    {
        windowSettingManager.resetWindow();
    }

    public void StartNewTrial()
    {
        GameObject divider = new("TrialDivider", typeof(TMP_Text));
        divider.transform.SetParent(savedSettings.transform);
        divider.transform.SetAsLastSibling();
        divider.transform.localPosition = Vector3.zero;
        divider.transform.localEulerAngles = Vector3.zero;
        divider.transform.localScale = Vector3.one;
        divider.AddComponent<RectTransform>();
        divider.AddComponent<CanvasRenderer>();
        divider.AddComponent<TextMeshProUGUI>();
        divider.GetComponent<TMP_Text>().text = "--------";
        divider.GetComponent<TMP_Text>().fontSize = 8;
    }
    public void switchWindowBehavior()
    {
        if (HeadAnchor.isOn)
        {
            SettingModeSwitch.windowMode = true;
            SettingModeSwitch.applyWindowBehavior();
        }
        else if (PathAnchor.isOn)
        {
            SettingModeSwitch.windowMode = false;
            SettingModeSwitch.applyWindowBehavior();
        }
    }
}
