using UnityEngine;

public static class SaveToPlayerPrefsSystem
{
    public static void WriteAlTo(string json, string keyName)
    {
        PlayerPrefs.SetString(keyName, json);
    }

    public static string LoadDataFrom(string keyName)
    {
        if (PlayerPrefs.HasKey(keyName))
        {
            Debug.Log("load data sucess");
            return PlayerPrefs.GetString(keyName);
        }
        else
        {
            Debug.Log("khong ton tai" + keyName);
            return null;
        }
    }
}
