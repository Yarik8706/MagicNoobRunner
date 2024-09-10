using System;
using UnityEngine;

namespace Gameplay
{
    public class ZombieAttackMechanic : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _animator.SetTrigger("Attack");
                Debug.Log("Die Die Die");
                other.GetComponent<PlayerBehaviour>().DefeatBehaviour();
            }
        }
    }
}