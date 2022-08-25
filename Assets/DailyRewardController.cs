using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using WordConnect;
using TMPro;

public class DailyRewardController : MonoBehaviour
{
    public int coinsAwarded;
    [SerializeField] private RectTransform fromRect = null;
    // [SerializeField] private TextMeshProUGUI categoryCoinsText = null;

    public void PlayDailyGiftCoinsAnimation()
    {
        List<RectTransform> fromPositions = new List<RectTransform>();

        for (int i = 0; i < coinsAwarded; i++)
        {
            fromPositions.Add(fromRect);
        }

        CoinController.Instance.AnimateCoins(GameController.Instance.Coins - coinsAwarded, GameController.Instance.Coins, fromPositions);
        // categoryCoinsText.text = (categoryCoinsAmountTo - categoryCoinsAmountFrom).ToString() + " COINS";

        PlayCoinsGroupJumpAnimation();
    }

    private void PlayCoinsGroupJumpAnimation()
    {
        // categoryCoinsText.transform.DOScale(1, 1f).From(0).SetEase(Ease.OutBack);
    }
}
