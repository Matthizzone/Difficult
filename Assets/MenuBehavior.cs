using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    public enum Menus { MainMenu, Stats }
    public Menus current_menu = Menus.MainMenu;

    private void Start()
    {
        SaveSystem.LoadData();
        GameState.loaded = true;

        GoToMenu(Menus.MainMenu);
    }

    void Update()
    {
        HandleNavigation();
    }

    void HandleNavigation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameState.game_over = false;
            LoaderBehavior.instance.FadeToScene(1, 0.3f, 0.3f, 0.3f);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GoToMenu(Menus.Stats);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (current_menu == Menus.Stats)
            {
                GoToMenu(Menus.MainMenu);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    void GoToMenu(Menus new_menu)
    {
        current_menu = new_menu;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject screen = transform.GetChild(i).gameObject;

            screen.SetActive(screen.name == current_menu.ToString());
        }

        if (current_menu == Menus.Stats) CalculateStats();
    }

    void CalculateStats()
    {
        float[] success_rates = new float[GameState.attempts.Length];

        for (int i = 0; i < GameState.attempts.Length; i++)
        {
            success_rates[i] = 0f;
            if (GameState.attempts[i] > 0) success_rates[i] = (float)GameState.successes[i] / GameState.attempts[i];
            success_rates[i] *= 100f;

            string success_rate_str = "" + (success_rates[i] >= 100 ? MattMath.GetDigit(success_rates[i], 2) : "")
                + MattMath.GetDigit(success_rates[i], 1) 
                + MattMath.GetDigit(success_rates[i], 0) + "." +
                + MattMath.GetDigit(success_rates[i], -1) 
                + MattMath.GetDigit(success_rates[i], -2) + "%";

            transform.Find("Stats").Find("Values").GetChild(i).GetComponent<TMPro.TMP_Text>().text =
                "" + GameState.successes[i] + " / " + GameState.attempts[i] + " = " + success_rate_str;
        }

        float overall_success_rate = 100;

        for (int i = 0; i < success_rates.Length; i++)
        {
            success_rates[i] /= 100f;
            overall_success_rate *= success_rates[i];
        }

        string overall_success_rate_str = "" + (overall_success_rate >= 100 ? MattMath.GetDigit(overall_success_rate, 2) : "")
                + MattMath.GetDigit(overall_success_rate, 1)
                + MattMath.GetDigit(overall_success_rate, 0) + ".";

        int digit_i = -1;
        while (MattMath.GetDigit(overall_success_rate, digit_i) == 0)
        {
            overall_success_rate_str += MattMath.GetDigit(overall_success_rate, digit_i);
            digit_i--;

            if (digit_i <= -32)
            {
                break;
            }
        }
        overall_success_rate_str += MattMath.GetDigit(overall_success_rate, digit_i) + "%";

        transform.Find("Stats").Find("VictoryChance").GetComponent<TMPro.TMP_Text>().text =
            "Chance of victory: " + overall_success_rate_str;
    }
}
