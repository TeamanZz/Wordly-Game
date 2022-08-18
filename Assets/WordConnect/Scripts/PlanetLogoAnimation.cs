using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlanetLogoAnimation : MonoBehaviour
{
    public int rotatePerSec = 10;
    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, 10), 1f, RotateMode.Fast).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}