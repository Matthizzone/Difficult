using UnityEngine;
using UnityEngine.Events;

public class LoadEvent : UnityEvent { }

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    public LoadEvent loadEvent = new LoadEvent();
    public LoadEvent completeEvent = new LoadEvent();

    public GameObject FadeFab;
    public GameObject SlidingFab;
    public GameObject CutoutFab;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    public void StartFade(float in_length, float wait_length, float out_length, Color color)
    {
        Transform newTransition = Instantiate(FadeFab).transform;
        newTransition.SetParent(transform);

        newTransition.GetComponent<FadeTransition>().in_length = in_length;
        newTransition.GetComponent<FadeTransition>().wait_length = wait_length;
        newTransition.GetComponent<FadeTransition>().out_length = out_length;
        newTransition.GetComponent<FadeTransition>().color = color;

        GameState.currently_transitioning = true;
    }

    public void StartSlide(float in_length, float wait_length, float out_length, Color color, SlidingDoorTransition.SlideType in_type, SlidingDoorTransition.SlideType out_type)
    {
        Transform newTransition = Instantiate(SlidingFab).transform;
        newTransition.SetParent(transform);

        newTransition.GetComponent<SlidingDoorTransition>().in_length = in_length;
        newTransition.GetComponent<SlidingDoorTransition>().wait_length = wait_length;
        newTransition.GetComponent<SlidingDoorTransition>().out_length = out_length;
        newTransition.GetComponent<SlidingDoorTransition>().color = color;
        newTransition.GetComponent<SlidingDoorTransition>().in_type = in_type;
        newTransition.GetComponent<SlidingDoorTransition>().out_type = out_type;

        GameState.currently_transitioning = true;
    }

    public void StartCutout(float in_length, float wait_length, float out_length, Color color)
    {
        Transform newTransition = Instantiate(CutoutFab).transform;
        newTransition.SetParent(transform);

        newTransition.GetComponent<CutoutTransition>().in_length = in_length;
        newTransition.GetComponent<CutoutTransition>().wait_length = wait_length;
        newTransition.GetComponent<CutoutTransition>().out_length = out_length;
        newTransition.GetComponent<CutoutTransition>().color = color;

        GameState.currently_transitioning = true;
    }

    public void CallLoadFunc()
    {
        // called by transition prefab when screen is fully black

        loadEvent.Invoke();
        loadEvent.RemoveAllListeners();
    }

    public void CallCompleteFunc()
    {
        // called by transition prefab when screen is back to clear

        completeEvent.Invoke();
        completeEvent.RemoveAllListeners();

        GameState.currently_transitioning = false;
    }
}
