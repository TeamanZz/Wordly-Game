using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using BBG;

namespace WordConnect
{
    public class LevelCompletePopup : BBG.Popup
    {
        #region Inspector Variables
        [SerializeField] private RewardPopup rewardPopup;

        [SerializeField] private Image backgroundImage = null;
        [SerializeField] private Text nextLevelButtonText = null;
        [SerializeField] private Text currentLevelTitleText = null;

        [Space]
        [SerializeField] private GameObject categoryCoinGroup;
        [SerializeField] private RectTransform categoryCoinPrizeIcon = null;
        [SerializeField] private Text categoryCoinsText = null;

        private object[] lastInData;

        #endregion

        #region Member Variables

        public const string PlayNextAction = "play_next";
        public const string BackAction = "back";

        private IEnumerator animationEnumerator;

        #endregion

        #region Public Methods

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                PlayCoinsGroupJumpAnimation();
            }
        }

        public override void OnShowing(object[] inData)
        {
            lastInData = inData;
            Debug.Log("last in data changes");
            base.OnShowing(inData);
            // SoundManager.Instance.Stop("bkg-music");
            SoundManager.Instance.ChangeBackgroundMusicVolume("bkg-music", 0.1f);
            SoundManager.Instance.Play("success");
            rewardPopup.OnSetup();
            rewardPopup.OpenWindow(0);
            categoryCoinGroup.SetActive(false);


            ActiveLevel level = (ActiveLevel)inData[0];
            bool isLevelAlreadyComplete = (bool)inData[1];
            int currentGamePoints = (int)inData[2];
            int awardedGamePoints = (int)inData[3];
            int categoryCoinsAwarded = (int)inData[4];
            int categoryCoinsAmountFrom = (int)inData[5];
            int categoryCoinsAmountTo = (int)inData[6];
            int extraWordsCoinsAwarded = (int)inData[7];
            int extraWordsCoinsAmountFrom = (int)inData[8];
            int extraWordsCoinsAmountTo = (int)inData[9];
            bool isLastLevel = (bool)inData[10];

            backgroundImage.sprite = level.packInfo.background;
            nextLevelButtonText.text = isLastLevel ? "HOME" : string.Format("PLAY LEVEL {0}", level.levelData.GameLevelNumber + 1);
            currentLevelTitleText.text = isLastLevel ? "HOME" : string.Format("LEVEL {0} COMPLETE", level.levelData.GameLevelNumber);

            if (!isLevelAlreadyComplete)
            {
                int categoryNumberComplete = level.levelData.CategoryLevelNumber;
                int totalLevelsInCategory = level.categoryInfo.LevelDatas.Count;

                float categoryProgressFromValue = Mathf.Lerp(0.095f, 1f, (float)(categoryNumberComplete - 1) / (float)totalLevelsInCategory);
                float categoryProgressToValue = Mathf.Lerp(0.095f, 1f, (float)(categoryNumberComplete) / (float)totalLevelsInCategory);

                animationEnumerator =
                    Animate(
                        level,
                        currentGamePoints,
                        awardedGamePoints,
                        categoryProgressFromValue,
                        categoryProgressToValue,
                        categoryCoinsAwarded,
                        categoryCoinsAmountFrom,
                        categoryCoinsAmountTo,
                        extraWordsCoinsAwarded,
                        extraWordsCoinsAmountFrom,
                        extraWordsCoinsAmountTo);

                StartCoroutine(animationEnumerator);
            }
        }

        public void OnPlayNextClicked()
        {
            if (animationEnumerator != null)
            {
                StopCoroutine(animationEnumerator);

                animationEnumerator = null;
            }
            // SoundManager.Instance.Play("bkg-music");
            SoundManager.Instance.ChangeBackgroundMusicVolumeToDefault("bkg-music");

            Hide(false, new object[] { PlayNextAction });
        }

        public void OnBackClicked()
        {
            if (animationEnumerator != null)
            {
                StopCoroutine(animationEnumerator);

                animationEnumerator = null;
            }

            Hide(false, new object[] { BackAction });
        }

        #endregion

        #region Private Methods

        private IEnumerator Animate(
            ActiveLevel level,
            int currentGamePoints,
            int awardedGamePoints,
            float categoryProgressFromValue,
            float categoryProgressToValue,
            int categoryCoinsAwarded,
            int categoryCoinsAmountFrom,
            int categoryCoinsAmountTo,
            int extraWordsCoinsAwarded,
            int extraWordsCoinsAmountFrom,
            int extraWordsCoinsAmountTo)
        {
            float duration = 500;
            float betweenDelay = 0.25f;

            double timeEnd;

            // Wait for the popup to fade fully in
            yield return new WaitForSeconds(animDuration + betweenDelay);

            // Animate the ticking up of the game points
            timeEnd = Utilities.SystemTimeInMilliseconds + duration;

            while (true)
            {
                float t = 1f - (float)(timeEnd - Utilities.SystemTimeInMilliseconds) / duration;
                int gp = (int)Mathf.Lerp(1, awardedGamePoints, t);

                if (Utilities.SystemTimeInMilliseconds >= timeEnd)
                {
                    break;
                }

                yield return null;
            }

            // Animate the category progress bar
            timeEnd = Utilities.SystemTimeInMilliseconds + duration;

            while (true)
            {
                float t = 1f - (float)(timeEnd - Utilities.SystemTimeInMilliseconds) / duration;
                float value = Mathf.Lerp(categoryProgressFromValue, categoryProgressToValue, t);

                if (Utilities.SystemTimeInMilliseconds >= timeEnd)
                {
                    break;
                }

                yield return null;
            }

            yield return new WaitForSeconds(betweenDelay * 2f);
            animationEnumerator = null;
        }

        public void PlayCategoryCoinsAnimation()
        {
            int categoryCoinsAwarded = (int)lastInData[4];
            int categoryCoinsAmountFrom = (int)lastInData[5];
            int categoryCoinsAmountTo = (int)lastInData[6];

            if (categoryCoinsAwarded > 0)
            {
                List<RectTransform> fromPositions = new List<RectTransform>();

                for (int i = 0; i < 10; i++)
                {
                    fromPositions.Add(categoryCoinPrizeIcon);
                }

                categoryCoinGroup.SetActive(true);
                CoinController.Instance.AnimateCoins(categoryCoinsAmountFrom, categoryCoinsAmountTo, fromPositions);
                categoryCoinsText.text = (categoryCoinsAmountTo - categoryCoinsAmountFrom).ToString() + " COINS";

                PlayCoinsGroupJumpAnimation();
            }
        }

        private void PlayCoinsGroupJumpAnimation()
        {
            categoryCoinPrizeIcon.transform.DOScale(0, 1).From(1).SetEase(Ease.InBack);
            categoryCoinsText.transform.DOScale(1, 1f).From(0).SetEase(Ease.OutBack);
        }

        #endregion
    }
}