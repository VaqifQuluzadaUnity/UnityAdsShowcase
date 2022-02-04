using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdInitializationUIController : MonoBehaviour
{
    public static AdInitializationUIController instance;

    [SerializeField] private Button bannerAdSceneButton;

    [SerializeField] private Button interstatialAdSceneButton;

    [SerializeField] private Button rewardedAdSceneButton;

    private void Awake()
    {
        if(instance!=null && instance!= this)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
    }

    private void Start()
    {

        bannerAdSceneButton.onClick.AddListener(onBannerSceneButtonClicked);
        interstatialAdSceneButton.onClick.AddListener(onInterstatialSceneButtonClicked);
        rewardedAdSceneButton.onClick.AddListener(onRewardedSceneButtonPressed);

    }

    private void onBannerSceneButtonClicked()
    {
        SceneManager.LoadScene("BannerAdScene");
    }

    private void onInterstatialSceneButtonClicked()
    {
        SceneManager.LoadScene("InterstatialAdScene");
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

}
