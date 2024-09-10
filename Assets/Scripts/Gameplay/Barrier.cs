using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] GameObject _bricksEffectPreafad;
    private void OnTriggerEnter(Collider other)
    {
        PlayerDeformation playerDeformation = other.attachedRigidbody.GetComponent<PlayerDeformation>();
        if (playerDeformation)
        {
            playerDeformation.HitBarrier();
            Destroy(gameObject);
            Instantiate(_bricksEffectPreafad, transform.position, transform.rotation);
        }
    }

}
