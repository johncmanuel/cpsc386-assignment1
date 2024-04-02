using UnityEngine;

public static class Prefs
{
    // Helper functions to save and load data for the settings
    public static void SaveData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static void SaveData(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public static void SaveData(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public static int LoadDataInt(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        throw new System.Exception("Key not found: " + key);
    }

    public static float LoadDataFloat(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        throw new System.Exception("Key not found: " + key);
    }

    public static string LoadDataString(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        throw new System.Exception("Key not found: " + key);
    }
}
