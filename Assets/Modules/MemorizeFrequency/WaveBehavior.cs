using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehavior : MonoBehaviour
{
    Vector3[] positions;

    public float time_shift = 1;
    public float freq = 20; // 10 to 30

    private void Start()
    {
        positions = new Vector3[100];
    }

    void Update()
    {
        float x_start = transform.position.x - 4f;
        float x_end = transform.position.x + 4f;

        for (int i = 0; i < positions.Length; i++)
        {
            float t = (float)i / 100;
            float t2 = t * 2 - 1;

            positions[i] = new Vector3(
                Mathf.Lerp(x_start, x_end, t),
                1f,
                transform.position.z + Mathf.Pow(100, -Mathf.Pow(t2, 2)) * Mathf.Sin(freq * (t2 + Time.time * time_shift))
                );
        }

        GetComponent<LineRenderer>().SetPositions(positions);

        GetComponent<AudioSource>().pitch = OctavePitchCurve((freq - 20f) / 10f);
        GetComponent<AudioSource>().volume = 1f - (freq - 10f) / 40f;
    }

    public static float OctavePitchCurve(float x)
    {
        x = Mathf.Clamp(x, -1f, 1f); // Ensure input remains bounded
        return Mathf.Pow(2f, x);
    }
}
