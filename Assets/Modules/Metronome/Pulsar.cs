using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pulsar : MonoBehaviour
{
    public bool auto = true;

    float bpm = 100;

    float alpha = 0;

    float prev_pulse = -1;

    private void Start()
    {
        if (auto)
        {
            bpm = Random.Range(80f, 120f);
            UpdateBPMText();

            StartCoroutine(PulseRoutine());
        }
    }

    void UpdateBPMText()
    {
        string bpm_str = "" +
            (bpm > 100 ? MattMath.GetDigit(bpm, 2) : "") +
            MattMath.GetDigit(bpm, 1) +
            MattMath.GetDigit(bpm, 0);

        transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = " = " + bpm_str;
    }

    void Update()
    {
        alpha = MattMath.FRIndepLerp(alpha, 0, 10);

        Image img = transform.GetChild(0).GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);

        HandleClick();
    }

    void HandleClick()
    {
        if (auto) return;
        if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space)) return;

        Pulse();

        bpm = 60 / (Time.time - prev_pulse);

        if (prev_pulse > 0)
        {
            CheckCalc();
            UpdateBPMText();
        }
        prev_pulse = Time.time;
    }

    void CheckCalc()
    {
        float target_bpm = transform.parent.Find("EnemyMet").GetComponent<Pulsar>().bpm;
        if (Mathf.Abs(bpm - target_bpm) < 5)
        {
            transform.parent.Find("Checks").GetComponent<Checks>().AddCheck();
        }
        else
        {
            transform.parent.Find("Checks").GetComponent<Checks>().AddX();
        }
    }

    IEnumerator PulseRoutine()
    {
        while (true)
        {
            Pulse();

            yield return new WaitForSeconds(60 / bpm);
        }
    }

    public void Pulse()
    {
        alpha = 1;

        if (auto)
        {
            AudioManager.instance.ResetValues();
            AudioManager.instance.SetPitch(0.749153538438f);
            AudioManager.instance.PlaySound("Blip", false);
        }
        else
        {
            AudioManager.instance.PlaySound("Blip", true);
        }
    }
}
