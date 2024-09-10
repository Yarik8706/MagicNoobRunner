using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreFinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour playerBehaviourScript = other.attachedRigidbody.GetComponent<PlayerBehaviour>();
        if (playerBehaviourScript)
        {
            playerBehaviourScript.StartPreFinishBehaviour();
        }
    }

}
