using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderBehavior : MonoBehaviour
{
    public static LoaderBehavior instance;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    public void FadeToScene(int build_index, float in_length, float wait_length, float out_length)
    {
        if (GameState.currently_transitioning) return;

        // stop current music playing
        ClearMusic(in_length);

        // actually make transition
        TransitionManager.instance.loadEvent.AddListener(delegate { SceneManager.LoadScene(build_index); });
        TransitionManager.instance.StartFade(in_length, wait_length, out_length, Color.white);
    }

    public void SlideToScene(int build_index, float in_length, float wait_length, float out_length)
    {
        if (GameState.currently_transitioning) return;

        // stop current music playing
        ClearMusic(in_length);

        // actually make transition
        TransitionManager.instance.loadEvent.AddListener(delegate { SceneManager.LoadScene(build_index); });
        TransitionManager.instance.StartSlide(in_length, wait_length, out_length, Color.black, SlidingDoorTransition.SlideType.Left, SlidingDoorTransition.SlideType.Left);
    }

    public void CutoutToScene(int build_index, float in_length, float wait_length, float out_length)
    {
        if (GameState.currently_transitioning) return;

        // stop current music playing
        ClearMusic(in_length);

        // actually make transition
        TransitionManager.instance.loadEvent.AddListener(delegate { SceneManager.LoadScene(build_index); });
        TransitionManager.instance.StartCutout(in_length, wait_length, out_length, Color.black);

    }

    public void LoadMultiplayerHost(string name, bool friends_only)
    {
        if (GameState.currently_transitioning) return;

        // stop current music playing
        ClearMusic(1);

        // actually make transition
        TransitionManager.instance.loadEvent.AddListener(delegate { SceneManager.LoadScene(2); });
        //TransitionManager.instance.loadEvent.AddListener(delegate { LobbyManager.instance.CreateLobby(name, friends_only); });
        TransitionManager.instance.StartCutout(1, 2, 1, Color.black);
    }

    public void LoadMultiplayerJoin(uint lobby_id, ulong targetSteamId)
    {
        if (GameState.currently_transitioning) return;

        // stop current music playing
        ClearMusic(1);

        // actually make transition
        TransitionManager.instance.loadEvent.AddListener(delegate { SceneManager.LoadScene(2); });
        //TransitionManager.instance.loadEvent.AddListener(delegate { LobbyManager.instance.JoinLobby(lobby_id, targetSteamId); });
        TransitionManager.instance.StartCutout(1, 2, 1, Color.black);
    }






    void ClearMusic(float fadeout)
    {
        AudioManager.instance.FadeToVol("Music/Anxiety", 0, fadeout);
        AudioManager.instance.FadeToVol("Music/DarkCrimes", 0, fadeout);
        AudioManager.instance.FadeToVol("Music/Phobia", 0, fadeout);
    }
}
