using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using BBG;
using WordConnect;

public class DailyRewardPopup : Popup
{
    public DailyRewardController dailyRewardController;
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
        InitializeVariables();
    }

    private void OnEnable()
    {
        Reset();
    }

    private void Reset()
    {
        for (int i = 0; i < giftItemsComponents.Count; i++)
        {
            giftItemsComponents[i].Reset();
        }

        animator.Rebind();
        animator.Update(0);
        commonText.alpha = 1;
    }

    private void InitializeVariables()
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
        GameController.Instance.Coins += dailyRewardController.coinsAwarded;
        dailyRewardController.PlayDailyGiftCoinsAnimation();
        StartCoroutine(IEHidePopupAfterDelay());
    }

    public void PlayRewardAnimation(int itemIndex)
    {
        for (int i = 0; i < itemsCanvasGroups.Count; i++)
        {
            itemsCanvasGroups[i].blocksRaycasts = false;
            giftItemsComponents[i].KillAnimation();

            if (i == itemIndex)
            {
                giftItemsComponents[i].PlayOpenAnimation();
                continue;
            }

            //except selectedItem
            itemsCanvasGroups[i].DOFade(0, 0.5f);
        }
        commonText.DOFade(0, 0.5f);

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

    private IEnumerator IEHidePopupAfterDelay()
    {
        yield return new WaitForSeconds(CoinController.Instance.delayBetweenCoins * dailyRewardController.coinsAwarded);
        Hide(true);

    }
}