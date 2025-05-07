using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingDoorTransition : MonoBehaviour
{
    public float in_length;
    public float wait_length;
    public float out_length;
    public Color color;

    public enum SlideType { Top, Bottom, Left, Right, TopBottom, LeftRight };
    public SlideType in_type;
    public SlideType out_type;

    public void Start()
    {
        StartCoroutine(Transist());
    }

    IEnumerator Transist()
    {
        Color set_color = new Color(color.r, color.g, color.b, 1);

        transform.Find("Top").GetComponent<Image>().color = set_color;
        transform.Find("Bottom").GetComponent<Image>().color = set_color;
        transform.Find("Left").GetComponent<Image>().color = set_color;
        transform.Find("Right").GetComponent<Image>().color = set_color;

        // Fade to black

        float alpha;
        float t = 0;

        while (t < in_length)
        {
            t += Time.unscaledDeltaTime;
            alpha = t / in_length;

            SetCurtain(alpha, in_type);

            yield return null;
        }

        SetCurtain(1, in_type);
        yield return null;



        // Black. Now Load
        TransitionManager.instance.CallLoadFunc();



        // Wait
        yield return new WaitForSecondsRealtime(wait_length);



        SetCurtain(0, in_type);
        SetCurtain(1, out_type);

        // Clear out
        t = out_length;
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime;
            alpha = t / out_length;

            SetCurtain(alpha, out_type);

            yield return null;
        }


        // Clear. Now finish up
        TransitionManager.instance.CallCompleteFunc();

        Destroy(gameObject);
    }

    void SetCurtain(float alpha, SlideType type)
    {
        // 0 is clear, 1 is full block

        if (type == SlideType.Top)
        {
            transform.Find("Top").GetComponent<RectTransform>().sizeDelta = new Vector2(0, alpha * 1080);
        }
        else if (type == SlideType.Bottom)
        {
            transform.Find("Bottom").GetComponent<RectTransform>().sizeDelta = new Vector2(0, alpha * 1080);
        }
        else if (type == SlideType.Left)
        {
            transform.Find("Left").GetComponent<RectTransform>().sizeDelta = new Vector2(alpha * 1920, 0);
        }
        else if (type == SlideType.Right)
        {
            transform.Find("Right").GetComponent<RectTransform>().sizeDelta = new Vector2(alpha * 1920, 0);
        }
        else if (type == SlideType.TopBottom)
        {
            alpha /= 2;
            transform.Find("Top").GetComponent<RectTransform>().sizeDelta = new Vector2(0, alpha * 1080);
            transform.Find("Bottom").GetComponent<RectTransform>().sizeDelta = new Vector2(0, alpha * 1080);
        }
        else if (type == SlideType.LeftRight)
        {
            alpha /= 2;
            transform.Find("Left").GetComponent<RectTransform>().sizeDelta = new Vector2(alpha * 1920, 0);
            transform.Find("Right").GetComponent<RectTransform>().sizeDelta = new Vector2(alpha * 1920, 0);
        }
    }
}
