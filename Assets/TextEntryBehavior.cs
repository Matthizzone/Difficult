using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class TextEntryBehavior : MonoBehaviour
{
    public bool input_ready;

    public bool accepts_letters;
    public bool accepts_numbers;

    string text = "";

    public int max_length;

    public TMPro.TMP_Text textbox;

    void Start()
    {
        text = "";
        textbox.text = text;
    }

    void Update()
    {
        CheckKeyStroke();
    }

    void CheckKeyStroke()
    {
        if (!input_ready) return;

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
            // checks

            if (!accepts_numbers && char.IsDigit(digit_to_add[0]))
            {
                digit_to_add = "";
            }

            if (!accepts_letters && char.IsLetter(digit_to_add[0]))
            {
                digit_to_add = "";
            }
        }


        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
        {
            digit_to_add = "<";
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            digit_to_add = ">";
        }


        if (digit_to_add != "")
        {
            ApplyKeyStroke(digit_to_add);
        }
    }

    void ApplyKeyStroke(string digit_to_add)
    {
        if (digit_to_add == "<")
        {
            if (text.Length > 0)
            {
                text = text.Substring(0, text.Length - 1);

                AudioManager.instance.ResetValues();
                AudioManager.instance.SetRoundRobin(5);
                AudioManager.instance.SetPitchRandomize(0.1f);
                AudioManager.instance.PlaySound("Keyboard/KeyPress", false);
            }
        }
        else if (digit_to_add == ">")
        {
            if (transform.parent.parent.GetComponent<AngleMeasure>() != null)
            {
                transform.parent.parent.GetComponent<AngleMeasure>().SubmitAngle(text);
            }
            else if (transform.parent.parent.GetComponent<SquareCount>() != null)
            {
                transform.parent.parent.GetComponent<SquareCount>().SubmitAnswer(text);
            }
            input_ready = false;

            AudioManager.instance.PlaySound("Keyboard/KeyEnter", true);
        }
        else
        {
            if (text.Length < max_length)
            {
                text += digit_to_add;
            }

            AudioManager.instance.ResetValues();
            AudioManager.instance.SetRoundRobin(5);
            AudioManager.instance.SetPitchRandomize(0.1f);
            AudioManager.instance.PlaySound("Keyboard/KeyPress", false);
        }

        textbox.text = text;
    }

    public void Clear()
    {
        text = "";
        textbox.text = text;
    }
}