using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSpam : MonoBehaviour
{
    public TimerBehavior timer;

    public void NotifyKeys(int presses)
    {
        timer.StartTimer();

        if (presses >= 300)
        {
            transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);
            timer.StopTimer();
        }
    }

    public void OutOfTime()
    {
        transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
    }
}
