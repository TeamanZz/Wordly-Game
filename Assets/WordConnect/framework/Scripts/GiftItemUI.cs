using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GiftItemUI : MonoBehaviour
{
    public Transform lid;

    private Sequence mainSequence;
    private RectTransform rect;
    private RectTransform lidRect;
    private Vector2 startPosition;
    private CanvasGroup canvasGroup;

    private bool wasInitialized = false;

    private void Awake()
    {
        Initialize();
    }

    void Start()
    {
        Sequence s = DOTween.Sequence();
        mainSequence = s;
        s.Append(transform.DOScale(1.2f, 0.5f).SetEase(Ease.InOutBack));
        s.PrependInterval(2);
        s.SetLoops(-1, LoopType.Yoyo);
    }

    private void Initialize()
    {
        wasInitialized = true;
        rect = GetComponent<RectTransform>();
        lidRect = lid.GetComponent<RectTransform>();
        startPosition = rect.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void PlayOpenAnimation()
    {
        lid.DOLocalRotate(new Vector3(0, 0, 100), 0.6f).SetDelay(0.7f).SetEase(Ease.OutBack);
    }

    public void KillAnimation()
    {
        mainSequence.Kill();
    }

    public void Reset()
    {
        if (!wasInitialized)
            Initialize();

        rect.anchoredPosition = startPosition;
        transform.localScale = Vector3.one;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        lid.localEulerAngles = Vector2.zero;
    }
}