using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareExpiration : MonoBehaviour
{
    void Update()
    {
        if (transform.position.magnitude > 20) Destroy(gameObject);
    }
}
