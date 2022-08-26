using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using WordConnect;
using UnityEngine.UI;
using BBG;

public class RewardPopup : MonoBehaviour
{
    [SerializeField] private LevelCompletePopup levelCompletePopup;
    [SerializeField] private CanvasGroup firstPanel;
    [SerializeField] private CanvasGroup secondPanel;
    [SerializeField] private CanvasGroup lastCanvasGroup;
    [SerializeField] private CanvasGroup titleText;
    [SerializeField] private GameObject awardParticlesContainer;
    [SerializeField] private UIRibbonAnimation uiRibbonAnimation;
    [SerializeField] private float hideTime = 0.5f;
    [SerializeField] private float openTime = 0.5f;

    private Coroutine currentCorutine;

    public void OnSetup()
    {
        firstPanel.alpha = 0;
        firstPanel.blocksRaycasts = false;

        secondPanel.alpha = 0;
        secondPanel.blocksRaycasts = false;
    }

    public void OpenFirstPanel()
    {
        OpenWindow(0);
    }

    private void HideWindow(CanvasGroup panelGroup)
    {
        panelGroup.DOFade(0, hideTime);

        if (panelGroup == firstPanel)
        {
            awardParticlesContainer.SetActive(false);
        }
        lastCanvasGroup.blocksRaycasts = false;
        lastCanvasGroup = null;
    }

    public void OpenWindow(int windowNumber)
    {
        if (lastCanvasGroup != null)
        {
            HideWindow(lastCanvasGroup);
        }

        switch (windowNumber)
        {
            case 0:
                firstPanel.blocksRaycasts = true;
                titleText.alpha = 1;
                StartCoroutine(OpenWindow(firstPanel));
                break;

            case 1:
                SoundManager.Instance.Play("in-a-row");
                secondPanel.blocksRaycasts = true;
                titleText.alpha = 1;
                StartCoroutine(OpenWindow(secondPanel));
                break;
        }
    }

    private IEnumerator OpenWindow(CanvasGroup panelGroup)
    {
        lastCanvasGroup = panelGroup;
        panelGroup.DOFade(1, openTime);

        if (panelGroup == firstPanel)
        {
            awardParticlesContainer.SetActive(true);
        }

        if (panelGroup == secondPanel)
        {
            levelCompletePopup.PlayCategoryCoinsAnimation();
            uiRibbonAnimation.PlayRibbonAnimation();
        }

        yield return new WaitForSeconds(openTime);
    }
}