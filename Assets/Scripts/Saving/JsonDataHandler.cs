using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

//Uses JSON 
public class JsonDataHandler
{
    private string directoryPath;
    private string fileName;

    public JsonDataHandler(string directoryPath, string fileName)
    {
        this.directoryPath = directoryPath;
        this.fileName = fileName;
    }

    public void SaveData()
    {
        string path = Path.Combine(directoryPath, fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            GameData data = new GameData();
            string dataString = JsonUtility.ToJson(data, true);
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataString);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save file in " + path + " " + e);
        }
    }

    public GameData LoadData()
    {
        string path = Path.Combine(directoryPath, fileName);
        GameData dataToLoad = null;
        {
            if(File.Exists(path))
            {
                try
                {
                    string dataString = "";
                    using(FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataString = reader.ReadToEnd();
                        }
                    }

                    dataToLoad = JsonUtility.FromJson<GameData>(dataString);
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to load file in " + path + " " + e);
                }
            }
            return dataToLoad;
        }
    }
}
