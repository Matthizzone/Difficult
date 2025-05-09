using UnityEngine;

public class MouseSpamCounter : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public Spam module;

    int distance = 0;

    bool started = false;

    void Update()
    {
        if (GameState.game_over) return;

        if (Input.GetMouseButtonDown(0))
        {
            started = true;
            distance = 0;

            module.NotifyKeys(0);
        }

        if (!started) return;

        distance += (int)Mathf.Abs(Input.GetAxisRaw("Mouse X"));
        distance += (int)Mathf.Abs(Input.GetAxisRaw("Mouse Y"));


        module.NotifyKeys(distance);

        text.text = "" + MattMath.GetDigit(distance, 4)
                       + MattMath.GetDigit(distance, 3)
                       + MattMath.GetDigit(distance, 2)
                       + MattMath.GetDigit(distance, 1)
                       + MattMath.GetDigit(distance, 0);
    }
}
