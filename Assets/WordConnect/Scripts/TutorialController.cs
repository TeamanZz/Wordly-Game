using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance;

    public GameObject tutorialContainer;

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
        }
        else
        {
            tutorialContainer.SetActive(false);
        }
    }

    public void DisableTutorialAnimation()
    {
        tutorialContainer.SetActive(false);
        PlayerPrefs.SetInt("FTUEShowed", 1);
    }
}