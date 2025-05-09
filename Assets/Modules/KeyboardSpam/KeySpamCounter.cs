using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpamCounter : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public KeyboardSpam module;

    int presses = 0;

    void Update()
    {
        if (GameState.game_over) return;

        string digit_to_add =
            Input.GetKeyDown(KeyCode.A) ? "A" :
            Input.GetKeyDown(KeyCode.B) ? "B" :
            Input.GetKeyDown(KeyCode.C) ? "C" :
            Input.GetKeyDown(KeyCode.D) ? "D" :
            Input.GetKeyDown(KeyCode.E) ? "E" :
            Input.GetKeyDown(KeyCode.F) ? "F" :
            Input.GetKeyDown(KeyCode.G) ? "G" :
            Input.GetKeyDown(KeyCode.H) ? "H" :
            Input.GetKeyDown(KeyCode.I) ? "I" :
            Input.GetKeyDown(KeyCode.J) ? "J" :
            Input.GetKeyDown(KeyCode.K) ? "K" :
            Input.GetKeyDown(KeyCode.L) ? "L" :
            Input.GetKeyDown(KeyCode.M) ? "M" :
            Input.GetKeyDown(KeyCode.N) ? "N" :
            Input.GetKeyDown(KeyCode.O) ? "O" :
            Input.GetKeyDown(KeyCode.P) ? "P" :
            Input.GetKeyDown(KeyCode.Q) ? "Q" :
            Input.GetKeyDown(KeyCode.R) ? "R" :
            Input.GetKeyDown(KeyCode.S) ? "S" :
            Input.GetKeyDown(KeyCode.T) ? "T" :
            Input.GetKeyDown(KeyCode.U) ? "U" :
            Input.GetKeyDown(KeyCode.V) ? "V" :
            Input.GetKeyDown(KeyCode.W) ? "W" :
            Input.GetKeyDown(KeyCode.X) ? "X" :
            Input.GetKeyDown(KeyCode.Y) ? "Y" :
            Input.GetKeyDown(KeyCode.Z) ? "Z" :
            Input.GetKeyDown(KeyCode.Keypad0) ? "0" :
            Input.GetKeyDown(KeyCode.Keypad1) ? "1" :
            Input.GetKeyDown(KeyCode.Keypad2) ? "2" :
            Input.GetKeyDown(KeyCode.Keypad3) ? "3" :
            Input.GetKeyDown(KeyCode.Keypad4) ? "4" :
            Input.GetKeyDown(KeyCode.Keypad5) ? "5" :
            Input.GetKeyDown(KeyCode.Keypad6) ? "6" :
            Input.GetKeyDown(KeyCode.Keypad7) ? "7" :
            Input.GetKeyDown(KeyCode.Keypad8) ? "8" :
            Input.GetKeyDown(KeyCode.Keypad9) ? "9" :
            Input.GetKeyDown(KeyCode.Alpha0) ? "0" :
            Input.GetKeyDown(KeyCode.Alpha1) ? "1" :
            Input.GetKeyDown(KeyCode.Alpha2) ? "2" :
            Input.GetKeyDown(KeyCode.Alpha3) ? "3" :
            Input.GetKeyDown(KeyCode.Alpha4) ? "4" :
            Input.GetKeyDown(KeyCode.Alpha5) ? "5" :
            Input.GetKeyDown(KeyCode.Alpha6) ? "6" :
            Input.GetKeyDown(KeyCode.Alpha7) ? "7" :
            Input.GetKeyDown(KeyCode.Alpha8) ? "8" :
            Input.GetKeyDown(KeyCode.Alpha9) ? "9" : "";

        if (digit_to_add != "")
        {
            presses++;
            module.NotifyKeys(presses);
        }

        text.text = "" + MattMath.GetDigit(presses, 2)
                       + MattMath.GetDigit(presses, 1)
                       + MattMath.GetDigit(presses, 0);
    }
}
