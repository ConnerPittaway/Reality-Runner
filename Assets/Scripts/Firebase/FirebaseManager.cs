using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using System.Threading.Tasks;
using System.Threading;
using System;

public class FirebaseManager : MonoBehaviour
{
    //Instance
    public static FirebaseManager Instance;

    //Ready Flag
    public bool fireBaseReady = false;

    //Firebase App
    private FirebaseApp app;

    //Firebase Authenticator (log in/out)
    public FirebaseAuth authenticator;

    //Firebase Realtime Database (High Scores)
    public FirebaseDatabase database;
    public DatabaseReference DBreference;

    //Firebase Cloud Storage (Save Files)
    public FirebaseStorage storage;
    public StorageReference storageRef;

    //Leaderboard
    public List<User> scoreLeaderboard;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Firebase Started");
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

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = Firebase.FirebaseApp.DefaultInstance;

                //Intialise Firebase Modules
                InitialiseAuthenticator();
                Debug.Log("Firebase Auth Done");

                InitialiseDatabase();
                Debug.Log("Firebase DB Done");

                InitialiseCloudStorage();
                Debug.Log("Firebase Cloud Done");

                //Firebase Flag
                fireBaseReady = true;

            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));

                //Failed -> set instance to null for checks
                Instance = null;
            }
        });
    }

    private void InitialiseAuthenticator()
    {
        authenticator = FirebaseAuth.DefaultInstance;
    }

    private void InitialiseDatabase()
    {
        database = FirebaseDatabase.DefaultInstance;
        DBreference = database.RootReference;
    }

    private void InitialiseCloudStorage()
    {
        storage = FirebaseStorage.DefaultInstance;
        storageRef = storage.GetReferenceFromUrl("gs://reality-runner.appspot.com/");
    }

    public void UploadData(string localFileLocation, string dataType)
    {
        Debug.Log(dataType);

        StorageReference dataRef = default;
        // Create a reference to the file you want to upload
        if (dataType == "GameData")
        {
           dataRef = storageRef.Child("gameData");
        }
        else if (dataType == "StatsData")
        {
            dataRef = storageRef.Child("statsData");
        }
        else
        {
            Debug.Log("Not Saving Settings To Cloud");
            return;
        }

        StorageReference playerDataRef = dataRef.Child(SystemInfo.deviceUniqueIdentifier);

        string localfileuri = string.Format("{0}://{1}", Uri.UriSchemeFile, localFileLocation);

        // Upload the file to the path "images/rivers.jpg"
        playerDataRef.PutFileAsync(localfileuri)
            .ContinueWith((Task<StorageMetadata> task) => {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
            // Uh-oh, an error occurred!
        }
                else
                {
            // Metadata contains file metadata such as size, content-type, and download URL.
            StorageMetadata metadata = task.Result;
                    string md5Hash = metadata.Md5Hash;
                    Debug.Log("Finished uploading...");
                    Debug.Log("md5 hash = " + md5Hash);
                }
            });
    }

    public void LoadData(string localFileLocation, string dataType)
    {
        BeginLoadData(localFileLocation, dataType);
    }

    public void BeginLoadData(string localFileLocation, string dataType)
    {
        Debug.Log(dataType);

        StorageReference dataRef = default;
        // Create a reference to the file you want to upload
        if (dataType == "GameData")
        {
            dataRef = storageRef.Child("gameData");
        }
        else if (dataType == "StatsData")
        {
            dataRef = storageRef.Child("statsData");
        }
        else
        {
            Debug.Log("Not correct Type");
        }

        StorageReference playerDataRef = dataRef.Child(SystemInfo.deviceUniqueIdentifier);

        string localfileuri = string.Format("{0}://{1}", Uri.UriSchemeFile, localFileLocation);

        // Start downloading a file
        Task task = playerDataRef.GetFileAsync(localfileuri,
            new StorageProgress<DownloadState>(state =>
            {
                // called periodically during the download
                Debug.Log(string.Format(
                            "Progress: {0} of {1} bytes transferred.",
                            state.BytesTransferred,
                            state.TotalByteCount
                        ));
            }), CancellationToken.None);

        task.ContinueWithOnMainThread(resultTask =>
        {
            //When Completed
            if (!resultTask.IsFaulted && !resultTask.IsCanceled)
            {
                if (dataType == "GameData")
                {
                    GlobalDataManager.Instance.UpdateData();
                }
                else if (dataType == "StatsData")
                {
                    GlobalStatsData.Instance.UpdateData();
                }
            }
        });
    }

    public void UploadHighScore()
    {
        User user = new User(SystemInfo.deviceUniqueIdentifier, GlobalStatsData.Instance.usernameLeaderboard, GlobalDataManager.Instance.GetHighScore());
        string json = JsonUtility.ToJson(user);
        DBreference.Child("userScores").Child(SystemInfo.deviceUniqueIdentifier).SetRawJsonValueAsync(json);

        //Upload Test Users
        /*for(int i = 0; i < 10; i++)
        {
            User user = new User(string.Format("User{0}", i*10), string.Format("User{0} Name", i*10), i*10000);
            string json = JsonUtility.ToJson(user);
            DBreference.Child("userScores").Child(user.uid).SetRawJsonValueAsync(json);
        }*/
    }

    public void UpdateUserName()
    {
        User user = new User(SystemInfo.deviceUniqueIdentifier, GlobalStatsData.Instance.usernameLeaderboard, GlobalDataManager.Instance.GetHighScore());
        string json = JsonUtility.ToJson(user);
        DBreference.Child("userScores").Child(SystemInfo.deviceUniqueIdentifier).SetRawJsonValueAsync(json);
    }

    public void GetHighScores()
    {
        scoreLeaderboard = new List<User>();
        FirebaseDatabase.DefaultInstance
      .GetReference("userScores").OrderByChild("score").LimitToLast(10)
      .GetValueAsync().ContinueWithOnMainThread(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {

              DataSnapshot snapshot = task.Result;
              foreach (DataSnapshot childData in snapshot.Children)
              {
                  User userScore = new User(childData.Child("uid").Value.ToString(), childData.Child("name").Value.ToString(), int.Parse(childData.Child("score").Value.ToString()));
                  scoreLeaderboard.Add(userScore);
              }
              Debug.Log(scoreLeaderboard.Count);
              //Reverse order
              scoreLeaderboard.Reverse();
          }
      });
    }
}
