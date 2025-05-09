using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreqSliderBehave : MonoBehaviour
{
    public Slider freq_slider;

    public MemorizeFrequency mod;

    void Update()
    {
        if (GameState.game_over) return;

        GetComponent<WaveBehavior>().freq = freq_slider.value;

        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            mod.Submit(freq_slider.value);
        }
    }
}
