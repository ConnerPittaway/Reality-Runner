using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Include Facebook namespace
using Facebook.Unity;


public class FacebookManager : MonoBehaviour
{
    public static FacebookManager Instance;
    public Image testImage;

    // Awake function from Unity's MonoBehavior
    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActivateFacebook()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InternalInit);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InternalInit()
    {
        if (FB.IsInitialized)
        {
            Debug.Log("Successful Initialize the Facebook SDK");
            // Signal an app activation App Event
            FB.ActivateApp();
            FB.LogInWithReadPermissions(new List<string>() { "public_profile" }, AuthCallback);
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            ShareRun();
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    public bool ShareRun()
    {
        if (FB.IsLoggedIn)
        {
            FB.FeedShare(
                link: new Uri("https://connerpittaway.github.io/"),
                linkCaption: "test",
                linkDescription: "test"
            );

            return true;
        }
        else
        {
            return false;
        }
    }
}
