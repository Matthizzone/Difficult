using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArrowBehavior;

public class ArrowDrum : MonoBehaviour
{
    public enum Directions { Left, Down, Up, Right };
    public Directions my_dir = Directions.Right;

    void Update()
    {
        HandlePresses();
    }

    void HandlePresses()
    {
        if (GameState.game_over) return;

        string key_pressed = "";

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            key_pressed = "V";
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            key_pressed = "^";
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            key_pressed = "<";
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            key_pressed = ">";
        }


        if (key_pressed == "") return;
        if (transform.localPosition.magnitude > 400f) return;


        if (my_dir == Directions.Down && key_pressed == "V")
        {
            DestroyArrow();
        }
        if (my_dir == Directions.Up && key_pressed == "^")
        {
            DestroyArrow();
        }
        if (my_dir == Directions.Left && key_pressed == "<")
        {
            DestroyArrow();
        }
        if (my_dir == Directions.Right && key_pressed == ">")
        {
            DestroyArrow();
        }
    }

    void DestroyArrow()
    {
        if (transform.GetChild(0).localPosition.magnitude > 0.5f)
        {
            transform.parent.parent.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
            return;
        }

        AudioManager.instance.PlaySound("Blip", true);
        transform.GetChild(0).GetComponent<ArrowBehavior>().DestroyArrow();
    }
}
