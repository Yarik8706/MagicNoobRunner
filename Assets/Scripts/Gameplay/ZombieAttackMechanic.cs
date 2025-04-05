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
                PlayerDieMechanic.Instance.Die();
            }
        }
    }
}