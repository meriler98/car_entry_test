using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIUtilities
{
    public static void ForceLayoutRebuild(MonoBehaviour behaviour, RectTransform rectTransform)
    {
        behaviour.StartCoroutine(RebuildLayoutDelayed(rectTransform));
    }

    private static IEnumerator RebuildLayoutDelayed(RectTransform rectTransform)
    {
        yield return null;
        LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
    }
}