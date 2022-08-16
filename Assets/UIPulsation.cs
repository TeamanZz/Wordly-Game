using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIPulsation : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(1.05f, 1).SetLoops(-1, LoopType.Yoyo);
    }
}
