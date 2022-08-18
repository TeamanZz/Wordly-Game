using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIRibbonAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform ribbonRect;
    [SerializeField] private RectTransform textRect;
    [ContextMenu("Play Ribbon")]
    public void PlayRibbonAnimation()
    {
        ribbonRect.DOSizeDelta(new Vector2(840, 200), 1).SetEase(Ease.OutBack).From(Vector2.zero);
        textRect.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack).From(Vector2.zero);
    }
}
