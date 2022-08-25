using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using WordConnect;
using TMPro;
using System;

public class DailyRewardController : MonoBehaviour
{
    public int coinsAwarded;
    [SerializeField] private RectTransform fromRect = null;
    private bool isRewardReady;
    public GameObject dailyGiftButton;
    [Header("Timing")]
    //wait 23 Hours to activate the next reward (it's better to use 23h instead of 24h)
    [SerializeField] double nextRewardDelay = 23f;
    //check if reward is available every 5 seconds
    [SerializeField] float checkForRewardDelay = 5f;

    private void Start()
    {
        //Check if the game is opened for the first time then set Reward_Claim_Datetime to the current datetime
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Reward_Claim_Datetime")))
            PlayerPrefs.SetString("Reward_Claim_Datetime", DateTime.Now.ToString());

        StartCoroutine(CheckForRewards());
    }

    public void PlayDailyGiftCoinsAnimation()
    {
        List<RectTransform> fromPositions = new List<RectTransform>();

        for (int i = 0; i <= 10; i++)
        {
            fromPositions.Add(fromRect);
        }

        CoinController.Instance.AnimateCoins(GameController.Instance.Coins - coinsAwarded, GameController.Instance.Coins, fromPositions);
        // categoryCoinsText.text = (categoryCoinsAmountTo - categoryCoinsAmountFrom).ToString() + " COINS";

        PlayCoinsGroupJumpAnimation();
    }

    private IEnumerator CheckForRewards()
    {
        while (true)
        {
            if (!isRewardReady)
            {
                DateTime currentDatetime = DateTime.Now;
                DateTime rewardClaimDatetime = DateTime.Parse(PlayerPrefs.GetString("Reward_Claim_Datetime", currentDatetime.ToString()));

                //get total Hours between this 2 dates
                double elapsedHours = (currentDatetime - rewardClaimDatetime).TotalHours;

                if (elapsedHours >= nextRewardDelay)
                    dailyGiftButton.SetActive(true);
            }

            yield return new WaitForSeconds(checkForRewardDelay);
        }
    }

    private void PlayCoinsGroupJumpAnimation()
    {
        // categoryCoinsText.transform.DOScale(1, 1f).From(0).SetEase(Ease.OutBack);
    }
}
