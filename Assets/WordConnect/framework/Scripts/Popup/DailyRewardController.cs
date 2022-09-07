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
    public GameObject dailyGiftButton;

    [SerializeField] private RectTransform fromRect = null;

    [Header("Timing")]
    [SerializeField] double nextRewardDelay = 23f;
    [SerializeField] float checkForRewardDelay = 5f;

    public bool isRewardReady;

    private void Start()
    {
        //Check if the game is opened for the first time then set Reward_Claim_Datetime to the current datetime
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Reward_Claim_Datetime")))
        {
            isRewardReady = true;
            // PlayerPrefs.SetString("Reward_Claim_Datetime", DateTime.Now.ToString());
        }

        StartCoroutine(CheckForRewards());
    }

    [ContextMenu("Delete Prefs")]
    public void ResetPrefs()
    {
        PlayerPrefs.DeleteKey("Reward_Claim_Datetime");
    }

    public void PlayDailyGiftCoinsAnimation()
    {
        List<RectTransform> fromPositions = new List<RectTransform>();

        for (int i = 0; i <= 10; i++)
        {
            fromPositions.Add(fromRect);
        }

        CoinController.Instance.AnimateCoins(GameController.Instance.Coins - coinsAwarded, GameController.Instance.Coins, fromPositions);
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
                {
                    isRewardReady = true;
                    dailyGiftButton.SetActive(true);
                }
            }
            else
            {
                dailyGiftButton.SetActive(true);
            }

            yield return new WaitForSeconds(checkForRewardDelay);
        }
    }
}