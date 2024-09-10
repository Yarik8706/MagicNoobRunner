using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonDotween : MonoBehaviour
{
    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 2f, 0, 3f));
        sequence.SetLoops(-1);
    }
}
