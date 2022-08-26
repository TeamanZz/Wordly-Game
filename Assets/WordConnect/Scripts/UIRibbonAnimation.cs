using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIRibbonAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform ribbonRect;
    [SerializeField] private RectTransform textRect;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Text ribbonText;
    [SerializeField] private List<string> ribbonTitles = new List<string>();

    [ContextMenu("Play Ribbon")]
    public void PlayRibbonAnimation()
    {
        if (particle != null)
            particle.Play();

        ribbonText.text = ribbonTitles[Random.Range(0, ribbonTitles.Count)];
        ribbonRect.DOSizeDelta(new Vector2(840, 200), 1).SetEase(Ease.OutBack).From(Vector2.zero);
        textRect.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack).From(Vector2.zero);
    }

    public void PlayRibbonAnimationFade()
    {
        ribbonRect.DOSizeDelta(Vector2.zero, 1).SetEase(Ease.InBack);
        textRect.DOScale(Vector3.zero, 0.8f).SetEase(Ease.InBack);
    }
}