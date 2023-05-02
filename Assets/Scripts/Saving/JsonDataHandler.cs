using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

//Uses JSON to save and load data between files
public class JsonDataHandler
{
    private string directoryPath;
    private string fileName;

    public JsonDataHandler(string directoryPath, string fileName)
    {
        this.directoryPath = directoryPath;
        this.fileName = fileName;
    }

    public T LoadData<T>()
    {
        string path = Path.Combine(directoryPath, fileName);
        T dataToLoad = default(T);
        {
            if (File.Exists(path))
            {
                try
                {
                    string dataString = "";
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataString = reader.ReadToEnd();
                        }
                    }
                    dataToLoad = JsonUtility.FromJson<T>(dataString);
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to load file in " + path + " " + e);
                }
            }
            //Return Default data
            return dataToLoad;
        }
    }

    public T LoadCloudData<T>()
    {
        string path = Path.Combine(directoryPath, fileName);
        T cloudDataToLoad = default(T);
        {
            if (FirebaseManager.Instance != null)
            {
                Debug.Log("Downloading From Firebase");
                FirebaseManager.Instance.LoadData(path, typeof(T).FullName);
            }
            else
            {
                Debug.Log("Firebase Load Error:" + typeof(T).FullName);
            }
        }
        return cloudDataToLoad;
    }

    public void SaveData<T>() where T : new()
    {
        string path = Path.Combine(directoryPath, fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            T data = new T();
            string dataString = JsonUtility.ToJson(data, true);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataString);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save file in " + path + " " + e);
        }

        if (FirebaseManager.Instance != null)
        {
            Debug.Log("Uploading to Firebase");
            FirebaseManager.Instance.UploadData(path, typeof(T).FullName);
        }
        else
        {
            Debug.Log("Firebase Is Null");
        }
    }
}
