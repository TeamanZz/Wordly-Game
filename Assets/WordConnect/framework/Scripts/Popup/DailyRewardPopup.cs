using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using BBG;
using WordConnect;
using System;

public class DailyRewardPopup : Popup
{
    [SerializeField] private DailyRewardController dailyRewardController;
    [SerializeField] private Button colllectButton;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI commonText;
    [SerializeField] private GameObject coinParticle;
    [SerializeField] private GameObject dailyGiftButton;
    [SerializeField] private CanvasGroup mainCoin;
    [SerializeField] private RectTransform mainCoinRect;
    [SerializeField] private List<GameObject> giftItems = new List<GameObject>();

    private List<RectTransform> itemsRects = new List<RectTransform>();
    private List<CanvasGroup> itemsCanvasGroups = new List<CanvasGroup>();
    private List<GiftItemUI> giftItemsComponents = new List<GiftItemUI>();

    private Animator animator;

    private void Awake()
    {
        InitializeVariables();
    }

    public override void Hide(bool cancelled)
    {
        base.Hide(cancelled);
        coinParticle.SetActive(false);
    }

    public override void Show()
    {
        base.Show();
        coinParticle.SetActive(true);
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
        colllectButton.enabled = false;
        PlayerPrefs.SetString("Reward_Claim_Datetime", DateTime.Now.ToString());
        StartCoroutine(IEHidePopupAfterDelay());
    }

    public void PlayRewardAnimation(int itemIndex)
    {
        SoundManager.Instance.Play("lootbox-select");
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

        coinsText.text = dailyRewardController.coinsAwarded + " coins";

        commonText.DOFade(0, 0.5f);
        itemsRects[itemIndex].DOAnchorPos(new Vector2(0, -55), 1);
        itemsRects[itemIndex].transform.DOScale(2f, 1f).SetEase(Ease.OutBack);
        var giftSequence = DOTween.Sequence();
        giftSequence.Insert(1, itemsCanvasGroups[itemIndex].DOFade(0, 0.5f));

        StartCoroutine(IEPlayRewardAnimation(itemIndex));
    }

    private IEnumerator IEPlayRewardAnimation(int itemIndex)
    {
        yield return new WaitForSeconds(0.6f);
        // SoundManager.Instance.Play("lootbox");

        for (int i = 0; i < itemsCanvasGroups.Count; i++)
        {
            if (i != itemIndex)
                giftItems[i].SetActive(false);
        }
        animator.Play("Daily Reward", 0, 0);
    }

    private IEnumerator IEHidePopupAfterDelay()
    {
        yield return new WaitForSeconds(CoinController.Instance.delayBetweenCoins * 10);
        dailyGiftButton.SetActive(false);
        Hide(true);
    }
}