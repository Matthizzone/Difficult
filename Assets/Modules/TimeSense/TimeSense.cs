using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSense : MonoBehaviour
{
    bool need_reset = false;
    bool started = false;
    float start_time = 0;
    float target_time = 0;

    public TMPro.TMP_Text text;
    public Image Banner;

    private void Start()
    {
        ResetTimeNeeded();
    }

    void ResetTimeNeeded()
    {
        target_time = Random.Range(18f, 35f);
        string target_time_str = "" + (int)target_time + "." +
            MattMath.GetDigit(target_time, -1);

        transform.Find("Canvas").Find("Instructions").GetComponent<TMPro.TMP_Text>().text =
            "Click in exactly " + target_time_str + " seconds.";

        text.text = "Click to begin";
        Banner.color = ColorPalette.instance.blue;
    }

    void Update()
    {
        HandleClick();
    }

    void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space)) return;
        if (GameState.game_over) return;

        if (need_reset)
        {
            ResetTimeNeeded();
            need_reset = false;
            return;
        }

        if (!started)
        {
            // begin trial
            started = true;
            start_time = Time.time;

            AudioManager.instance.PlaySound("Blip", true);

            text.text = "Counting . . .";
            Banner.color = ColorPalette.instance.blue;
        }
        else
        {
            started = false;

            AudioManager.instance.PlaySound("Blip", true);

            float click_time = Time.time - start_time;
            string click_time_str = "" + (int)click_time + "." + 
                MattMath.GetDigit(click_time, -1) + " seconds";

            if (click_time - target_time < -0.25f)
            {
                text.text = "Too fast!\r\n" + click_time_str;
                Banner.color = ColorPalette.instance.red;

                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
            }
            else if (click_time - target_time > 0.25f)
            {

                text.text = "Too slow!\r\n" + click_time_str;
                Banner.color = ColorPalette.instance.red;

                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
            }
            else
            {
                text.text = "You got it!\r\n" + click_time_str;
                Banner.color = ColorPalette.instance.green;
                need_reset = true;

                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);
            }
        }
    }
}
