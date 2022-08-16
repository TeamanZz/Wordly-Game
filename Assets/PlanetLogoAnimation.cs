using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlanetLogoAnimation : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, 5), 2f, RotateMode.Fast).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}