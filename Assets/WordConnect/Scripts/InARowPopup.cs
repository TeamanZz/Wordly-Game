using System.Collections;
using System.Collections.Generic;
using BBG;
using UnityEngine;

public class InARowPopup : Popup
{
    public UIRibbonAnimation ribbonAnimation;
    private void OnEnable()
    {
        ribbonAnimation.PlayRibbonAnimation();
        StartCoroutine(HideRibonAfterDelay());
    }

    private IEnumerator HideRibonAfterDelay()
    {
        yield return new WaitForSeconds(2);
        ribbonAnimation.PlayRibbonAnimationFade();
        yield return new WaitForSeconds(1);
        Hide(true);
    }
}
