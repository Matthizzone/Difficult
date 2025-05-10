using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject ArrowFab;

    float spawn_time = 0;

    public float down_amt;

    float start_time = 0;

    void Start()
    {
        start_time = Time.time;
    }

    void Update()
    {
        if (Time.time - start_time > 27)
        {
            transform.parent.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);
        }
        else
        {
            SpawnCycle();
        }
    }

    void SpawnCycle()
    {
        if (Time.time > spawn_time)
        {
            // spawn

            int dir = Random.Range(0, 4);

            Transform new_arrow = Instantiate(ArrowFab).transform;
            new_arrow.SetParent(transform.GetChild(dir));
            new_arrow.localRotation = Quaternion.identity;
            new_arrow.localScale = Vector3.one;
            new_arrow.localPosition = Vector3.zero;
            new_arrow.position -= Vector3.forward * down_amt;


            float t = (Time.time - start_time);
            float spawn_interval = Mathf.Pow(1.1f, -t);

            spawn_time = Time.time + spawn_interval;
        }
    }
}
