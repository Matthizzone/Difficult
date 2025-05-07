using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checks : MonoBehaviour
{
    [Header("Spawn Info")]
    public GameObject CheckFab;
    public int num_checks;
    public float check_scale;
    public float check_spacing;
    public Vector3 center_point;

    [Header("Sprites")]
    public Sprite Check;
    public Sprite Check_BG;
    public Sprite X;
    public Sprite X_BG;

    int check_i = 0;

    void Start()
    {
        GenerateChecks();
    }

    void GenerateChecks()
    {
        if (Mathf.Abs(center_point.x) < 0.1f)
        {
            // generate horizontally

            for (int i = 0; i < num_checks; i++)
            {
                Vector3 spacing_vec = Vector3.right * check_spacing;
                Vector3 start_point = center_point - spacing_vec * ((num_checks - 1) / 2f);

                RectTransform new_check = Instantiate(CheckFab).GetComponent<RectTransform>();
                new_check.SetParent(transform);
                new_check.anchoredPosition = start_point + spacing_vec * i;
                new_check.localScale = Vector3.one * check_scale;
            }
        }
        else
        {
            // generate vertically
        }
    }

    public void AddCheck()
    {
        if (check_i >= num_checks) return;

        transform.GetChild(check_i).GetComponent<Image>().sprite = Check_BG;
        transform.GetChild(check_i).GetChild(0).GetComponent<Image>().sprite = Check;
        transform.GetChild(check_i).GetChild(0).GetComponent<Image>().color = ColorPalette.instance.green;

        check_i++;
    }

    public void AddX()
    {
        if (check_i >= num_checks) return;

        transform.GetChild(check_i).GetComponent<Image>().sprite = X_BG;
        transform.GetChild(check_i).GetChild(0).GetComponent<Image>().sprite = X;
        transform.GetChild(check_i).GetChild(0).GetComponent<Image>().color = ColorPalette.instance.red;

        check_i++;
    }
}
