using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorizeFrequency : MonoBehaviour
{
    public Transform RefFreq;
    public Transform PlayerFreq;
    public TMPro.TMP_Text answer_box;
    public TMPro.TMP_Text player_box;

    float right_ans = 0;

    private void Start()
    {
        GenerateFreq();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RefFreq.GetComponent<AudioSource>().Stop();
            RefFreq.GetComponent<LineRenderer>().enabled = false;

            PlayerFreq.GetComponent<AudioSource>().Play();
            PlayerFreq.GetComponent<LineRenderer>().enabled = true;
        }
    }

    void GenerateFreq()
    {
        right_ans = Random.Range(10f, 30f);
        RefFreq.GetComponent<WaveBehavior>().freq = right_ans;

        RefFreq.GetComponent<AudioSource>().Play();
        RefFreq.GetComponent<LineRenderer>().enabled = true;

        PlayerFreq.GetComponent<AudioSource>().Stop();
        PlayerFreq.GetComponent<LineRenderer>().enabled = false;

        answer_box.text = "?";
        player_box.text = "";
    }

    public void Submit(float freq)
    {
        float answer_freq = CalcActualFreq(right_ans);

        answer_box.text = "" + MattMath.GetDigit(answer_freq, 2)
                             + MattMath.GetDigit(answer_freq, 1)
                             + MattMath.GetDigit(answer_freq, 0);



        float player_freq = CalcActualFreq(freq);

        player_box.text = "" + MattMath.GetDigit(player_freq, 2)
                             + MattMath.GetDigit(player_freq, 1)
                             + MattMath.GetDigit(player_freq, 0);


        RefFreq.GetComponent<AudioSource>().Play();
        RefFreq.GetComponent<LineRenderer>().enabled = true;

        PlayerFreq.GetComponent<AudioSource>().Play();
        PlayerFreq.GetComponent<LineRenderer>().enabled = true;

        if (Mathf.Abs(answer_freq - player_freq) < answer_freq * 0.01f)
        {
            transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);

            if (!GameState.game_over) StartCoroutine(WaitThenNextRoutine());
        }
        else
        {
            transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
        }
    }

    float CalcActualFreq(float freq)
    {
        float freq3 = (freq - 20f) / 10f;

        float g4 = 392;

        return g4 * Mathf.Pow(2, freq3);
    }

    IEnumerator WaitThenNextRoutine()
    {
        yield return new WaitForSeconds(1.7f);

        GenerateFreq();
    }
}
