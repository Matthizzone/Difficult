using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDataObject
{
    public int[] successes;
    public int[] attempts;

    public int master_vol;
    public int music_vol;
    public int sfx_vol;
    public int ambience_vol;

    public LevelDataObject(int[] new_successes, int[] new_attempts, int new_master_vol, int new_music_vol, int new_sfx_vol, int new_ambience_vol)
    {
        successes = new_successes;
        attempts = new_attempts;

        master_vol = new_master_vol;
        music_vol = new_music_vol;
        sfx_vol = new_sfx_vol;
        ambience_vol = new_ambience_vol;
    }
}
