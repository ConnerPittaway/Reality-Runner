using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Include Facebook namespace
using Facebook.Unity;


public class FacebookManager : MonoBehaviour
{
    public Image testImage;

    // Awake function from Unity's MonoBehavior
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
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
            
            /*FB.ShareLink(
                //contentURL: new Uri("https://play.google.com/store/apps/details?id=com.halfbrick.jetpackjoyride&hl=en_GB"),
                contentTitle: "test",
                contentDescription: "test"
            );*/

            FB.FeedShare(
                link: new Uri("https://play.google.com/store/apps/details?id=com.halfbrick.jetpackjoyride&hl=en_GB"),
                linkCaption: "test",
                linkDescription: "test"
            );
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    void LoginCallback(IResult result)
    {
        if (FB.IsLoggedIn)
        {
            testImage.enabled = true;
            //OnLoggedIn();
        }
    }
}
