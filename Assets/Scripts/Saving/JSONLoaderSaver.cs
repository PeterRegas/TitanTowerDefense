using UnityEngine;
using System.IO;

public class JSONLoaderSaver
{
    public static void SaveAsJSON(string savePath, string filename, SaveState playerStats)
    {

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        string json = JsonUtility.ToJson(playerStats);
        File.WriteAllText(savePath + filename, json);
    }

    public static SaveState LoadFromJSON(string savePath, string filename)
    {
        if (File.Exists(savePath + filename))
        {
            Debug.Log("Trying to log at: " + savePath + filename);
            string json = File.ReadAllText(savePath + filename);
            Debug.Log("JSON" + json);
            SaveState gameStats = JsonUtility.FromJson<SaveState>(json);

            /*
            Debug.Log("Health:" + playerStats.health);
            Debug.Log("Score:" + playerStats.score);
            Debug.Log("Level:" + playerStats.level);
            Debug.Log("XP:" + playerStats.experience);
            */

            return gameStats;
        }
        Debug.Log("Unable to load from file: " + savePath + filename);
        return null;
    }
}