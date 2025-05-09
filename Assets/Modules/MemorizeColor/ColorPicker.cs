using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public MemorizeColor mod;

    public Slider hue;
    public Slider sat;
    public Slider val;

    void Update()
    {
        transform.GetChild(0).GetComponent<Image>().color =
            Color.HSVToRGB(hue.value, sat.value, val.value);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mod.SubmitColor(Color.HSVToRGB(hue.value, sat.value, val.value));
        }
    }
}
