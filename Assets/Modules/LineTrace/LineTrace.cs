using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTrace : MonoBehaviour
{
    bool started = false;

    public Material LightGrayMaterial;
    public Material BlueMaterial;

    void Update()
    {
        MouseMove();
    }

    void MouseMove()
    {
        if (GameState.game_over) return;

        Vector3 mouse_pos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        mouse_pos.y = 0;

        if (!started)
        {
            // check for start

            if (Mathf.Abs(mouse_pos.x + 8) < 0.25f && Mathf.Abs(mouse_pos.z) < 0.25f)
            {
                started = true;
                AudioManager.instance.PlaySound("Blip", true);

                // set materials

                transform.Find("Start").GetComponent<Renderer>().material = LightGrayMaterial;
                transform.Find("Line").GetComponent<Renderer>().material = BlueMaterial;
                transform.Find("End").GetComponent<Renderer>().material = BlueMaterial;
            }
        }
        else
        {
            // check z pos
            bool too_far_left = mouse_pos.x < -8.25f;
            bool cube_violation = mouse_pos.x < -7.75f && Mathf.Abs(mouse_pos.z) > 0.25f;
            bool line_violation = mouse_pos.x > -7.75f && Mathf.Abs(mouse_pos.z) > 0.05f;

            if (too_far_left || cube_violation || line_violation)
            {
                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
            }

            if (mouse_pos.x > 7.75f)
            {
                transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);
            }
        }
    }
}


