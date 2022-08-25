using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using BBG;

public class DailyRewardPopup : Popup
{
    public TextMeshProUGUI commonText;
    public CanvasGroup mainCoin;
    public RectTransform mainCoinRect;
    public List<GameObject> giftItems = new List<GameObject>();
    private List<RectTransform> itemsRects = new List<RectTransform>();
    private List<CanvasGroup> itemsCanvasGroups = new List<CanvasGroup>();
    private List<GiftItemUI> giftItemsComponents = new List<GiftItemUI>();

    private Animator animator;

    private void Awake()
    {
        mainCoinRect = mainCoin.GetComponent<RectTransform>();
        animator = GetComponent<Animator>();

        for (int i = 0; i < giftItems.Count; i++)
        {
            itemsRects.Add(giftItems[i].GetComponent<RectTransform>());
            itemsCanvasGroups.Add(giftItems[i].GetComponent<CanvasGroup>());
            giftItemsComponents.Add(giftItems[i].GetComponent<GiftItemUI>());
        }
    }

    public void CollectCoins()
    {

    }

    public void PlayRewardAnimation(int itemIndex)
    {
        for (int i = 0; i < itemsCanvasGroups.Count; i++)
        {
            itemsCanvasGroups[i].blocksRaycasts = false;
            giftItemsComponents[i].KillAnimation();

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