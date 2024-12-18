using System.IO;
using UnityEngine;

public static class SaveToFileSystem
{
    public static void WriteAlTo(string json, string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
    }

    public static string LoadDataFrom(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            Debug.Log("load data sucess :" + path);
            return File.ReadAllText(path);
        }
        else
        {
            Debug.LogWarning("File not found!");
            return null;
        }
    }
}
