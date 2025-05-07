using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    int sphere_i = 0;
    public int scale = 5;

    bool running = false;

    int successes = 0;
    int attempts = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            running = !running;

            if (!running)
            {
                // just stopped the wheel

                attempts++;
                if (sphere_i / scale == 0) successes++;

                UpdateRate();
            }

        }
    }

    void UpdateRate()
    {
        float success_rate = attempts == 0 ? 0 : (float)(successes) / attempts * 100f;
        string rate_txt = "" + MattMath.GetDigit(success_rate, 1)
                             + MattMath.GetDigit(success_rate, 0) + "."
                             + MattMath.GetDigit(success_rate, -1)
                             + MattMath.GetDigit(success_rate, -2) + "%";

        GameObject.Find("SuccessRate").GetComponent<TMPro.TMP_Text>().text =
            "" + successes + " / " + attempts + " = " + rate_txt;
    }

    private void FixedUpdate()
    {
        SetActiveSphere(sphere_i / scale);

        if (running) sphere_i++;
        if (sphere_i >= 50 * scale) sphere_i = 0;

        if (sphere_i % scale == 0 && (sphere_i / scale) % 2 == 0 && running)
        {
            AudioManager.instance.PlaySound("Blip", true);
        }
    }

    void SetActiveSphere(int which)
    {
        which = Mathf.Clamp(which, 0, transform.Find("Ring").childCount);

        for (int i = 0; i < transform.Find("Ring").childCount; i++)
        {
            float scale = i == which ? 0.5f : 0.2f;
            transform.Find("Ring").GetChild(i).localScale = Vector3.one * scale;
        }
    }



    /*
    public GameObject SphereFab;

    void SpawnSphereCircle()
    {
        for (int i = 0; i < 50; i++)
        {
            float angle = (float)i / 50 * Mathf.PI * 2;

            Transform new_sphere = Instantiate(SphereFab).transform;
            new_sphere.parent = transform;
            new_sphere.position = 5 * new Vector3(
                Mathf.Cos(angle),
                0,
                Mathf.Sin(angle)
                );
        }
    }
    */
}
