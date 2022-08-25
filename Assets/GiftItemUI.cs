using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GiftItemUI : MonoBehaviour
{
    private Sequence mainSequence;

    private RectTransform rect;
    private Vector2 startPosition;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        startPosition = rect.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }

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

    public void Reset()
    {
        rect.anchoredPosition = startPosition;
        transform.localScale = Vector3.one;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}