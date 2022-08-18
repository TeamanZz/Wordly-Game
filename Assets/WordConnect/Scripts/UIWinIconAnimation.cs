using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIWinIconAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        PlayOutBackAnimation();
    }

    public void PlayOutBackAnimation()
    {
        transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack).From(Vector3.zero);
    }
}