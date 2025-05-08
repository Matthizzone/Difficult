using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class CircleCenter : MonoBehaviour
{
    bool clickable = true;

    public Transform Circle;

    private void Start()
    {
        SetCirclePos();
    }

    void SetCirclePos()
    {
        Circle.position = new Vector3(
            Random.Range(-9f, 9f),
            0,
            Random.Range(-4f, 4f)
            );
        Circle.GetComponent<Renderer>().enabled = false;

        Circle.GetChild(0).GetComponent<Transform>().localScale = Vector3.one * Random.Range(20f, 50f);
    }

    void Update()
    {
        HandleClick();
    }

    void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space)) return;
        if (GameState.game_over) return;
        if (!clickable) return;

        clickable = false;

        AudioManager.instance.PlaySound("Blip", true);


        Vector3 mouse_pos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        mouse_pos.y = 0;

        Vector3 offset = mouse_pos - Circle.position;

        Circle.GetComponent<Renderer>().enabled = true;

        if (offset.magnitude > 0.15f)
        {
            transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
        }
        else
        {
            transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);

            StartCoroutine(WaitThenNextRoutine());
        }
        
    }

    IEnumerator WaitThenNextRoutine()
    {
        yield return new WaitForSeconds(1);

        SetCirclePos();
        clickable = true;
    }
}

