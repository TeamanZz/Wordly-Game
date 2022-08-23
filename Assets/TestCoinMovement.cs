using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestCoinMovement : MonoBehaviour
{
    public RectTransform elementRect;
    public RectTransform targetRect;

    private void Start()
    {
        var toDestinationInWorldSpace = targetRect.position - elementRect.position;
        var toDestinationInLocalSpace = elementRect.InverseTransformVector(toDestinationInWorldSpace);
        elementRect.DOAnchorPos(toDestinationInLocalSpace, 1f).SetRelative(true);
        elementRect.DOScale(0.3f, 1);
        Destroy(this, 0.3f);
    }
}
