using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using WordConnect;

public class RewardPopup : MonoBehaviour
{
    [SerializeField] private LevelCompletePopup levelCompletePopup;

    [SerializeField] private CanvasGroup firstPanel;
    [SerializeField] private CanvasGroup secondPanel;
    [SerializeField] private CanvasGroup thirdPanel;

    [SerializeField] private CanvasGroup lastCanvasGroup;
    private Coroutine currentCorutine;

    [SerializeField] private float hideTime = 0.5f;
    [SerializeField] private float openTime = 0.5f;

    public void Awake()
    {
        OnSetup();
    }

    public void OnSetup()
    {
        firstPanel.alpha = 0;
        firstPanel.blocksRaycasts = false;
        
        secondPanel.alpha = 0;
        secondPanel.blocksRaycasts = false;

        thirdPanel.alpha = 0;
        thirdPanel.blocksRaycasts = false;
    }

    [ContextMenu("Open First")]
    public void OpenFirstPanel()
    {
        OpenWindow(0);
    }

    private IEnumerator HideWindow(CanvasGroup panelGroup)
    {
        panelGroup.DOFade(0, hideTime);
        yield return new WaitForSeconds(hideTime);

        lastCanvasGroup.blocksRaycasts = false;
        lastCanvasGroup = null;
        Debug.Log($"Hide {lastCanvasGroup}");
    }

    public void OpenWindow(int windowNumber)
    {
        if (lastCanvasGroup != null)
        {
            StartCoroutine(HideWindow(lastCanvasGroup));
        }

        switch (windowNumber)
        {
            case 0:
                firstPanel.blocksRaycasts = true;
                StartCoroutine(OpenWindow(firstPanel));
                break;

            case 1:
                secondPanel.blocksRaycasts = true;
                StartCoroutine(OpenWindow(secondPanel));
                break;

            case 2:
                thirdPanel.blocksRaycasts = true;
                StartCoroutine(OpenWindow(thirdPanel));
                break;
        }
    }

    private IEnumerator OpenWindow(CanvasGroup panelGroup)
    {
        panelGroup.DOFade(1, openTime);
        yield return new WaitForSeconds(openTime);
        lastCanvasGroup = panelGroup;
        Debug.Log($"Open {lastCanvasGroup}");
    }

}