using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveData" + GameState.currentFile + ".matt";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelDataObject data = new LevelDataObject(GameState.successes, GameState.attempts, GameState.master_vol, GameState.music_vol, GameState.sfx_vol, GameState.music_vol);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void LoadData()
    {
        string path = Application.persistentDataPath + "/saveData" + GameState.currentFile + ".matt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            LevelDataObject data = formatter.Deserialize(stream) as LevelDataObject;
            stream.Close();

            GameState.successes = data.successes;
            GameState.attempts = data.attempts;

            GameState.master_vol = data.master_vol;
            GameState.music_vol = data.music_vol;
            GameState.sfx_vol = data.sfx_vol;
            GameState.ambient_vol = data.ambience_vol;
        }
        else
        {
            // default values
            GameState.successes = new int[20];
            GameState.attempts = new int[20];

            GameState.master_vol = 100;
            GameState.music_vol = 100;
            GameState.sfx_vol = 100;
            GameState.ambient_vol = 100;
        }
    }

    public static LevelDataObject GetData(int i)
    {
        string path = Application.persistentDataPath + "/saveData" + i + ".matt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            LevelDataObject data = formatter.Deserialize(stream) as LevelDataObject;
            stream.Close();

            return data;
        }

        return null;
    }

    public static void EraseData(int i)
    {
        string path = Application.persistentDataPath + "/saveData" + i + ".matt";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
