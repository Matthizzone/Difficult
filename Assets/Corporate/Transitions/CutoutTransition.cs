using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutoutTransition : MonoBehaviour
{
    public float in_length;
    public float wait_length;
    public float out_length;
    public Color color;

    public void Start()
    {
        StartCoroutine(Transist());
    }

    IEnumerator Transist()
    {
        Color set_color = new Color(color.r, color.g, color.b, 1);

        transform.Find("Cutout").Find("Black").GetComponent<Image>().color = set_color;

        // Fade to black

        float alpha;
        float t = 0;

        while (t < in_length)
        {
            t += Time.unscaledDeltaTime;
            alpha = t / in_length;

            SetCurtain(alpha);

            yield return null;
        }

        SetCurtain(1);
        yield return null;


        // Black. Now Load
        TransitionManager.instance.CallLoadFunc();



        // Wait
        yield return new WaitForSecondsRealtime(wait_length);



        SetCurtain(0);
        SetCurtain(1);

        // Clear out
        t = out_length;
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime;
            alpha = t / out_length;

            SetCurtain(alpha);

            yield return null;
        }


        // Clear. Now finish up
        TransitionManager.instance.CallCompleteFunc();

        Destroy(gameObject);
    }

    void SetCurtain(float alpha)
    {
        // 0 is clear, 1 is full block

        transform.Find("Cutout").GetComponent<RectTransform>().sizeDelta = Vector2.one * 2200 * (1 - alpha);
    }
}
