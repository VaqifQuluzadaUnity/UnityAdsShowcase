using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdInitializationController : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;

    [SerializeField] string _iOSGameId;
    
    [SerializeField] bool _testMode = true;

    private string _gameId;

    void Awake()
    {
        InitializeAds();
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




    public void OnInitializationComplete()
    {
        AdInitializationUIController.instance.EnableAllButtons();
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
