using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMixup : MonoBehaviour
{
    public Image color_indicator;

    void Start()
    {
        StartCoroutine(MixupRoutine());
    }

    IEnumerator MixupRoutine()
    {
        yield return new WaitForSeconds(1);

        int round = 0;

        while (round < 16)
        {
            float wait_time = Mathf.Pow(1.1f, -round) + 0.1f;

            int col = Random.Range(0, 4);
            color_indicator.enabled = true;
            color_indicator.color = col == 0 ? ColorPalette.instance.red :
                                    col == 1 ? ColorPalette.instance.yellow :
                                    col == 2 ? ColorPalette.instance.green :
                                               ColorPalette.instance.blue;

            AudioManager.instance.ResetValues();
            AudioManager.instance.SetPitch(0.749153538438f);
            AudioManager.instance.PlaySound("Blip", false);

            yield return new WaitForSeconds(wait_time); // reaction window

            // hide all cubes except correct one
            for (int i = 0; i < 4; i++)
            {
                transform.Find("ColorCubes").GetChild(i).gameObject.SetActive(i == col);
            }

            AudioManager.instance.PlaySound("Blip", true);

            // win/lose check

            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            bool fail_red = col == 0 && (mouse_pos.x > 0 || mouse_pos.z < 0);
            bool fail_yellow = col == 1 && (mouse_pos.x < 0 || mouse_pos.z < 0);
            bool fail_green = col == 2 && (mouse_pos.x > 0 || mouse_pos.z > 0);
            bool fail_blue = col == 3 && (mouse_pos.x < 0 || mouse_pos.z > 0);

            if (fail_red || fail_yellow || fail_green || fail_blue)
            {
                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
                break;
            }
            else
            {
                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(false);
            }


            yield return new WaitForSeconds(wait_time);

            // unhide all cubes
            for (int i = 0; i < 4; i++)
            {
                transform.Find("ColorCubes").GetChild(i).gameObject.SetActive(true);
            }

            color_indicator.enabled = false;

            yield return new WaitForSeconds(wait_time); // cooldown

            round++;
        }
    }
}
