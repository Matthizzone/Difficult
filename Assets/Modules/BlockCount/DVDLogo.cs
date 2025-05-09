using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVDLogo : MonoBehaviour
{
    public Vector3 dir;
    public Vector3 bounds;

    void Update()
    {
        transform.position += dir * Time.deltaTime;

        if (transform.position.x < -bounds.x || transform.position.x > bounds.x)
        {
            dir.x *= -1;
        }
        if (transform.position.z < -bounds.z || transform.position.z > bounds.z)
        {
            dir.z *= -1;
        }
    }
}
