using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    public float in_length;
    public float wait_length;
    public float out_length;
    public Color color;

    public void Start()
    {
        StartCoroutine(TransitionRoutine());
    }

    IEnumerator TransitionRoutine()
    {
        // Fade to black

        float alpha;
        float t = 0;

        while (t < in_length)
        {
            t += Time.unscaledDeltaTime;
            alpha = t / in_length;

            transform.Find("Curtain").GetComponent<Image>().color =
                new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        transform.Find("Curtain").GetComponent<Image>().color =
            new Color(color.r, color.g, color.b, 1);

        yield return null;


        // Black. Now Load
        TransitionManager.instance.CallLoadFunc();



        // Wait
        yield return new WaitForSecondsRealtime(wait_length);
        


        // Clear out
        t = out_length;
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime;
            alpha = t / out_length;

            transform.Find("Curtain").GetComponent<Image>().color =
                new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }


        // Clear. Now finish up
        TransitionManager.instance.CallCompleteFunc();

        Destroy(gameObject);
    }
}
