using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemorizeColor : MonoBehaviour
{
    public ColorPicker color_picker;
    public Image answer_box;

    Color solution_color;

    void Start()
    {
        GenerateColor();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            answer_box.color = new Color(0.76f, 0.76f, 0.76f);
            answer_box.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void GenerateColor()
    {
        solution_color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        answer_box.color = solution_color;
        answer_box.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SubmitColor(Color submission)
    {
        float color_distance = 0;
        color_distance += Mathf.Abs(submission.r - solution_color.r);
        color_distance += Mathf.Abs(submission.g - solution_color.g);
        color_distance += Mathf.Abs(submission.b - solution_color.b);

        answer_box.color = solution_color;
        answer_box.transform.GetChild(0).gameObject.SetActive(false);

        if (color_distance < 0.25f)
        {
            transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);

            if (!GameState.game_over) StartCoroutine(WaitThenNextRoutine());
        }
        else
        {
            transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddX();
        }
    }

    IEnumerator WaitThenNextRoutine()
    {
        yield return new WaitForSeconds(2.5f);

        GenerateColor();
    }
}
