using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour playerBehaviourScript = other.attachedRigidbody.GetComponent<PlayerBehaviour>();
        if (playerBehaviourScript)
        {
            GameEvents.WinEvent.Invoke();
            playerBehaviourScript.WinBehaviour();
            GameManager.Instance.ShowFinishWindowWin();
        }
    }
}
