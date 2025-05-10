using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorBehavior : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<RectTransform>().position.x < 300)
        {
            transform.parent.parent.parent.Find("Checks").GetComponent<Checks>().AddX();
        }
    }
}
