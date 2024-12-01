using System.Threading.Tasks;
using UnityEngine;

public static class ConvertDataSystem
{
    public static void SaveToFile<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data);
        SaveToFileSystem.WriteAlTo(json, fileName);
    }

    public static void SaveToPlayerPrefs<T>(T data, string keyName)
    {
        string json = JsonUtility.ToJson(data);
        SaveToPlayerPrefsSystem.WriteAlTo(json, keyName);
    }

    public static async Task SaveToRemoveAsync<T>(T data, string serverUrl)
    {
        string json = JsonUtility.ToJson(data);
        await SaveToRemoteSystem.WriteAlTo(json, serverUrl);
    }

    public static T LoadDataFromFile<T>(string fileName)
    {
        string json = SaveToFileSystem.LoadDataFrom(fileName);
        return JsonUtility.FromJson<T>(json);
    }

    public static T LoadDataFromPlayerPrefs<T>(string keyName)
    {
        string json = SaveToPlayerPrefsSystem.LoadDataFrom(keyName);
        return JsonUtility.FromJson<T>(json);
    }

    public static async Task<T> LoadDataFromRemote<T>(string serverUrl)
    {
        string json = await SaveToRemoteSystem.LoadDataFrom(serverUrl);
        return JsonUtility.FromJson<T>(json);
    }
}
