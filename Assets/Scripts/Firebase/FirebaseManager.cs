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
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

                //Intialise Firebase Modules
                InitialiseAuthenticator();
                Debug.Log("Firebase Auth Done");

                //InitialiseDatabase();
                Debug.Log("Firebase DB Done");

                InitialiseCloudStorage();
                Debug.Log("Firebase Cloud Done");

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                fireBaseReady = true;   

                //Push Event To Compare Data To Load
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));

                // Firebase Unity SDK is not safe to use here.
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

        // Upload the file to the path "images/rivers.jpg"
        playerDataRef.PutFileAsync(localFileLocation)
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
        StartCoroutine(BeginLoadData(localFileLocation, dataType));
    }

    public IEnumerator BeginLoadData(string localFileLocation, string dataType)
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

        // Start downloading a file
        Task task = playerDataRef.GetFileAsync(localFileLocation,
            new StorageProgress<DownloadState>(state => {
                // called periodically during the download
                Debug.Log(string.Format(
                            "Progress: {0} of {1} bytes transferred.",
                            state.BytesTransferred,
                            state.TotalByteCount
                        ));
            }), CancellationToken.None);

        yield return new WaitUntil(predicate: () => task.IsCompleted);

        //When Completed
        if(!task.IsFaulted && !task.IsCanceled)
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
    }
}
