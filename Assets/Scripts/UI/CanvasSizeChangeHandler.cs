using System;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;

namespace Flatformer.GameData.UIManager
{
    public class CanvasSizeChangeHandler : MonoBehaviour
    {
        [SerializeField] private SimpleScrollSnap firstScrollSnap;
        [SerializeField] private SimpleScrollSnap secondScrollSnap;
        
        private void OnRectTransformDimensionsChange()
        {
            firstScrollSnap.Setup();
            secondScrollSnap.Setup();
        }
    }
}