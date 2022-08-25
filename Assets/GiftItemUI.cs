using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GiftItemUI : MonoBehaviour
{
    private Sequence mainSequence;
    void Start()
    {
        Sequence s = DOTween.Sequence();
        mainSequence = s;
        s.Append(transform.DOScale(1.2f, 0.5f).SetEase(Ease.InOutBack));
        s.PrependInterval(2);
        s.SetLoops(-1, LoopType.Yoyo);
    }

    public void KillAnimation()
    {
        mainSequence.Kill();
    }
}
