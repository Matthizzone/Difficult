using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    public GameObject SquareFab;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.017f);

            SpawnSquare();
        }
    }


    void SpawnSquare()
    {
        Transform new_square = Instantiate(SquareFab).transform;
        new_square.SetParent(transform);
        new_square.position = GetRandomEdgePoint(15f, 9f);
        new_square.localScale = Vector3.one * Random.Range(0.5f, 3f);
        new_square.GetComponent<SlowMove>().dir = 4f * new Vector3(
            Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
    }

    public static Vector3 GetRandomEdgePoint(float halfWidth, float halfHeight)
    {
        float angle = Random.Range(0f, 2 * Mathf.PI);
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Scale the direction vector until it hits the rectangle boundary
        float tMax = float.PositiveInfinity;

        if (direction.x != 0)
        {
            float tx = halfWidth / Mathf.Abs(direction.x);
            tMax = Mathf.Min(tMax, tx);
        }

        if (direction.y != 0)
        {
            float ty = halfHeight / Mathf.Abs(direction.y);
            tMax = Mathf.Min(tMax, ty);
        }

        Vector2 p = direction * tMax;

        return new Vector3(p.x, 0, p.y);
    }
}
