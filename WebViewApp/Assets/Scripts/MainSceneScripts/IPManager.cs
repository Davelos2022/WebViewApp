using System;
using System.Collections;
using UnityEngine;
using HtmlAgilityPack;
using UnityEngine.Networking;

public class IPManager : MonoBehaviour
{
    
    string url = "http://checkip.dyndns.org";
    const string countryHtmlText = "/countries/";

    public void Start()
    {
        
    }
    
    IEnumerator GetRequest(string url, Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            callback(webRequest.downloadHandler.text);
        }
    }

    private void GetIP(Action<string> callbackIP)
    {
        StartCoroutine(GetRequest(url, (callback) =>
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(callback);
            Debug.Log(callback);
            callback = callback.Substring(callback.IndexOf(":") + 1);
            callback = callback.Substring(0, callback.IndexOf("<"));
            url = "http://ipinfo.io/" + callback;
            callbackIP(callback);
        }));
    }

    public void GetCountry(Action<string> callbackCountry)
    {
        GetIP((callbackText) =>
        {
            url = "http://ipinfo.io/" + callbackText;

            StartCoroutine(GetRequest(url, (callback) =>
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(callback);
                callback = callback.Substring(callback.IndexOf(countryHtmlText) + countryHtmlText.Length, 2);
                Debug.Log(callback);
                callbackCountry(callback);
            }));
        });
    }


}