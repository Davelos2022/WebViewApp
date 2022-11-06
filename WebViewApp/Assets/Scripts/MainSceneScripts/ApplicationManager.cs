using UnityEngine;
using OneSignalSDK;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private MyFirebase myFirebase;
    [SerializeField] private IPManager IPManager;

    private OneSignal oneSignal;
    private string path;

    private float waitTime = 6f;

    //private const string noCountry = "us";

    private void Start()
    {

        //IPManager.GetCountry((callbackText) =>
        //{
        //    string country;
        //    country = callbackText;
        //    if (country == noCountry) OpenGame();
        //    else OpenWevView();
        //});

        Ones();
        StartCoroutine(TitleApp());
    }


    IEnumerator TitleApp()
    {
        yield return new WaitForSeconds(waitTime);

        if (PlayerPrefs.HasKey("path"))
        {
            path = PlayerPrefs.GetString("path");
            OpenWevView();
        }
        else
        {
            LoadInfo();
        }
    }


    private void LoadInfo()
    {
        string getUrl = myFirebase.Link;
        bool emulator = true;

        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaClass cls = new AndroidJavaClass("com.nekolaboratory.EmulatorDetector");
            AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("getApplicationContext");

            emulator = cls.CallStatic<bool>("isEmulator", context);
        }

        if (string.IsNullOrEmpty(getUrl) || emulator|| Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            OpenGame();
            return;
        }
        else
        {
            path = getUrl;
            OpenWevView();
        }
    }


    private async void Ones()
    {
        OneSignal.Default.Initialize("feed28d9-7d5e-4633-a670-0a11203c1908");

        try
        {
            await oneSignal.PromptForPushNotificationsWithUserResponse();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void OpenWevView()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OpenGame();
            return;
        }
        else
        {
            PlayerPrefs.SetString("path", path);
            SceneManager.LoadScene("WebView");
        }
    }

    private void OpenGame()
    {
        SceneManager.LoadScene("Game");
    }
}
