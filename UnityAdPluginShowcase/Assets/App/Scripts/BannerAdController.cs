using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class BannerAdController : MonoBehaviour
{
    #region Fields
    //For loading banner from Unity Ads API
    [SerializeField] private Button loadBannerButton;

    [SerializeField] private Button showBannerButton;

    [SerializeField] private Button hideBannerButton;

    [Space]
    [Header("For showing info about states of ad")]
    [SerializeField] private Text infoText;
    [SerializeField] private float infoTextFadeSpeed = 0.01f;

    [Space]
    [Header("The position of banner over screen")]
    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    [Space]
    [Header("ID's must be replaced banner ID's in monetization dashboard")]
    [SerializeField] private string bannerAndroidId = "Banner_Android";

    [SerializeField] private string bannerIOSId = "Banner_iOS";

    [Space]
    private string currentPlatformBannerId = null;

    #endregion

    #region Monobehaviour Callbacks
    private void Start()
    {
        //First we need to set show and hide buttons non-interactable to avoid bugs and errors
        showBannerButton.interactable = false;
        hideBannerButton.interactable = false;

#if UNITY_ANDROID
        currentPlatformBannerId = bannerAndroidId;
#elif UNITY_IOS
        currentPlatformBannerId = bannerIOSId;
#endif
        //Setting the position of banner
        Advertisement.Banner.SetPosition(bannerPosition);

        loadBannerButton.onClick.AddListener(OnLoadBannerButtonClicked);

        //Checking if there is internet connection
        CheckInternetConnection();

    }

    private void OnDisable()
    {
        loadBannerButton.onClick.RemoveAllListeners();
        showBannerButton.onClick.RemoveAllListeners();
        hideBannerButton.onClick.RemoveAllListeners();
    }

    #endregion

    #region Button Methods
    //This method will be added to Load Banner Button.
    private void OnLoadBannerButtonClicked()
    {
        if (CheckInternetConnection())
        {
            //That is the load and error handling options of banner
            BannerLoadOptions bannerLoadOptions = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnErrorOccured
            };

            Advertisement.Banner.Load(currentPlatformBannerId, bannerLoadOptions);
        }
        
        
        
    }

    private void OnShowBannerButtonClicked()
    {
        if (!CheckInternetConnection())
        {
            return;
        }
        BannerOptions showBannerOptions = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHideClicked,
            showCallback = OnBannerShowClicked
        };

        Advertisement.Banner.Show(currentPlatformBannerId, showBannerOptions);
    }

    //This method is used for hiding banner
    private void OnHideBannerButtonClicked()
    {
        if (!CheckInternetConnection())
        {
            return;
        }
        Advertisement.Banner.Hide();
    }

    #endregion

    #region Banner Load Callbacks
    private void OnBannerLoaded()
    {
        StartCoroutine(ShowInfoText("Banner Loaded"));

        showBannerButton.interactable = true;

        hideBannerButton.interactable = true;

        showBannerButton.onClick.AddListener(OnShowBannerButtonClicked);

        hideBannerButton.onClick.AddListener(OnHideBannerButtonClicked);
    }

   

    private void OnErrorOccured(string errorMessage)
    {
        if (!CheckInternetConnection())
        {
            loadBannerButton.interactable = false;
            return;
        }
        else
        {
            StartCoroutine(ShowInfoText(errorMessage));
        }

        OnLoadBannerButtonClicked();
    }

    #endregion

    #region Banner Show Callbacks

    //This method is used when the banner is showing and we click on banner
    private void OnBannerClicked()
    {
        StartCoroutine(ShowInfoText("Player Clicked the Banner"));
    }

    //This method is used when the banner is showing and we click hide banner button
    private void OnBannerHideClicked()
    {
        StartCoroutine(ShowInfoText("Player Clicked Hide Banner"));
        
    }

    //This method is used when the banner is showing and we click show banner button
    private void OnBannerShowClicked()
    {
        StartCoroutine(ShowInfoText("Player Clicked Show Banner"));
        
    }

    #endregion

    #region Debugging Methods
    //This method check the internet connection for a single time and take actions according to result.
    private bool CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {

            StartCoroutine(ShowInfoText("Internet Disconnected"));

            StartCoroutine(CheckNetworkRepeatedly());

            return false;
        }
        else
        {
            return true;
        }
    }

    //This coroutine works until network connection established
    IEnumerator CheckNetworkRepeatedly()
    {
        loadBannerButton.interactable = false;

        showBannerButton.interactable = false;

        hideBannerButton.interactable = false;

        while (Application.internetReachability == NetworkReachability.NotReachable)
        {
            yield return null;
        }

        loadBannerButton.interactable = true;

        StartCoroutine(ShowInfoText("Internet Connection Established"));
    }

    IEnumerator ShowInfoText(string infoString)
    {
        infoText.text = infoString;

        Color infoTextFadeColor = new Color(infoText.color.r, infoText.color.g, infoText.color.b, 1);

        for(float i = 1; i >= 0; i -= Time.deltaTime)
        {
            infoTextFadeColor.a = i;
            infoText.color = infoTextFadeColor;
            yield return new WaitForSeconds(infoTextFadeSpeed);
        }
    }

    #endregion
}
