using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AngleMeasure : MonoBehaviour
{
    public TextEntryBehavior textbox;
    public TMPro.TMP_Text answer_box;

    float solution_angle;

    void Start()
    {
        GenerateAngle();
    }

    void GenerateAngle()
    {
        float angle1 = Random.Range(0, 360);
        float angle2 = Random.Range(0, 360);

        transform.Find("Side1").localEulerAngles = new Vector3(0, angle1, 0);
        transform.Find("Side2").localEulerAngles = new Vector3(0, angle2, 0);

        transform.Find("Side1").position = Vector3.zero;
        transform.Find("Side2").position = Vector3.zero;

        Vector3 tip1_pos = transform.Find("Side1").Find("Line").Find("Tip").position;
        Vector3 tip2_pos = transform.Find("Side2").Find("Line").Find("Tip").position;
        Vector3 new_center = -(tip1_pos + tip2_pos) / 3;

        transform.Find("Side1").position = new_center;
        transform.Find("Side2").position = new_center;

        solution_angle = Mathf.Abs(Mathf.DeltaAngle(angle1, angle2));

        answer_box.text = "?";
    }

    public void SubmitAngle(string text)
    {
        float user_angle = float.Parse(text);

        answer_box.text = "" + (int)solution_angle;

        if (Mathf.Abs(user_angle - solution_angle) < 4)
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
        yield return new WaitForSeconds(1);

        GenerateAngle();
        textbox.input_ready = true;
        textbox.Clear();
    }
}
