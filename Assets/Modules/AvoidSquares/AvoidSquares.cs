using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidSquares : MonoBehaviour
{
    float end_time;

    void Start()
    {
        end_time = Time.time + 20;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
        }

        if (Time.time > end_time)
        {
            transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);
        }
    }
}
