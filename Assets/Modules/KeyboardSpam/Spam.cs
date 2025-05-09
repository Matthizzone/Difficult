using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spam : MonoBehaviour
{
    public TimerBehavior timer;

    public float target_amt;

    public void NotifyKeys(int presses)
    {
        timer.StartTimer();

        if (presses >= target_amt)
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
