using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HomeButtonControl : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            GameEvents.StartGameEvent.AddListener(DisableButton);
            GameEvents.ResetLevelEvent.AddListener(EnableButton);
        }

        private void DisableButton()
        {
            _button.interactable = false;
        }
        
        private void EnableButton()
        {
            _button.interactable = true;
        }
    }
}