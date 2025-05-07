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

    void Update()
    {
        HandleClick();

        if (started && Time.time > flash_time)
        {
            text.text = "Click!";
            Banner.color = new Color(0.89f, 0.77f, 0.33f); // yellow
        }
    }

    void HandleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!started)
            {
                // begin trial
                started = true;
                flash_time = Time.time + Random.Range(3f, 7f);

                text.text = "Waiting . . .";
                Banner.color = new Color(0.3f, 0.44f, 0.8f); // blue

            }
            else
            {
                started = false;

                float click_time = Time.time - flash_time;
                string click_time_str = "" +
                    MattMath.GetDigit(click_time, -1) +
                    MattMath.GetDigit(click_time, -2) +
                    MattMath.GetDigit(click_time, -3) + " ms";

                if (Time.time < flash_time)
                {
                    text.text = "Too fast!";
                    Banner.color = new Color(0.84f, 0.29f, 0.27f); // red
                    
                }
                else if (click_time > 0.2f)
                {

                    text.text = "Too slow!\r\n" + click_time_str;
                    Banner.color = new Color(0.84f, 0.29f, 0.27f); // red
                }
                else
                {

                    text.text = "You got it!\r\n" + click_time_str;
                    Banner.color = new Color(0.42f, 0.77f, 0.37f); // green
                }
            }
        }
    }
}
