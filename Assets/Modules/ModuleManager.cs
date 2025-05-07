using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{
    int[] order;
    int num_completed;

    public static ModuleManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        order = new int[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            order[i] = i;
        }

        MattMath.FisherYatesShuffle(order);

        for (int i = 0; i < transform.childCount; i++)
        {
            order[i] = 2;

            print(order[i]);
        }

        //GoToModule(order[num_completed]);
        GoToModule(2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameState.game_over = true;
            LoaderBehavior.instance.FadeToScene(0, 1f, 1f, 1f);
        }
    }

    void GoToModule(int module_num)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject screen = transform.GetChild(i).gameObject;

            screen.SetActive(i == module_num);
        }
    }

    public void ModuleVictory()
    {
        AudioManager.instance.PlaySound("Victory", true);


        GameState.successes[order[num_completed]]++;
        GameState.attempts[order[num_completed]]++;

        SaveSystem.SaveData();


        num_completed++;
        StartCoroutine(WaitThenAdvance());
    }
    IEnumerator WaitThenAdvance()
    {
        yield return new WaitForSeconds(3);

        GoToModule(order[num_completed]);
    }

    public void GameOver()
    {
        AudioManager.instance.PlaySound("GameOver", true);
        GameState.game_over = true;


        GameState.attempts[order[num_completed]]++;

        SaveSystem.SaveData();


        StartCoroutine(WaitThenReturn());
    }

    IEnumerator WaitThenReturn()
    {
        yield return new WaitForSeconds(3);

        LoaderBehavior.instance.FadeToScene(0, 1f, 1f, 1f);
    }
}
