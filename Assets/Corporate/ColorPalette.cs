using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public Color red;
    public Color blue;
    public Color yellow;
    public Color green;
    public Color light_grey;
    public Color off_white;

    public static ColorPalette instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
