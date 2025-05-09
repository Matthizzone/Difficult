using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionTime : MonoBehaviour
{
    bool started = false;
    float flash_time = 0;

    public TMPro.TMP_Text text;
    public Image Banner;

    public float max = 0.220f;

    void Update()
    {
        HandleClick();

        if (started && Time.time > flash_time)
        {
            text.text = "Click!";
            Banner.color = ColorPalette.instance.yellow;
        }
    }

    void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space)) return;
        if (GameState.game_over) return;

        if (!started)
        {
            // begin trial
            started = true;
            flash_time = Time.time + Random.Range(3f, 7f);

            AudioManager.instance.PlaySound("Blip", true);

            text.text = "Waiting . . .";
            Banner.color = ColorPalette.instance.blue;

        }
        else
        {
            started = false;

            AudioManager.instance.PlaySound("Blip", true);

            float click_time = Time.time - flash_time;
            string click_time_str = "" +
                MattMath.GetDigit(click_time, -1) +
                MattMath.GetDigit(click_time, -2) +
                MattMath.GetDigit(click_time, -3) + " ms";

            if (Time.time < flash_time)
            {
                text.text = "Too fast!";
                Banner.color = ColorPalette.instance.red;

                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
            }
            else if (click_time > max)
            {

                text.text = "Too slow!\r\n" + click_time_str;
                Banner.color = ColorPalette.instance.red;

                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
            }
            else
            {

                text.text = "You got it!\r\n" + click_time_str;
                Banner.color = ColorPalette.instance.green;

                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);
            }
        }
    }
}
