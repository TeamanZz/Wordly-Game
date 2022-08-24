using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DailyRewardPopup : MonoBehaviour
{
    public TextMeshProUGUI commonText;
    public CanvasGroup mainCoin;
    public RectTransform mainCoinRect;

    public Animator animator;
    public List<RectTransform> itemsRects = new List<RectTransform>();
    public List<CanvasGroup> itemsCanvasGroups = new List<CanvasGroup>();

    private void Awake()
    {
        mainCoinRect = mainCoin.GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
    }

    public void GetDailyReward(int itemIndex)
    {
        for (int i = 0; i < itemsCanvasGroups.Count; i++)
        {
            itemsCanvasGroups[i].blocksRaycasts = false;

            if (i == itemIndex)
                continue;

            commonText.DOFade(0, 0.5f);
            itemsCanvasGroups[i].DOFade(0, 0.5f);
        }

        itemsRects[itemIndex].DOAnchorPos(new Vector2(0, -55), 1);
        itemsRects[itemIndex].transform.DOScale(2f, 1f).SetEase(Ease.OutBack);

        var giftSequence = DOTween.Sequence();
        giftSequence.Insert(1, itemsCanvasGroups[itemIndex].DOFade(0, 0.5f));

        StartCoroutine(IEPlayRewardAnimation());
    }

    private IEnumerator IEPlayRewardAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        animator.Play("Daily Reward", 0, 0);

    }
}