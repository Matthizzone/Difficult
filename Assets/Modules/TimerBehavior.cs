using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{
    public Spam module;

    public float time_left = 10f;

    bool started = false;
    bool finished = false;

    void Update()
    {
        if (started && !finished)
        {
            time_left -= Time.deltaTime;
            transform.Find("Textbox").GetComponent<TMPro.TMP_Text>().text = ""
                + MattMath.GetDigit(time_left, 0) + "."
                + MattMath.GetDigit(time_left, -1)
                + MattMath.GetDigit(time_left, -2)
                + MattMath.GetDigit(time_left, -3);

            if (time_left < 0)
            {
                time_left = 0;
                finished = true;

                transform.Find("Textbox").GetComponent<TMPro.TMP_Text>().text = "0.000";

                module.OutOfTime();
            }
        }
    }

    public void StartTimer()
    {
        started = true;
    }

    public void StopTimer()
    {
        started = false;
    }
}
