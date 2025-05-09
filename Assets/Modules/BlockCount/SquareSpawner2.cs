using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner2 : MonoBehaviour
{
    public GameObject SquareFab;

    public void SpawnSquares(int how_many)
    {
        // clear old

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < how_many; i++)
        {
            Transform new_square = Instantiate(SquareFab).transform;
            new_square.SetParent(transform);
            new_square.position = new Vector3(-Random.Range(-10, 10f), 0, Random.Range(-5f, 5f));
            new_square.localScale = Vector3.one * Random.Range(0.5f, 3f);
            new_square.GetComponent<DVDLogo>().dir = 5f * new Vector3(
                Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        }
    }

}
