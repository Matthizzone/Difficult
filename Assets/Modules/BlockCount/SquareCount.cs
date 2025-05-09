using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCount : MonoBehaviour
{
    public TextEntryBehavior textbox;
    public TMPro.TMP_Text answer_box;

    int solution_count;

    int round = 0;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        solution_count = Random.Range(12, 18) + round * 6;

        transform.Find("Squares").GetComponent<SquareSpawner2>().SpawnSquares(solution_count);

        answer_box.text = "?";
    }

    public void SubmitAnswer(string text)
    {
        float user_angle = int.Parse(text);

        answer_box.text = "" + solution_count;

        if (user_angle == solution_count)
        {
            transform.Find("Canvas").Find("Checks").GetComponent<Checks>().AddCheck(true);
            round++;

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

        Generate();
        textbox.input_ready = true;
        textbox.Clear();
    }
}
