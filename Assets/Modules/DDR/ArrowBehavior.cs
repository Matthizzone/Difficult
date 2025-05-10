using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        Move();
        CheckMiss();
    }

    void Move()
    {
        if (GameState.game_over) return;

        transform.position += Vector3.forward * Time.deltaTime * speed;
        transform.position += Vector3.up * Time.deltaTime * 0.1f;
    }

    void CheckMiss()
    {
        if (transform.position.z > 4)
        {
            transform.parent.parent.parent.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
        }
    }

    public void DestroyArrow()
    {
        GetComponent<Renderer>().enabled = false;
        speed = 0f;
        transform.GetChild(0).GetComponent<ParticleSystem>().Emit(30);
        transform.parent = null;

        StartCoroutine(WaitThenDelete());
    }

    IEnumerator WaitThenDelete()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
