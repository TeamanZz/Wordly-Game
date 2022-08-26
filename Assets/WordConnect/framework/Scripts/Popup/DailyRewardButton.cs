using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DailyRewardButton : MonoBehaviour
{
    private void Start()
    {
        PlayScaleJumpAnimation();
    }

    private void PlayScaleJumpAnimation()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(1.2f, 0.5f).SetEase(Ease.InOutBack));
        s.PrependInterval(2);
        s.SetLoops(-1, LoopType.Yoyo);
    }
}