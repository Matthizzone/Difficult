using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WheelStop : MonoBehaviour
{
    int sphere_i = 0;
    public int scale = 5;

    bool running = false;

    private void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space)) return;
        if (GameState.game_over) return;
        
        running = !running;

        if (!running)
        {
            // just stopped the wheel

            if (sphere_i / scale == 0)
            {
                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);
            }
            else
            {
                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
            }
        }
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
