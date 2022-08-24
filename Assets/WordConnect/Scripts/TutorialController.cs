using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance;

    public GameObject tutorialContainer;
    public GameObject tutorialFade;

    private void Awake()
    {
        Instance = this;
    }

    [ContextMenu("Delete FTUE Prefs")]
    public void ResetPrefs()
    {
        PlayerPrefs.DeleteKey("FTUEShowed");
    }

    public void OpenTutorial(int levelIndex)
    {
        if (levelIndex == 0 && PlayerPrefs.GetInt("FTUEShowed") == 0)
        {
            tutorialContainer.SetActive(true);
            tutorialFade.SetActive(true);
        }
        else
        {
            tutorialContainer.SetActive(false);
            tutorialFade.SetActive(false);
        }
    }

    public void DisableTutorialAnimation()
    {
        tutorialContainer.SetActive(false);
        tutorialFade.SetActive(false);
        PlayerPrefs.SetInt("FTUEShowed", 1);
    }
}