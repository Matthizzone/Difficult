using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisDots : MonoBehaviour
{
    bool started = false;
    bool disabled = false;

    int round = 0;

    public GameObject DotFab;

    void Start()
    {
        GenerateDots();
    }

    void GenerateDots()
    {
        // clear dots

        for (int i = transform.Find("Dots").childCount - 1; i >= 0; i--)
        {
            Destroy(transform.Find("Dots").GetChild(i).gameObject);
        }


        // generate new dots

        int num_dots = 3 + round;

        for (int i = 0; i < num_dots; i++)
        {
            Transform new_dot = Instantiate(DotFab).transform;
            new_dot.position = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-5f, 3.75f));
            new_dot.parent = transform.Find("Dots");
        }

        started = false;
    }

    void Update()
    {
        MouseMove();
    }

    void MouseMove()
    {
        if (GameState.game_over) return;
        if (disabled) return;

        Vector3 mouse_pos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        mouse_pos.y = 0;


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                // disappear dots

                if (!started)
                {
                    for (int i = 0; i < transform.Find("Dots").childCount; i++)
                    {
                        transform.Find("Dots").GetChild(i).GetComponent<Renderer>().enabled = false;
                    }

                    started = true;
                }

                // make dot reappear

                hit.transform.GetComponent<Renderer>().enabled = true;
                AudioManager.instance.PlaySound("Blip", true);


                bool all_clicked = true;

                for (int i = 0; i < transform.Find("Dots").childCount; i++)
                {
                    all_clicked &= transform.Find("Dots").GetChild(i).GetComponent<Renderer>().enabled;
                }

                // win check
                if (all_clicked)
                {
                    // got them all
                    transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);

                    round++;

                    if (round < 3)
                    {
                        StartCoroutine(WaitThenNew());
                    }
                }
            }
            else
            {
                if (started)
                {
                    transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();

                    // reveal all dots

                    for (int i = 0; i < transform.Find("Dots").childCount; i++)
                    {
                        transform.Find("Dots").GetChild(i).GetComponent<Renderer>().enabled = true;
                    }
                }
            }
        }
    }

    IEnumerator WaitThenNew()
    {
        disabled = true;
        yield return new WaitForSeconds(1);

        disabled = false;
        GenerateDots();
    }
}
