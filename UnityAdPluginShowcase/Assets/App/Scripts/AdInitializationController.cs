using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdInitializationController : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private Button bannerAdSceneButton;

    [SerializeField] private Button interstatialAdSceneButton;

    [SerializeField] private Button rewardedAdSceneButton;

    [SerializeField] string _androidGameId;

    [SerializeField] string _iOSGameId;
    
    [SerializeField] bool _testMode = true;

    private string _gameId;

    private void Awake()
    {
        InitializeAds();
    }


    private void Start()
    {
        bannerAdSceneButton.onClick.AddListener(onBannerSceneButtonClicked);
        interstatialAdSceneButton.onClick.AddListener(onInterstatialSceneButtonClicked);
        rewardedAdSceneButton.onClick.AddListener(onRewardedSceneButtonPressed);

        EnableAllButtons();
    }

    public void InitializeAds()
    {
#if UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_IOS
        _gameId = _iOSGameId;
#endif
        Advertisement.Initialize(_gameId, _testMode, this);
    }


    #region Ad Initialization Callbacks

    //Cant give any method with parameter in this method.If we give method will throw error(get_isPlaying can only be called from the main thread).
    public void OnInitializationComplete()
    {
        Debug.Log("Advertisement Initialized");

        EnableAllButtons();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Button Methods

    private void onBannerSceneButtonClicked()
    {
        SceneManager.LoadScene("BannerAdScene");
    }

    private void onInterstatialSceneButtonClicked()
    {
        SceneManager.LoadScene("InterstatialScene");
    }

    private void onRewardedSceneButtonPressed()
    {
        SceneManager.LoadScene("RewardedAdScene");
    }

   
    public void EnableAllButtons()
    {
        bannerAdSceneButton.interactable = true;

        interstatialAdSceneButton.interactable = true;

        rewardedAdSceneButton.interactable = true;
    }

    #endregion
}
